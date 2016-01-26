using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class Orchestra : MonoBehaviour {

	static int numberNotes= 8;
	private Conductor conductor;
	private DrawOnScreen dos;
	public Dictionary<Player, PlayerSprite> PlayerDict { get; set;}

	void Awake() {
		PlayerDict = new Dictionary<Player, PlayerSprite>();
//        PlayerDict.Add(new Player("tim", "timy"), new PlayerSprite(new Player("tim", "timy")));
	}



    // Use this for initialization
    void Start () {
		conductor = gameObject.GetComponent<Conductor> ();
		dos = gameObject.GetComponent<DrawOnScreen> ();
    }
	
	// Update is called once per frame
	void Update () {
		System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

//        List<Player> sortedList = playerToSprite.Keys.ToList().OrderByDescending(p => p.CurrentlyPlaying).ToList(); //sort playerlist that all currently playing players come first;

        Dictionary<int, int[]> nextPlay = new Dictionary<int, int[]>(); //array with instruments and notes which get played in the next frame
		int totalPlaying = 0;

        foreach (KeyValuePair<Player, PlayerSprite> entry in PlayerDict)
        {
            Player p = entry.Key;
            if (p.CurrentlyPlaying)
            {
                entry.Value.effectOn();
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
                entry.Value.effectOff();
            }
            dos.draw(entry.Key, entry.Value);
        }


		conductor.crossfadeSounds (nextPlay, totalPlaying); // crossfade from tempPlaying to nextPlay
		stopwatch.Stop();
//		Debug.Log ("timer: " + stopwatch.ElapsedMilliseconds);
//		Debug.Log ("fps: " + 1.0/Time.deltaTime);
	}
}
