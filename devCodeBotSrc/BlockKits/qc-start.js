const questionCardStart = (message) => {
	var block = 
	`{
        "channel": "${message.channel}",
        "blocks": [
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "Click this button to fill out a new question card"
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
        "action-id": "launchQuestionCardModal"
    }`;
	return block;
}

exports.questionCardStart = questionCardStart;