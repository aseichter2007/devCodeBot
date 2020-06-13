function buildmyjson(role, username, request, type, search, id, name, oldname, match, matchid, set, day ){

var request = {"search": {
    "role": role,
    "username":username,
    "request": {
        "type": request,
        "day": day,
        "search": search,       
    },
    "add": {
        "type": type,
        "id": id,
        "name": name,
        "matchTo": match,
        "conceptKey": matchid,
        "day": day
    },
    "edit": { 
        "type": type,
        "id": id,
        "originalname": oldname,
        "newname": name,
        "matchto": match,
        "conceptKey": matchid,
        "day": day
    },
    "remove": {
        "type": type,
        "id": id,
        "name": name       
    },
    "setting":{
        "id": id,
        "set": set
    }
}}    

return request;
    
}

exports.buildmyjson = buildmyjson;