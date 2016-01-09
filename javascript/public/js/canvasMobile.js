var canvas = document.getElementById("mobileCanvas");
var context = canvas.getContext("2d");
var player;

$(document).ready(function(){    
    initCanvas();

});

function Player(){
    // do we have an existing instance?
    if (typeof Player.instance === 'object') {
        console.log('player already exists');
        return Player.instance;
    }
    this.id;
    this.y = canvas.height/2;
    this.color = "#00ff00";
    this.instrument = 0;
    // cache
    Player.instance = this;
    return this;
}
    

function initCanvas() {
    $(window).resize(function() {respondCanvas(); drawCircle();});
    respondCanvas();
    player = new Player();
    drawCircle();

    canvas.addEventListener('touchmove', updatePosition);
    canvas.addEventListener('touchend', interactionEnd);
    canvas.addEventListener('touchstart', updatePosition);
//    setInterval(function() {
//      var percentY = 100 * (player.y) / canvas.height;
//     socket.emit('update position', { "player": {"id": socket.id, "y": percentY, "instrument": player.instrument }});
// }, 1000);
}

/*
* Make canvas responsive 
*/
function respondCanvas(){ 
    var container = $(canvas).parent();
    $(canvas).attr('width', $(container).width() ); //max width
    $(canvas).attr('height', $(container).height() ); //max height
}

/*
* Clear canvas and redraw lines 
*/
function redrawCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height);
    drawLines();
}

/*
* Clear canvas and redraw lines 
*/
function getMousePos(evt) {
    var rect = canvas.getBoundingClientRect();
    return {
      x: Math.round(evt.clientX - rect.left),
      y: Math.round(evt.clientY - rect.top)
    };
}

function drawLines() {
    context.beginPath();
	context.strokeStyle = "#fff";
    var offset = Math.round(canvas.height / (config.numLines + 1));
    var verticalSpace = offset;
    for (var i = 0; i < config.numLines; i++) {
        context.moveTo(0, verticalSpace);
        context.lineTo(canvas.width, verticalSpace);
        verticalSpace += offset;
    }
	context.stroke();
}

function updatePosition(e) {
    e.preventDefault();
    var touch = e.targetTouches[0];
	var pos = getMousePos(touch);
    player.y = pos.y;
    drawCircle();
    emitPlayerUpdate();
}

function drawCircle() {
    
    context.clearRect(0, 0, canvas.width, canvas.height);
    drawLines();
    context.fillStyle = player.color;
    context.beginPath();
    context.arc(canvas.width/2, player.y, 12, 0, 2*Math.PI);
    context.fill();
}

function interactionEnd(e) {
    e.preventDefault();
    socket.emit('stop interaction', {"player": socket.id});
}


function emitPlayerUpdate() {
    var percentY = Math.round(10000 * (player.y) / canvas.height) / 100;
    socket.emit('update position', { "player": {"id": socket.id, "y": percentY, "instrument": player.instrument }});
}