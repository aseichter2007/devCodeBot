const questionCard = (trigger) => {
	var block = 
	`{
        "trigger_id": "${trigger}",
        "view": {
            "callback_id": "questionCardSubmit",
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
                    "block_id": "myGoalBlock",
                    "element": {
                        "type": "plain_text_input",
                        "action_id": "myGoalResponse",
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
                    "block_id": "myProblemBlock",
                    "element": {
                        "type": "plain_text_input",
                        "action_id": "myProblemResponse",
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
                    "block_id": "myAttemptsBlock",
                    "element": {
                        "type": "plain_text_input",
                        "action_id": "myAttemptsResponse",
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
        }
    }`;
	return block;
}

exports.questionCard = questionCard;