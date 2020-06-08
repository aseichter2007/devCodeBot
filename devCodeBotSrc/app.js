require('dotenv').config();

const express = require('express');
const http = require('http');
const app = express();
const bodyParser = require('body-parser');
const port = process.env.PORT;

const { createEventAdapter } = require('@slack/events-api');
const slackEvents = createEventAdapter(process.env.SLACK_SIGNING_SECRET);
const { WebClient } = require('@slack/web-api');
const token = process.env.SLACK_BOT_TOKEN;
const web = new WebClient(token);

app.use('/slack/events', slackEvents.expressMiddleware());
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

slackEvents.on('message', (message, body) => {
    // Respond to the reaction back with the same emoji
    (async () => {
      try {
        // Respond to the message back in the same channel
        if (message.text == 'Help') {
            const response = await web.chat.postMessage({ channel: message.channel, text: 'Sup' });
        }
      } catch (error) {
        console.log(error.data);
      }
    })();
  });

// Handle errors (see `errorCodes` export)
slackEvents.on('error', console.error);

// Starts server
http.createServer(app).listen(port, () => {
    console.log(`server listening on port ${port}`);
});