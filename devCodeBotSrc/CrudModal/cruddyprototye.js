function testblockdonotupvote(data, trigger){
    var datatype = data.data.responseType;
    var viewdata;
    var labelcontent;
    blocks=[];
    switch (datatype) {
        case "rawsearches":
            viewdata = data.data.rawSearches;//this should be fancier but this is all time allows for
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.studentName+" "+d.Search+`",
                        "emoji": true
                    },
                    "value": "`+d.studentName+`"
                }`);
        
            });
            break;
        case "activeprojects":
            viewdata = data.data.activeProjects; //currently we dont track days, only the first activeproject will append to the search.
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.ProjectType+" day:"+ d.Day+`",
                        "emoji": true
                    },
                    "value": "`+d.ProjectType+`"
                }`);
        
            });
        break;
        case "preferredlanguages":
            viewdata = data.data.preferredLanguages;//currently we dont track days, only the first preferredlanguage will append to the search.
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.LanguageName+" day:"+ d.Day+`",
                        "emoji": true
                    },
                    "value": "`+d.LanguageName+`"
                }`);
        
            });
            break;
        case "badwords":
            viewdata = data.data.badWords;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.word+`",
                        "emoji": true
                    },
                    "value": "`+d.word+`"
                }`);
        
            });
            break;
        case "badphrases":
            viewdata = data.data.badPhrases;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.Phrase+`",
                        "emoji": true
                    },
                    "value": "`+d.Phrase+`"
                }`);
        
            });
            break;
        case "nearconcepts":
            viewdata = data.data.nearConcepts;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.Phrase+" becomes "+d.Properform+`",
                        "emoji": true
                    },
                    "value": "`+"phrase="+d.Phrase+ "=properform="+d.ProperForm+`"
                }`);
        
            });
            break;
        case "languages":
            viewdata = data.data.languages;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.LanguageName+`",
                        "emoji": true
                    },
                    "value": "`+d.LanguageName+`"
                }`);
        
            });
            break;
        case "platforms":
            viewdata = data.data.platforms;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.PlatformName+`",
                        "emoji": true
                    },
                    "value": "`+d.PlatformName+`"
                }`);
        
            });
            break;
        case "settings":
            viewdata = data.data.settings;
            viewdata.map(d=> {
                blocks.push(
                `{
                    "text": {
                        "type": "plain_text",
                        "text": "`+d.SettingName+" set "+d.Set+`",
                        "emoji": true
                    },
                    "value": "`+d.SettingName+`"
                }`);
        
            });
            break;
    
        default:
            console.log("bad input in crudhandler")
            break;
    }   
    
    blockmiddle = blocks.join();
                        
                        

blockstart = 
`{
    "trigger_id": "${trigger}",
    "replace_original": "true",

    "view": 
        {
        "type": "modal",
        "callback_id": "crudout",
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
                "type": "divider"
            },
            {
                "type": "input",
                "element": {
                    "type": "static_select",
                    "placeholder": {
                        "type": "plain_text",
                        "text": "Select an item to edit. select new to create new item.",
                        "emoji": true
                    },
                    "options": [
                        {
                            "text": {
                                "type": "plain_text",
                                "text": "new",
                                "emoji": true
                            },
                            "value": "new"
                        },`;
                    
                        blockend =
                    `]
                },
                "label": {
                    "type": "plain_text",
                    "text": "Create",
                    "emoji": true
                }
            }
		]
	}
}`;
return blockstart+blockmiddle+blockend;

}
exports.testblockdonotupvote = testblockdonotupvote;