using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using System.Linq;

public class Conductor: MonoBehaviour {


	private AudioMixer master;
	private Dictionary<string, AudioSource[]> soundCollection;

	static Dictionary<string, string[]> soundNames = new Dictionary<string, string[]> {
		{"instrument_a", new string[]{"t_low", "t","t_high" }},
		{"instrument_b", new string[]{"440", "480","520" }}
	};


	void Start() {
		master = Resources.Load("Master") as AudioMixer;
		soundCollection = new Dictionary<string, AudioSource[]>();
		foreach(KeyValuePair<string, string[]> instrument in soundNames){
			
			AudioSource[] sources = new AudioSource[instrument.Value.Length];

			for(int i = 0; i < instrument.Value.Length; i++) {
				string note = instrument.Value [i];
				AudioSource s = gameObject.AddComponent<AudioSource>();
				s.clip = Resources.Load(note) as AudioClip;
				s.outputAudioMixerGroup = master.FindMatchingGroups (instrument.Key) [0];
				sources [i] = s;
			}
			soundCollection.Add(instrument.Key, sources);
		}
//		soundCollection["instrument_b"][0].Play ();

	}

	int i = 0;
	void Update() {
		List<Player> playerList = gameObject.GetComponent<SocketBehaviour> ().PlayerList;
		i++;
		List <Player> sortedList = playerList.OrderByDescending (p => p.CurrentlyPlaying).ToList();
		if (i % 10 == 1) {

			foreach (Player p in sortedList) {
//				Debug.Log (p.ToString ());
			}
		}
	}

	

}
