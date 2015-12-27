using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using System.Linq;

public class Conductor: MonoBehaviour {


	private AudioMixer master;
	private Dictionary<int, AudioSource[]> soundCollection;

	static Dictionary<string, string[]> soundNames = new Dictionary<string, string[]> {
		{"instrument_a", new string[]{"t_high", "t","t_low" }},
		{"instrument_b", new string[]{"520", "480","440" }}
	};
	static int numberNotes= 3;



	void Start() {
		master = Resources.Load("Master") as AudioMixer;
		soundCollection = new Dictionary<int, AudioSource[]>();
		int i = 0;
		foreach(KeyValuePair<string, string[]> instrument in soundNames){
			
			AudioSource[] sources = new AudioSource[instrument.Value.Length];

			for(int j = 0; j < instrument.Value.Length; j++) {
				string note = instrument.Value [j];
				AudioSource s = gameObject.AddComponent<AudioSource>();
				s.clip = Resources.Load(note) as AudioClip;
				s.outputAudioMixerGroup = master.FindMatchingGroups (instrument.Key) [0];
				sources [j] = s;
			}
			soundCollection.Add(i, sources);
			i++;
		}
//		soundCollection["instrument_b"][0].Play ();

	}

	void Update() {
		System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew(); 
		List<Player> playerList = gameObject.GetComponent<SocketBehaviour> ().PlayerList;
		List <Player> sortedList = playerList.OrderByDescending (p => p.CurrentlyPlaying).ToList();

		Dictionary<int, int[]> tempPlay = new Dictionary<int, int[]>();
		int totalPlaying = 0;
		foreach (Player p in sortedList) {
			if(p.CurrentlyPlaying) {
				totalPlaying++;
				Debug.Log ("playing");
				int[] noteArray;
				if (tempPlay.TryGetValue(p.Instrument, out noteArray)){
					tempPlay [p.Instrument][p.Note]++;
				} else {
					int[] noteArr = new int[numberNotes];
					noteArr[p.Note] = 1;
					tempPlay [p.Instrument] = noteArr;
				}
			} else {
				break;
			}
		}
		playSounds (tempPlay, totalPlaying);
		stopwatch.Stop();
		foreach (KeyValuePair<int, int[]> kvp in tempPlay)
		{
			Debug.Log( "instrument: "+kvp.Key+" notes: "+ kvp.Value[0]+ " , "+kvp.Value[1]+ " , "+kvp.Value[2] + " time: "+stopwatch.ElapsedMilliseconds);
		}
	}

	void playSounds(Dictionary<int, int[]> tempPlay, int total) {
		foreach (KeyValuePair<int, int[]> instr in tempPlay) {
			if(instr.Value.Length > 0) {
				for (int i = 0; i < instr.Value.Length; i++) {
					int volume = instr.Value [i];
					if(volume > 0 && !soundCollection [instr.Key] [i].isPlaying) {
						soundCollection [instr.Key] [i].Play ();
					}
					Debug.Log("note "+i);

				}
			}
		}
	}

	

}
