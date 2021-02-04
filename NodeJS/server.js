var websocket = require('ws');

var callbackInitServer = ()=>{
    console.log("Richelle Server operational.");
}

var wss = new websocket.Server({port:5500}, callbackInitServer);

var wsList = [];

wss.on("connection", (ws)=>{

    console.log("Client connected.");
    wsList.push(ws);

    ws.on("message", (data)=>{
        console.log("send from client : " + data);
        Broadcast(data);
    }); // Broadcast to all

    ws.on("close", ()=>{
        console.log("Client disconnected.");
        wsList = ArrayRemove(wsList, ws);
    });
});

function ArrayRemove(arr, value)
{
    return arr.filter((element)=>{
        return element != value;
    });
}

function Broadcast(data)
{
    for(var i = 0; i < wsList.length; i++)
    {
        wsList[i].send(data);
    }
}

