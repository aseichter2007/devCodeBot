const questionCardPost = (goal, problem, attempts, user, channel) => {
	var block = 
	`{
        "channel": "${channel}",
        "blocks": [
            {
                "type": "section",
                "text": {
                    "type": "plain_text",
                    "text": "Question card submitted by: ${user}",
                    "emoji": true
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "*What is the task you are trying to accomplish? What is the goal?*"
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "plain_text",
                    "text": "${goal}",
                    "emoji": true
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "*What do you think the problem or impediment is?*"
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "plain_text",
                    "text": "${problem}",
                    "emoji": true
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": "*What have you specifically tried in your code?*"
                }
            },
            {
                "type": "section",
                "text": {
                    "type": "plain_text",
                    "text": "${attempts}",
                    "emoji": true
                }
            }
        ]
    }`;
	return block;
}

exports.questionCardPost = questionCardPost;