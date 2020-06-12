// require npm dotenv package to access environment variables
require('dotenv').config();

// build context
const express = require('express');
const http = require('http');
const app = express();
const bodyParser = require('body-parser');
const port = process.env.PORT;

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
const tonyTestStart = require('./BlockKits/tonyTestStart'); // TONY: This pulls in the function from the specified file to be used below
const selectOperationModal = require('./Crud-Y_Modals/selectOperationModal'); // TONY: This file contains the actual modal block
const basicSearchReturn = require('./Crud-y_Modals/BasicSearchReturn')

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
        if (message.text == 'Help') {
            var block = greeting.greeting(message);
            var parsedBlock = JSON.parse(block);
            const response = await web.chat.postMessage(parsedBlock);
        }
        if (message.text == 'question card') {
            var block = qcStart.questionCardStart(message);
            var parsedBlock = JSON.parse(block);
                const response = await web.chat.postMessage(parsedBlock);
        }
        // TONY: typing 'tony test' will run this and return the contents of the tonyTestStart file
        if (message.text == 'tony test') {//I'm pretty sure there is a contains method
            //var myjson = jsonbuilder.buildmyjson("student", "name", "search", "help this is killing me", message.text, 0, "name", "name", "name", 0, false, 0)
            //var thisresponse = myaxios.AxiosPostRequest()

            //send the default case to meeeeeeeeeeee.
            var reformatted = basicSearchReturn.SearchApiFormatter(message.text);
            var apiresponse = await axios.get('http://localhost:58685/api/values/'+reformatted);  
            var parsedBlock = basicSearchReturn.BasicSearch(apiresponse, message);
            const response = await web.chat.postMessage(parsedBlock);

             // postMessage(parsedBlock) accepts the entire JSON payload from tonyTestStart and pushes it to the chat window in Slack
        }
      } catch (error) {
        console.log(error.data);
      }
    })();
});



// interactivity functions
slackInteractions.action({ "action-id": "launchQuestionCardModal" }, async (payload) => {
    try {
        var openModal = JSON.parse(questionCard.questionCard(payload.trigger_id));
        await web.views.open( openModal );
    } catch (e) {
        console.log(e);
    }

    return {
        text: 'Processing...'
    }
});

// TONY: interactivity function: invoked when the button is clicked from the first message sent from tonyTestStart()
function getpost(){

    var input = jsonbuilder.buildmyjson()
    var output = myaxios.AxiosPostRequest(input)
}



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
})

// new funciton

// modal submit functions
slackInteractions.viewSubmission('questionCardSubmit', async (payload) => {
    const blockData = payload.view.state;
    const myGoal = blockData.values.myGoalBlock.myGoalResponse.value;
    const myProblem = blockData.values.myProblemBlock.myProblemResponse.value;
    const myAttempts = blockData.values.myAttemptsBlock.myAttemptsResponse.value;

    try {
        const msg = JSON.parse(qcPost.questionCardPost(myGoal, myProblem, myAttempts, payload.user.name, instructorChannel));
        const response = await web.chat.postMessage(msg);
        // Below is not currently working; idea is to post validation back to user that the message was sent. Need to find a clever way to access current channel ID
        // const verification = await web.chat.postMessage({channel: channel.id!?, text: 'Your card has been submitted and will be reviewed by your instructors.'});
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