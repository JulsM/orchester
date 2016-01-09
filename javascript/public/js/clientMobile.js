$(document).ready(function(){
    // WebSocket
    socket = io.connect();
    initMobile();

    // fired upon successful connection
    socket.on('connect', function() {
        console.log('connection successful');
        socketConnect = true;
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

    socket.on('screen disconnect', function(data) {
        console.log('screen got disconnected');
    });
    
    
});
var socketConnect = false;
function initMobile() {
    $('#playBtn').click(function() {
        if($('#nameInput').val().length > 0) {
            var fullscreenEnabled = document.fullscreenEnabled || document.mozFullScreenEnabled || document.webkitFullscreenEnabled;
            if(fullscreenEnabled) {
                // toggleFullscreen();
            }
            $('#mobileOverlay').hide();
            connectMobile($('#nameInput').val());
        }

    });
    $('#nameInput').focus( function() {
        $(this).select();

    });

    // Events
    document.addEventListener("fullscreenchange", function(e) {
        leaveFullscreen();
    });
    document.addEventListener("mozfullscreenchange", function(e) {
        leaveFullscreen();
    });
    document.addEventListener("webkitfullscreenchange", function(e) {
        leaveFullscreen();
    });
    document.addEventListener("msfullscreenchange", function(e) {
        leaveFullscreen();
    });

    $('#btnWrapper').children().click(function() {
        $('#btnWrapper').children().css('border', '1px solid #333');
        $(this).css('border', '2px solid #fff');
        player.color = $(this).css('background-color');
        player.instrument = $(this).data('instr');
        drawCircle();
    })
}


function toggleFullscreen() {
    var element = document.documentElement;
    var fullscreen = document.fullscreenElement ||document.webkitFullscreenElement ||document.mozFullScreenElement ||document.msFullscreenElement;
    
    if (fullscreen === undefined) {
        if(element.requestFullscreen) {
            element.requestFullscreen();
        } else if(element.mozRequestFullScreen) {
            element.mozRequestFullScreen();
        } else if(element.webkitRequestFullscreen) {
            element.webkitRequestFullscreen();
        } else if(element.msRequestFullscreen) {
            element.msRequestFullscreen();
        }
    } else {
        if(document.exitFullscreen) {
            document.exitFullscreen();
        } else if(document.mozCancelFullScreen) {
            document.mozCancelFullScreen();
        } else if(document.webkitExitFullscreen) {
            document.webkitExitFullscreen();
        }
        $('#playBtn').show();
    }
    
}

function leaveFullscreen() {
    var fullscreen = document.fullscreenElement ||document.webkitFullscreenElement ||document.mozFullScreenElement ||document.msFullscreenElement;
    
    if (fullscreen === undefined) {
        $('#mobileOverlay').show();
    }
}


function connectMobile(name) {
    if(socketConnect) {
        socket.emit('connect mobile', { "player": socket.id, "name": name}, function(data){
            if(data.connected){
                console.log('player: '+data.player+' connected');
                if(player != 'undefined') {
                    player.id = socket.id;
                }
            }else{
                console.log('player not connected. '+data.error);
            }
        });
    } else {
        setTimeout(connectMobile(name), 5000);
    }
}



