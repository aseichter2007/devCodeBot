// require npm dotenv package to access environment variables
require('dotenv').config();

// build context
const express = require('express');
const http = require('http');
const app = express();
const bodyParser = require('body-parser');
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
const qcPost = require('./BlockKits/qc-post'); // TONY: reference this file to see how I handle input data from the modal and posting it back to the instructor chat. You will need to create your own file similar to this one to post the payload back to your API
const timerUp = require('./BlockKits/timerUp');
const selectOperationModal = require('./Crud-Y_Modals/selectOperationModal'); // TONY: This file contains the actual modal block
const basicSearchReturn = require('./Crud-y_Modals/BasicSearchReturn')
const testblock = require('./Crud-y_Modals/cruddyprototye')
const dbaccess = require('./Crud-y_Modals/DbAccess.js');


//tony includes
const axios = require("axios");
const myaxios = require('./Crud-y_Modals/axiosmethods');
const jsonbuilder = require('./Crud-y_Modals/jsonbuilder');
const { Console } = require('console');


// configure express server for development
app.use('/slack/actions', slackInteractions.expressMiddleware());
app.use('/slack/events', slackEvents.expressMiddleware());
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());


//testing junk by tony 



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

                case 'manage':
                     var block = dbaccess.DbAcess(message);
                     var parsedBlock = JSON.parse(block);
                     const manageresponse = await web.chat.postMessage(parsedBlock)
                    break;
                default:
                    var user = message.user;
                   // var userinfo = await web.users.info(user);   //fowley I would be pretty stoked if you could sort this out. We need it for search logging
                    
                //     var myjson = jsonbuilder.buildmyjson("student", "name", "search", "type", message.text, 0, "name", "name", "name", 0, false, 0)
                //     var postme = JSON.stringify(myjson);
                //     var url = 'http://localhost:58685/api/values/';
                //     var thisresponse = await axios.axios({
                //         method: 'post',
                //         url: url,
                //         data: postme,
                //     })  
                //     .then((res) => {
                //         console.log(res)
                //     }).catch((error) => {
                //     console.error(error)
                // });


                    //send the default case to meeeeeeeeeeee.
                    var reformatted = basicSearchReturn.SearchApiFormatter(message.text);
                    var apiresponse = await axios.get('http://localhost:58685/api/values/'+reformatted);  
                    var parsedBlock = basicSearchReturn.BasicSearch(apiresponse, message);
                    const response = await web.chat.postMessage(parsedBlock);
                    // postMessage(parsedBlock) accepts the entire JSON payload from basicsearch and pushes it to the chat window in Slack
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

// TONY: interactivity function:
slackInteractions.action({"action-id": "selectoperation" }, async (payload) =>{
    try {
        //call manage selection modal
        var block = selectOperationModal.ManageActionSelect(payload.trigger_id);
        var openModal = JSON.parse(block);
        await web.views.open( openModal );

    } catch (e) {
        console.log(e);
    }
});

//tony modal calls
slackInteractions.action({"action-id": "manageactionselected" }, async (payload) =>{
    try {

        //testing prototype crud modal 
        var block = testblock.testblockdonotupvote(payload.trigger_id);
        var openModal = JSON.parse(block);
        await web.views.open( openModal );

    } catch (e) {
        console.log(e);
    }
});
slackInteractions.viewSubmission('manageactionselect', async (payload) => {

    var trigger = payload.trigger_id;
    var actionselect = payload.view.state.values.selectAction.manageactionselected.selected_option.value;
    var powstdata = jsonbuilder.buildmyjson("instructor", "na", actionselect, actionselect, no, 0, "no", "no", "no", 0, false, 0)

    var gettingdata = axios.post('http://localhost:58685/api/values/'[postdata])
    var databack = gettingdata;

    try {
        
    } catch (e) {
        console.log(e);
    }
});

//I dont think I am using this currently but better safe than sorry
slackInteractions.action({ "action-id": "manageactionselect" }, async (payload) => {
    try {
        var openTonyModal = JSON.parse(tonyTestModal.tonyTestModal(/*Your parameters to send in from the payload */)); // This prepares the modal in JSON format
        await web.views.open( openTonyModal ); // This actually opens the modal in the UI
    } catch (e) {
        console.log(e); // For debugging
    }

    return {
        text: 'Processing...'
    }
});

// new funciton

// modal submit functions
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

//TONY: test submission; do with this what you want, copy the above pattern to access input values from the modal and then, in the try block, do something with the captured data
slackInteractions.viewSubmission('tonyTestModalSubmit', async (payload) => {
    // extract input data into variables
    console.log('Extracting input data');

    try {
        console.log('Do things with input data');
    } catch (e) {
        console.log(e);
    }
})

// Handle errors (see `errorCodes` export)
slackEvents.on('error', console.error);

// Starts express server
http.createServer(app).listen(port, () => {
    console.log(`server listening on port ${port}`);
});