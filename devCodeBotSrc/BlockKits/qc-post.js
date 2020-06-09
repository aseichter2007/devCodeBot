const questionCardPost = (goal, problem, attempts, user, channel) => {
	var block = 
	`{
        "channel": "${channel}",
        "blocks": [
            {
                "type": "section",
                "text": {
                    "type": "mrkdwn",
                    "text": ":email: Question card submitted by *${user}*"
                }
            },
            {
                "type": "divider"
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
                    "type": "mrkdwn",
                    "text": ">${goal}"
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
                    "type": "mrkdwn",
                    "text": ">${problem}"
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
                    "type": "mrkdwn",
                    "text": ">${attempts}"
                }
            }
        ]
    }`;
	return block;
}

exports.questionCardPost = questionCardPost;