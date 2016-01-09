using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Orchestra : MonoBehaviour {

	public List<Player> PlayerList { get; set;}
	static int numberNotes= 10;
	private Conductor conductor;
	private DrawOnScreen dos;

	void Awake () {
		PlayerList = new List<Player> ();
	}

	// Use this for initialization
	void Start () {
		
		conductor = gameObject.GetComponent<Conductor> ();
		dos = gameObject.GetComponent<DrawOnScreen> ();
	}
	
	// Update is called once per frame
	void Update () {
		System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew(); 

		List <Player> sortedList = PlayerList.OrderByDescending (p => p.CurrentlyPlaying).ToList(); //sort playerlist that all currently playing players come first

		Dictionary<int, int[]> nextPlay = new Dictionary<int, int[]>(); //array with instruments and notes which get played in the next frame
		int totalPlaying = 0;

		foreach (Player p in sortedList) {
			
			if(p.CurrentlyPlaying) {
				totalPlaying++;
				int[] noteArray;
				if (nextPlay.TryGetValue(p.Instrument, out noteArray)){
					nextPlay [p.Instrument][p.Note]++; // increment number of players already playing this note
				} else {
					int[] noteArr = new int[numberNotes]; // create new note array
					noteArr[p.Note] = 1;
					nextPlay [p.Instrument] = noteArr;
				}
			} else {
				break; // done with all currently playing players
			}

		}

		conductor.crossfadeSounds (nextPlay, totalPlaying); // crossfade from tempPlaying to nextPlay
		stopwatch.Stop();
		//		Debug.Log ("timer: " + stopwatch.ElapsedMilliseconds);
	}
}
