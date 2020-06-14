const DbAcess = (message) => {
	var block = 
	`{
        "channel": "${message.channel}",
        "blocks": [
            {
                "type": "section",
                "block_id": "manage",
                "text": {
                    "type": "mrkdwn",
                    "text": "Would you like to manage the database?"
                },
                "accessory": {
                    "type": "button",
                    "text": {
                        "type": "plain_text",
                        "text": "Manage Database",
                        "emoji": true
                    },
                    "value": "click_me_123"
                }
            }
        ],
        "action_id": "selectoperation"
    }`;
	return block;
}

exports.DbAcess = DbAcess;