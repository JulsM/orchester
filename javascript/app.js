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

var room = new Room();
function Room(){
    // do we have an existing instance?
    if (typeof Room.instance === 'object') {
        console.log('room already exists');
        var r = Room.instance;
        return r;
    }
    this.screenSocket = null;  //Stores the socket for the desktop connection
    this.mobileSockets = [];       //A list of all the mobile connections

    // cache
    Room.instance = this;
    console.log('new room ');
    return this;
}

io.sockets.on('connection', function (socket) {

    socket.on("unity connect", function(data, fn){
        if(room != null) {
            room.screenSocket = socket;
            var json = {"connected": true, "players": [], "names": []};
            for (var i = 0; i < room.mobileSockets.length; i++) {
                json.players.push(room.mobileSockets[i].player);
                json.names.push(room.mobileSockets[i].name);
            }
            
        } else {
            var json = {"connected": false};
        }
        console.log(json);
        fn(json);
    });

    socket.on("connect mobile", function(data, fn){
        if(room !== null){
            socket.player = data.player;
            socket.name = data.name;
            room.mobileSockets.push(socket);
            console.log('mobileSockets length '+room.mobileSockets.length);
            fn({connected: true, player: data.player});
            if(room.screenSocket != null) {
                room.screenSocket.emit('add player', data);
            }
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
                    if(room.screenSocket != null) {
                        room.screenSocket.emit('remove player', {"player": socket.player});
                    }
                    break;
                }
            }
        }
    });

    socket.on("update position", function(data){
        var player = data.player;
        // console.log(data);
        var note = Math.floor(player.y * conf.notes / 100);
        // console.log(note);
        if(room.screenSocket != null) { 
            
            room.screenSocket.emit('update player', { "player": {"id": player.id, "y": player.y, "instrument": player.instrument, "note":  note}});
        }
    });

    socket.on("stop interaction", function(data){
        if(room.screenSocket != null) {
            room.screenSocket.emit('stop interaction', data);
        }
        
    });

});



// Portnummer in die Konsole schreiben
console.log('Der Server läuft nun unter localhost:' + conf.port + '/');
