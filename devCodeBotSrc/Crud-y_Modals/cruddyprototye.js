function testblockdonotupvote(data, trigger){
    testdata = data;

    blocks=[];
    testdata.map(d=> {
        blocks.push(
        `{
            "text": {
                "type": "plain_text",
                "text": "`+d+`",
                "emoji": true
            },
            "value": "value-0"
        }`);

    });
    blockmiddle = blocks.join();
                        
                        

blockstart = 
`{
    "trigger_id": "${trigger}",
    "view": 
        {
        "type": "modal",
        "title": {
            "type": "plain_text",
            "text": "My App",
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
                "type": "divider"
            },
            {
                "type": "input",
                "element": {
                    "type": "static_select",
                    "placeholder": {
                        "type": "plain_text",
                        "text": "Select an item",
                        "emoji": true
                    },
                    "options": [`;
                    
                        blockend =
                    `]
                },
                "label": {
                    "type": "plain_text",
                    "text": "Label",
                    "emoji": true
                }
            }
		]
	}
}`;
return blockstart+blockmiddle+blockend;

}
exports.testblockdonotupvote = testblockdonotupvote;