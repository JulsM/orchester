var express = require('express')
,   app = express()
,   server = require('http').createServer(app)
,   io = require('socket.io').listen(server)
,   conf = require('./config.json');

server.listen(conf.port);

// statische Dateien ausliefern
app.use(express.static(__dirname + '/public'));

// wenn der Pfad / aufgerufen wird
app.get('/', function (req, res) {
    // so wird die Datei index.html ausgegeben
    res.sendFile(__dirname + '/public/mobile.html');
});

var room = null;
function Room(screenSocket, roomId){
    // do we have an existing instance?
    if (typeof Room.instance === 'object') {
        console.log('room already exists');
        var r = Room.instance;
        r.screenSocket = screenSocket;
        return r;
    }
    this.screenSocket = screenSocket;  //Stores the socket for the desktop connection
    this.roomId = roomId;          //The room id/name. A unique string that links desktop to mobile
    this.mobileSockets = [];       //A list of all the mobile connections

    // cache
    Room.instance = this;
    console.log('new room ' + this.roomId);
    return this;
}

io.sockets.on('connection', function (socket) {

    socket.on("new room", function(data, fn){
        console.log(socket.id);
        room = new Room(socket, data.room);
        var json = {"players": []};
        for (var i = 0; i < room.mobileSockets.length; i++) {
            json.players.push(room.mobileSockets[i].player);
        }
        console.log(json);
        fn(json);
    });

    socket.on("connect mobile", function(data, fn){
        if(room !== null){
            socket.player = data.player;
            room.mobileSockets.push(socket);
            console.log('mobileSockets length '+room.mobileSockets.length);
            fn({connected: true, player: data.player});
            room.screenSocket.emit('add player', data);
        }else{
            fn({connected: false, error: "No live desktop connection found"});
        }
    });

  
    socket.on('disconnect', function () {
        if(typeof socket.player == 'undefined') {
            console.log('screen connection lost');
            if(room !== null) {
                for(i = 0; i < room.mobileSockets.length; i++) {
                    room.mobileSockets[i].emit('screen disconnect');
                }
            }
        } else {
            console.log('mobile connection lost');
            for(i = 0; i < room.mobileSockets.length; i++) {
                if(room.mobileSockets[i].id == socket.id) {
                    console.log('remove socket');
                    room.mobileSockets.splice(i, 1);
                    room.screenSocket.emit('remove player', {"player": socket.player});
                    break;
                }
            }
        }
    });

    socket.on("update position", function(data){
        room.screenSocket.emit('update position', data);
    });

    socket.on("stop interaction", function(data){
        room.screenSocket.emit('stop interaction', data);
    });

});



// Portnummer in die Konsole schreiben
console.log('Der Server läuft nun unter localhost:' + conf.port + '/');
