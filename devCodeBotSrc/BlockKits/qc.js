const questionCard = () => {
	var block = 
	`{
        "title": {
            "type": "plain_text",
            "text": "Question Card"
        },
        "submit": {
            "type": "plain_text",
            "text": "Submit"
        },
        "blocks": [
            {
                "type": "input",
                "element": {
                    "type": "plain_text_input",
                    "multiline": true
                },
                "label": {
                    "type": "plain_text",
                    "text": "What is the task you are trying to accomplish? What is the goal?",
                    "emoji": true
                }
            },
            {
                "type": "input",
                "element": {
                    "type": "plain_text_input",
                    "multiline": true
                },
                "label": {
                    "type": "plain_text",
                    "text": "What do you think the problem or impediment is?",
                    "emoji": true
                }
            },
            {
                "type": "input",
                "element": {
                    "type": "plain_text_input",
                    "multiline": true
                },
                "label": {
                    "type": "plain_text",
                    "text": "What have you specifically tried in your code?",
                    "emoji": true
                }
            }
        ],
        "type": "modal"
    }`;
	return block;
}

exports.questionCard = questionCard;