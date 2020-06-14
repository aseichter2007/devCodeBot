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
const selectOperationModal = require('./CrudModal/selectOperationModal'); // TONY: This file contains the actual modal block
const basicSearchReturn = require('./CrudModal/BasicSearchReturn')
const testblock = require('./CrudModal/crudprototype')
const dbaccess = require('./CrudModal/DbAccess.js');


//tony includes
const axios = require("axios");
const myaxios = require('./CrudModal/axiosmethods');
const jsonbuilder = require('./CrudModal/jsonbuilder');
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
            var thisuser = await web.users.info(message)
            var call = message.text;
            switch (call.toLowerCase()) {   
                case 'question card':
                case 'questioncard':
                case 'give me a question card':
                case 'need question card':
                case 'question':
                case 'card':
                case 'i need a question card':
                    var block = qcStart.questionCardStart(message);
                    var parsedBlock = JSON.parse(block);
                    let qcResponse = await web.chat.postMessage(parsedBlock);
                    break;
                case 'timebox':
                case 'timebox me':
                case 'time':
                case 'time me':
                case 'give me a timebox':
                case 'start timer':
                case 'give me a timer':
                case 'i need a timebox':
                case 'i need a timer':
                    var block = timerUp.timerUp(message, unixTimestamp.now('+2m'));//set to 2 minutes for demonstration pusrposes, change for production.
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
                case 'cancel':
                case 'solved':
                case 'stop timebox':
                case 'stop':
                case 'stop timing me':
                case 'stop timer':
                case 'stop my timer':
                case 'remove timebox':
                case 'remove timer':
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
                case 'help me':
                case 'devcodebot':
                case 'hi':
                case 'hello':
                case 'yo':
                case 'whats up devcodebot':
                    var block = greeting.greeting(message);
                    var parsedBlock = JSON.parse(block);
                    let defaultResponse = await web.chat.postMessage(parsedBlock);
                    break;

                case 'manage':
                    if (thisuser.user.is_admin) {//if not admin they will fall though to default case. 
                        var block = dbaccess.DbAcess(message);
                        var parsedBlock = JSON.parse(block);
                        const manageresponse = await web.chat.postMessage(parsedBlock)
                       break;
                    }
                default:
                   
                   
                   //uses proper endpoint to allow logging and user info to pass in. 
                    var myjson = jsonbuilder.buildmyjson("student", thisuser.user.real_name, "search", "type", message.text, 0, "name", "name", "name", 0, false, 0)
                    var url = "http://localhost:58685/api/values/";
                 
                    axios.post(url,myjson)
                    .then((res) => {
                        var parsedBlock = basicSearchReturn.BasicSearch(res, message);
                        web.chat.postMessage(parsedBlock);
                    }).catch((error) => {
                    console.error(error)
                    });

                    //we could offer a timebox after search, I am not currently doing that.

            
                    // uses get method. need post version for logging.                    
                    // var reformatted = basicSearchReturn.SearchApiFormatter(message.text);
                    // var apiresponse = await axios.get('http://localhost:58685/api/values/'+reformatted);  
                    // var parsedBlock = basicSearchReturn.BasicSearch(apiresponse, message);
                    // const response = await web.chat.postMessage(parsedBlock);

                    // postMessage(parsedBlock) accepts the entire JSON payload from basicsearch and pushes it to the chat window in Slack
                    break;
            }
        }
    } catch (error) {
        console.log(error.data);
    }
    })();
});




// this isn't ideal but it lets us return different modals. 
slackInteractions.action({"action_id": "selectoperation" }, async (payload) =>{
   var data= payload.actions;
    var actions = data[0];
    var modalexpected = actions.block_id;
   if (modalexpected == "qcard") {
    try {
        var openModal = JSON.parse(questionCard.questionCard(payload.trigger_id, payload.channel.id));
        await web.views.open( openModal );
    } catch (e) {
        console.log(e);
    }
   }
   if (modalexpected == "manage") {
       
       try {
           //call manage selection modal
           var block = selectOperationModal.ManageActionSelect(payload.trigger_id, payload.channel.id);
           var openModal = JSON.parse(block);
           await web.views.open( openModal );
   
       } catch (e) {
           console.log(e);
       }
   }
});


// interactivity functions
slackInteractions.action({ "action_id": "launchQuestionCardModal" }, async (payload) => {
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

slackInteractions.viewSubmission('manageactionselect', async (payload) => {
    
    try {//attempt to get data reqired to update module
    var responseurl = payload.view.private_metadata;
    viewid = payload.view.id;
    //var trigger = payload.trigger_id;
    //var team = payload.view.team.id;
    var actionselect = payload.view.state.values.selectAction.manageactionselected.selected_option.value;
    var powstdata = jsonbuilder.buildmyjson("instructor", "na", actionselect, actionselect, "non", 0, "non", "non", "non", 0, false, 0);
    var url = "http://localhost:58685/api/values/";
    var thisresponse;
    var awaiter = await axios.post(url,powstdata)
    .then((res) => {
        thisresponse = res;
        console.log(res)
    }).catch((error) => {
        console.error(error)
    });
    //needs the correct data to update modal. which data is the correct data remains a mystery.
    var block = testblock.testblockdonotupvote(thisresponse, viewid, responseurl);
    var parsedBlock = JSON.parse(block);
    var nextresponse = await web.chat.update(parsedBlock);// this does not currently work. updating modals is strange sorcery.
        
    } catch (e) {
        console.log(e);
    }
});
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


// Handle errors (see `errorCodes` export)
slackEvents.on('error', console.error);

// Starts express server
http.createServer(app).listen(port, () => {
    console.log(`server listening on port ${port}`);
});