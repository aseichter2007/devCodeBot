const tonyTestStart = (message) => {
	var block = //NOTE: The application is not going to run with the comments here, FYI
	`{
        "channel": "${message.channel}", ${/*Channel and Blocks or Text are required in the postMessage method; see https://api.slack.com/methods/chat.postMessage */ null}
        "blocks": [
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "Some text to tell the user to click the button" ${/*Some text to tell the user to do click the button; this is required to launch the modal */ null}
                },
                "accessory": {
                    "type": "button",
                    "text": {
                        "type": "plain_text",
                        "text": "Create Question Card",
                        "emoji": true
                    },
                    "value": "click_me_123"
                }
            }
        ],
        "action-id": "tonyTestModalLaunch" ${/*This is the action that occurs when the button is clicked; see app.js */ null}
    }`;
	return block; // returns the above JSON string literal
}

exports.tonyTestStart = tonyTestStart;