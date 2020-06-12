const greeting = (message) => {
	var block = 
	`{
		"channel": "${message.channel}",
		"blocks": [
			{
				"type": "section",
				"text": {
					"type": "mrkdwn",
					"text": "Hey! :wave: I'm *devCodeBot*, and I'm here to help you work your way through problems. Type any of the following for me to help:"
				}
			},
			{
				"type": "divider"
			},
			{
				"type": "section",
				"text": {
					"type": "mrkdwn",
					"text": "\`timebox\`: this will set a 30-minute timer to keep you in check while you're troubleshooting"
				}
			},
			{
				"type": "section",
				"text": {
					"type": "mrkdwn",
					"text": "\`question card\`: this will create a form that you can fill out to request help from your instructors"
				}
			},
			{
				"type": "section",
				"text": {
					"type": "mrkdwn",
					"text": "\`cancel timebox\`: use this to cancel a currently active timebox timer"
				}
			},
			{
				"type": "divider"
			},
			{
				"type": "section",
				"text": {
					"type": "mrkdwn",
					"text": "If you want me to help you look something up, just ask me a question!"
				}
			}
		]
	}`;
	return block;
}

exports.greeting = greeting;