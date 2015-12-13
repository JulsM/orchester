var canvas = document.getElementById("mobileCanvas");
var context = canvas.getContext("2d");

$(document).ready(function(){    
    initCanvas();

});
    

function initCanvas() {
    $(window).resize(respondCanvas);
    respondCanvas();
    drawLines();

    canvas.addEventListener('touchmove', updateCirclePosition);
    canvas.addEventListener('touchend', interactionEnd);
    canvas.addEventListener('touchstart', updateCirclePosition);
   
}

/*
* Make canvas responsive 
*/
function respondCanvas(){ 
    var container = $(canvas).parent();
    $(canvas).attr('width', $(container).width() ); //max width
    $(canvas).attr('height', $(container).height() ); //max height

    redrawCanvas();
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
    var verticalSpace = Math.round(canvas.height / (config.numLines + 1));
    for (var i = 0; i < config.numLines; i++) {
        context.moveTo(0, verticalSpace);
        context.lineTo(canvas.width, verticalSpace);
        verticalSpace += verticalSpace;
    }
	context.stroke();
}

function updateCirclePosition(e) {
    e.preventDefault();
    var touch = e.targetTouches[0];
	var pos = getMousePos(touch);
	context.clearRect(0, 0, canvas.width, canvas.height);
	drawLines();
	context.fillStyle = "#ffcc00";
    context.beginPath();
    context.arc(canvas.width/2, pos.y, 10, 0, 2*Math.PI);
    context.fill();

    var percentX = 100 * (pos.x) / canvas.width;
    var percentY = 100 * (pos.y) / canvas.height;
    socket.emit('update position', { "player": {"id": socket.id, "x": percentX, "y": percentY }});
    
}

function interactionEnd(e) {
    e.preventDefault();
    socket.emit('stop interaction', {user: socket.id});
}
