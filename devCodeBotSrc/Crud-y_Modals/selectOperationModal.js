const ManageActionSelect = (trigger, caller) =>{/* unsure how the return will work, need to track user to return second modal */
    var block =
`{ 
    "private_metadata": "${caller}",
    "trigger_id": "${trigger}",
    "view":{
        "callback_id": "manageactionselect",
        "title": {
            "type": "plain_text",
            "text": "devCodeBot",
            "emoji": true
        },
        "submit": {
            "type": "plain_text",
            "text": "Submit",
            "emoji": true
        },
        "close": {
            "type": "plain_text",
            "text": "Cancel",
            "emoji": true
        },
        "blocks": [
            {
                "type": "input",
                "element": {
                    "type": "static_select",
                    "block_id": "selectAction",
                    "placeholder": {
                        "type": "plain_text",
                        "text": "Select an action",
                        "emoji": true                        
                    },
                    "options": [
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*view raw search data.*",
                                "emoji": true
                            },
                            "value": "rawsearch"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage active project. ex: 'asp.net MVC' *",
                                "emoji": true
                            },
                            "value": "activeproject"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage preferred language. ex: 'c#', 'javascript'*",
                                "emoji": true
                            },
                            "value": "preferredlanguage"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage filtered words*",
                                "emoji": true
                            },
                            "value": "badwords"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage filtered phrases*",
                                "emoji": true
                            },
                            "value": "badphrases"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage linked concepts*",
                                "emoji": true
                            },
                            "value": "nearconcepts"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage platforms. ex: '.net', 'asp.net MVC'*",
                                "emoji": true
                            },
                            "value": "platforms"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage languages. ex: 'c#'*",
                                "emoji": true
                            },
                            "value": "value-7"
                        }
                    ]
                },
                "label": {
                    "type": "plain_text",
                    "text": "Manage devCodeBot",
                    "emoji": true
                }
            }
        ]
        "type": "modal",
    }
}`
return block;
}

exports.ManageActionSelect = ManageActionSelect;