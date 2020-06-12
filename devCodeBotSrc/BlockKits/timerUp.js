const timerUp = (message, time) => {
	var block = 
	`{
        "channel": "${message.channel}",
        "blocks": [
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "30 minutes have passed! :clock1: Would you like to submit a question card to the instructors?"
                },
                "accessory": {
                    "type": "button",
                    "text": {
                        "type": "plain_text",
                        "text": "Create question card",
                        "emoji": true
                    },
                    "value": "click_me_123"
                }
            }
        ],
        "action-id": "launchQuestionCardModal",
        "post_at": "${time}"
    }`;
	return block;
}

exports.timerUp = timerUp;