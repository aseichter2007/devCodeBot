// require npm dotenv package to access environment variables
require('dotenv').config();

// build context
const express = require('express');
const http = require('http');
const app = express();
const bodyParser = require('body-parser');
const axios = require('axios');
const port = process.env.PORT;

// custom npm packages for functionality
const unixTimestamp = require('unix-timestamp');

// create adapters for Slack APIs
const { createEventAdapter } = require('@slack/events-api');
const slackEvents = createEventAdapter(process.env.SLACK_SIGNING_SECRET);
const { WebClient } = require('@slack/web-api');
const token = process.env.SLACK_BOT_TOKEN;
const web = new WebClient(token);
const { createMessageAdapter } = require('@slack/interactive-messages');
const slackInteractions = createMessageAdapter(process.env.SLACK_SIGNING_SECRET);
const instructorChannel = process.env.INSTRUCTOR_CHANNEL_ID;

// import message payloads
const greeting = require('./BlockKits/greeting');
const questionCard = require('./BlockKits/qc');
const qcStart = require('./BlockKits/qc-start');
const qcPost = require('./BlockKits/qc-post');
const timerUp = require('./BlockKits/timerUp');

// configure express server for development
app.use('/slack/actions', slackInteractions.expressMiddleware());
app.use('/slack/events', slackEvents.expressMiddleware());
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

// on user sends a direct message
slackEvents.on('message', (message, body) => {
    (async () => {
      try {
        // Respond to the message back in the same channel depending on input
        if (typeof message.bot_id === 'undefined') {
            switch (message.text) {   
                case 'question card':
                    var block = qcStart.questionCardStart(message);
                    var parsedBlock = JSON.parse(block);
                    let qcResponse = await web.chat.postMessage(parsedBlock);
                    break;
                case 'timebox':
                    var block = timerUp.timerUp(message, unixTimestamp.now('+2m'));
                    var parsedBlock = JSON.parse(block);
                    var checkIfTimeboxExists = await web.chat.scheduledMessages.list({ channel: message.channel });
                    var timeboxValue = checkIfTimeboxExists.scheduled_messages[0];
                    if ( typeof timeboxValue !== 'undefined' ) {
                        let oneTimeboxResponse = await web.chat.postMessage({ channel: message.channel, text: 'You can only have one timebox timer running at a time.' });
                    } else {
                        let immediateResponse = await web.chat.postMessage({ channel: message.channel, text: 'Okay, starting a timer for 30 minutes now!' });
                        let delayedResponse = await web.chat.scheduleMessage(parsedBlock);
                    }
                    break;
                case 'cancel timebox':
                    try {
                        let queuedMessage = await web.chat.scheduledMessages.list({ channel: message.channel, limit: 1 });
                        let queuedMessageId = queuedMessage.scheduled_messages[0].id;
                        let deleteMessage = await web.chat.deleteScheduledMessage({ channel: message.channel, scheduled_message_id: queuedMessageId });
                        let cancelTbResponse = await web.chat.postMessage({ channel: message.channel, text: 'I cancelled your timebox timer!' });
                    } catch (e) {
                        let cancelTbErrResponse = await web.chat.postMessage({ channel: message.channel, text: 'You don\'t have a timebox timer running right now!' });
                    }
                    break;
                case 'help':
                    var block = greeting.greeting(message);
                    var parsedBlock = JSON.parse(block);
                    let defaultResponse = await web.chat.postMessage(parsedBlock);
                    break;
                default:
                    break;
            }
        }
    } catch (error) {
        console.log(error.data);
    }
    })();
});

// interactivity functions
slackInteractions.action({ "action-id": "launchQuestionCardModal" }, async (payload) => {
    try {
        var openModal = JSON.parse(questionCard.questionCard(payload.trigger_id, payload.channel.id));
        await web.views.open( openModal );
    } catch (e) {
        console.log(e);
    }

    return {
        text: 'Processing...'
    }
});

// modal submit function
slackInteractions.viewSubmission('questionCardSubmit', async (payload) => {
    const blockData = payload.view.state;
    const myGoal = blockData.values.myGoalBlock.myGoalResponse.value;
    const myProblem = blockData.values.myProblemBlock.myProblemResponse.value;
    const myAttempts = blockData.values.myAttemptsBlock.myAttemptsResponse.value;

    try {
        const msg = JSON.parse(qcPost.questionCardPost(myGoal, myProblem, myAttempts, payload.user.name, instructorChannel, payload.user.id));
        const response = await web.chat.postMessage(msg);
        const verification = await web.chat.postMessage({channel: payload.view.private_metadata, text: 'Your card has been submitted and will be reviewed by your instructors.'});
    } catch (e) {
        console.log(e);
    }

    return {
        response_action: "clear"
    }
});

// Handle errors (see `errorCodes` export)
slackEvents.on('error', console.error);

// Starts express server
http.createServer(app).listen(port, () => {
    console.log(`server listening on port ${port}`);
});