$(document).ready(function(){
    // WebSocket
    socket = io.connect();
    initMobile();

    // fired upon successful connection
    socket.on('connect', function() {
        console.log('connection successful');
        socket.emit('connect mobile', { user: socket.id}, function(data){
            if(data.connected){
                console.log('user: '+data.user+' connected');
            }else{
                console.log('user not connected. '+data.error);
            }
        });
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

function initMobile() {
    $('#playBtn').click(function() {
        var fullscreenEnabled = document.fullscreenEnabled || document.mozFullScreenEnabled || document.webkitFullscreenEnabled;
        if(fullscreenEnabled) {
            toggleFullscreen();
        }
        $('#mobileOverlay').hide();
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




