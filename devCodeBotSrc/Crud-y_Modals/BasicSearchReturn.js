
function BasicSearch(message, ogmessage) {
             
    var search = message.data.searches;
    var search1 = Queryformatter(search[0]);
    var search2 = Queryformatter(search[1]);
    var search3 = Queryformatter(search[2]);
    var search4 = Queryformatter(search[3]);
    var block = SearchReurn(search1,search2,search3,search4, ogmessage);
    var parsedBlock = JSON.parse(block);
    return parsedBlock;
}

function SearchReurn(search1, search2, search3, search4,  message) {
    var block =
`{
    "channel": "`+message.channel+`",

	"blocks": [
		{
			"type": "section",
			"text": {
				"type": "plain_text",
				"text": "Try one of these google search links:",
				"emoji": true
			}
		},
		{
			"type": "divider"
		},
		{
			"type": "section",
			"text": {
				"type": "mrkdwn",
				"text": "<`+search1+`|`+search1+`>"
			}
		},
		{
			"type": "divider"
        },
        {
			"type": "section",
			"text": {
				"type": "mrkdwn",
				"text": "<`+search2+`|`+search2+`>"
			}
		},
		{
			"type": "divider"
        },
        {
			"type": "section",
			"text": {
				"type": "mrkdwn",
				"text": "<`+search3+`|`+search3+`>"
			}
		},
		{
			"type": "divider"
        },
        {
			"type": "section",
			"text": {
				"type": "mrkdwn",
				"text": "<`+search4+`|`+search4+`>"
			}
		},
		{
			"type": "divider"
		}
	]
}`;
return block;
}
function Queryformatter(params) {
    var base = "https://www.google.com/search?q="
    var search = params.split(' ').join('+');
    var output = base + search +"%";
    return output;
}
function SearchApiFormatter(message) {
    var message = message.split(' ').join('_');
    return message;
}
exports.SearchApiFormatter = SearchApiFormatter;
exports.BasicSearch = BasicSearch;
