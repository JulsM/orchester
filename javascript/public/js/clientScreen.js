$(document).ready(function(){
    // WebSocket
    socket = io.connect();
    
    // fired upon successful connection
    socket.on('connect', function() {
        console.log('connection successful');
        var roomId = "test";
        socket.emit('new room', { room: roomId});
    });

    socket.on('add user', function(user){
        // players.push(new player(socketID));
        
        // $('#log').append('<li>new player '+user+'</li>');
        players.push(new Player(user));
        console.log('player array length '+ players.length);
    });

    socket.on('remove user', function(user){
        // $('#log').append('<li>remove user '+user+'</li>');
      
        for(i = 0; i < players.length; i++){
            if(players[i].id == user){
              players.splice(i, 1);
              console.log('remove user ' + user +', players length '+players.length);
              break;
            }
        }
    });

    //Fired upon a connection error
    socket.on('error', function(errorData) {
        console.log('connection error');
    });

    //Fired upon a disconnection
    socket.on('disconnect', function() {
        console.log('disconnected');
    });

    //Fired upon a disconnection
    socket.on('reconnect', function(numAttempts) {
        console.log('reconnected, attempts: '+ numAttempts);
    });

    socket.on("update position", function(data){
        for(i = 0; i < players.length; i++) {
            if(players[i].id == data.user){
                players[i].update(data.x, data.y);
                players[i].playSound();
                break;
            }
        }
    });

    socket.on("stop interaction", function(data){
        for(i = 0; i < players.length; i++) {
            if(players[i].id == data.user){
                players[i].stop();
                break;
            }
        }
    });

});

var players = [];
var FPS = 30
function Player(user){
    this.id = user;
    this.x = canvas.width/2;
    this.y = canvas.height/2;
    this.color = getRandomColor();
    this.gains = [];
    var context;

    try {
        window.AudioContext = window.AudioContext||window.webkitAudioContext;
        context = new AudioContext();
    } catch(e) {
        alert('Web Audio API is not supported in this browser');
    }
    

    for(i = 0; i < config.soundNames.length; i++) {
        
        var gain = context.createGain();
        gain.gain.value = 0;
        gain.connect(context.destination);
        var audio = new Audio();
        audio.src= config.soundNames[i];
        var source = context.createMediaElementSource(audio);
        source.connect(gain);
        audio.loop = true;
        audio.play();
        this.gains.push(gain);
        
    }
    
    this.currentSound = 1;

    this.update = function(x, y){
        this.x = x;
        this.y = y;
    } 
    this.stop = function() {
        this.gains[this.currentSound].gain.value = 0;
        
    }
    this.playSound = function() {
        if(this.y <= 33 && this.currentSound != 2) {
            this.stop();
            this.currentSound = 2;
        } else if(this.y <= 66 && this.y > 33 && this.currentSound != 1) {
            this.stop();
            this.currentSound = 1;
        }else if (this.y > 66 && this.currentSound != 0){
            this.stop();
            this.currentSound = 0;
        }
        
        this.gains[this.currentSound].gain.value = 1;
        
    }

    // this.crossFadeSounds = function (a, b) {
    //   var currentTime = context.currentTime,
    //       fadeTime = 2; // 3 seconds fade time

    //   // fade out
    //   this.gains[a].gain.linearRampToValueAtTime(1, currentTime);
    //   this.gains[a].gain.linearRampToValueAtTime(0, currentTime + fadeTime);

    //   // fade in
    //   this.gains[b].gain.linearRampToValueAtTime(0, currentTime);
    //   this.gains[b].gain.linearRampToValueAtTime(1, currentTime + fadeTime);
    // }

    return this;
}

setInterval(function() {
    updateCirclePosition(players);
}, 1000/FPS);

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++ ) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

