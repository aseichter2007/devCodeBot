const ManageActionSelect = (trigger, caller) =>{/* unsure how the return will work, need to track user to return second modal */
    var block =
`{ 
    "private_metadata": "${caller}",
    "trigger_id": "${trigger}",
    "view":{
        "type": "modal",
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
                "block_id": "selectAction",
                "element": {
                    "type": "static_select",
                    "action_id": "manageactionselected",
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
                                "text": "*View and manage active project.*",
                                "emoji": true
                            },
                            "value": "activeproject"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage preferred language.*",
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
                                "text": "*View and manage platforms.*",
                                "emoji": true
                            },
                            "value": "platforms"
                        },
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "*View and manage languages.",
                                "emoji": true
                            },
                            "value": "languages"
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
    }
}`
return block;
}

exports.ManageActionSelect = ManageActionSelect;