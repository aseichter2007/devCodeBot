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
					"text": "\`lookup\`: follow this with a search query and I'll get you some suggested Google search results"
				}
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
			}
		]
	}`;
	return block;
}

exports.greeting = greeting;