﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Orchestra : MonoBehaviour {

	static int numberNotes= 8; // change here if you add or delete the number of notes, needed for the size of your note array
	private Conductor conductor; // reference to conductor class
	private DrawOnScreen dos; // reference to DrawOnScreen class
	public Dictionary<Player, PlayerSprite> PlayerDict { get; set;} // the main list of all current players, with their PlayerSprite object
    float offset; //if players are near the same Y-Position, they get an offset in X-Direction to distinct between them


    void Awake() {
		PlayerDict = new Dictionary<Player, PlayerSprite>();
    }


    void Start () {
		conductor = gameObject.GetComponent<Conductor> ();
		dos = gameObject.GetComponent<DrawOnScreen> ();
    }
	
	// Main Update method that handels the visuals and sounds frame by frame
	void Update () {
		
        Dictionary<int, int[]> nextPlay = new Dictionary<int, int[]>(); //array with instruments and notes which get played in the next frame
		int totalPlaying = 0;

        foreach (KeyValuePair<Player, PlayerSprite> entry in PlayerDict)
        {
           
            Player p = entry.Key;
            if (p.CurrentlyPlaying)
            {
                entry.Value.effectOn(); // turn particle on
                totalPlaying++;
                int[] noteArray;
                if (nextPlay.TryGetValue(p.Instrument, out noteArray))
                {
                    nextPlay[p.Instrument][p.Note]++; // increment number of players already playing this note
                }
                else
                {
                    int[] noteArr = new int[numberNotes]; // create new note array
                    noteArr[p.Note] = 1;
                    nextPlay[p.Instrument] = noteArr;
                }
            }
            else
            {
                entry.Value.effectOff(); // turn particle off
            }

            offset = 0F;
            foreach(Player otherPlayer in PlayerDict.Keys) // check if other player is on same hight and calculate offset
            {
                if (otherPlayer.Id != p.Id)
                {
                    float distance = Mathf.abs(p.Y - otherPlayer.Y);//distance to otherPlayer
                    if (distance < 2)
                    {
                        offset += distance - 2;
                    }
                }
            }

            dos.draw(entry.Key, entry.Value, offset); // update visual position of each player
        }


		conductor.crossfadeSounds (nextPlay, totalPlaying); // crossfade from last played sounds to the sounds to be played next
	}
}
