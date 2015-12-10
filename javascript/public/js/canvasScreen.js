
var canvas = document.getElementById("screenCanvas");
var context = canvas.getContext("2d");



// var sounds = [];

// for(i = 0; i < config.soundNames.length; i++) {
// 	var audioElement = document.createElement('audio');
// 	audioElement.setAttribute('src', config.soundNames[i]);
// 	sounds.push(audioElement);
// }
    
$(document).ready(function(){
    drawLines();
    var beat = document.createElement('audio');
    beat.setAttribute('src', 'beat.wav');
    beat.volume = 0.2;
    beat.loop= true;
    // beat.play();
});

function drawLines() {
    context.beginPath();
	context.strokeStyle = "#fff";
	context.moveTo(0, 166);
	context.lineTo(canvas.width, 166);
	context.moveTo(0, 333);
	context.lineTo(canvas.width, 333);
	context.stroke();
}



// function playSound(player) {
//     var posY = player.y;
//     var playingSound = player.currentSound;
// 	if(posY <= 33 && playingSound.currentSrc != sounds[2].src) {
//         stopSound();
// 		playingSound = sounds[2];
// 	} else if(posY <= 66 && posY > 33 && playingSound.currentSrc != sounds[1].src) {
//         stopSound();
// 		playingSound = sounds[1];
// 	}else if (posY > 66 && playingSound.currentSrc != sounds[0].src){
//         stopSound();
// 		playingSound = sounds[0];
// 	}
//     player.currentSound = playingSound;
// 	playingSound.play();
// 	playingSound.loop = true;
// }

// function stopSound(player) {
// 	player.currentSound.muted = true;
// 	player.currentSound.currentTime = 0;
// }

function updateCirclePosition(players) {

    context.clearRect(0, 0, canvas.width, canvas.height);
    drawLines();
    

    for(i = 0; i < players.length; i++) {
        var player = players[i];
        context.fillStyle = player.color;
        context.beginPath();
        context.arc(canvas.width/2, player.y * canvas.height / 100, 10, 0, 2*Math.PI);
        context.fill();
    }
    
}
  
    



