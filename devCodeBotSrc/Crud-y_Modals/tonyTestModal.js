const tonyTestModal = (/*Your parameters*/) => {
	var block = // "view", "trigger_id", and "type": "modal" are requried. See qc.js for an example.
	`{
        "trigger_id": "${trigger}", ${/* trigger value is required; this directs that a button click is tied to a specific user in a specific context*/ null}
        "view": {
            "callback_id": "questionCardSubmit", ${/* callback_id is used to handle what the modal should do after it is submitted; see app.js for an example*/ null}
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
                    "block_id": "myGoalBlock", ${/* each block requries a block_id to access the input value */ null}
                    "element": {
                        "type": "plain_text_input",
                        "action_id": "myGoalResponse", ${/* each input requires a action_id to access the input.value */ null}
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
            "type": "modal" ${/* required to launch this message as a modal*/ null}
        }
    }`;
	return block;
}

exports.tonyTestModal = tonyTestModal;