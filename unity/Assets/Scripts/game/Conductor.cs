using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using System.Linq;

public class Conductor: MonoBehaviour {


	private AudioMixer master;
	private Dictionary<int, AudioSource[]> soundCollection; // all sounds as AudioSources
	private Dictionary<int, int[]> tempPlaying; //array of temporarily playing instruments, notes and how much of them

	static Dictionary<string, string> soundNames = new Dictionary<string, string> {
		{"instrument_a", "violin"},
		{"instrument_b", "oboe"},
		{"instrument_c", "waterpipe"},
		{"instrument_d", "conga"},
		{"instrument_e", "synth"}
	};
	static int numberNotes= 10;
	static float fadeRate = 0.99999f;



	void Start() {
		master = Resources.Load("Master") as AudioMixer; // master mixer
		soundCollection = new Dictionary<int, AudioSource[]>();
		int i = 0;
		//load AudioClips like specified in the SoundNames array
		foreach(KeyValuePair<string, string> instrument in soundNames){
			
			AudioSource[] sources = new AudioSource[numberNotes];

			for(int j = 1; j <= numberNotes ; j++) {
				string note = instrument.Value +"_"+ j;
				AudioSource s = gameObject.AddComponent<AudioSource>(); //add AudioSource to gameObject
				s.clip = Resources.Load(instrument.Key + "/"+note) as AudioClip; // load from Ressources folder
				s.outputAudioMixerGroup = master.FindMatchingGroups (instrument.Key) [0]; // route to matching mixer
				sources [numberNotes - j] = s;
			}
			soundCollection.Add(i, sources);
			i++;
		}
		playOnStart ();
		tempPlaying = new Dictionary<int, int[]> ();
	}



	/// <summary>
	/// Transition from the volume of the currently playing sounds to the volume of the sounds being played next.
	/// </summary>
	/// <param name="nextPlay">array with instruments and notes to be played next. With number of players playing one note.</param>
	/// <param name="total">Total number of notes being played.</param>
	public void crossfadeSounds(Dictionary<int, int[]> nextPlay, int total) {
//		Debug.Log ("nextplay");
//		foreach (KeyValuePair<int, int[]> kvp in nextPlay)
//		{
//			Debug.Log( "instrument: "+kvp.Key+" notes: "+ kvp.Value[0]+ " , "+kvp.Value[1]+ " , "+kvp.Value[2]);
//		}
//		Debug.Log ("tempPlay");
//		foreach (KeyValuePair<int, int[]> kvp in tempPlaying)
//		{
//			Debug.Log( "instrument: "+kvp.Key+" notes: "+ kvp.Value[0]+ " , "+kvp.Value[1]+ " , "+kvp.Value[2]);
//		}
		Dictionary<int, int[]> helpDict = new Dictionary<int, int[]>(nextPlay); // copy nextPlay for later
		
		foreach (KeyValuePair<int, int[]> kvp in tempPlaying) { // first go through tempPlaying
			int tempInstrument = kvp.Key;
			int[] tempNotes = kvp.Value;
			int[] nextNotes;
			if(nextPlay.TryGetValue(tempInstrument, out nextNotes)) { // check if a instrument gets played next
				for (int i = 0; i < numberNotes; i++) { // if yes iterate trough all notes of this instrument
					if(nextNotes[i] > 0) { // if one or more players play this note next
						float volume =  (float)nextNotes[i]/total; //calculate the volume of this note 
						AudioSource n = soundCollection [tempInstrument] [i]; // and set it to the soundCollection
						n.volume = volume;
					} else if(tempNotes[i] > 0) { // if note gets not played next, fade it out
						AudioSource n = soundCollection [tempInstrument] [i];
						StartCoroutine (FadeOut (n));
					}

				}
				nextPlay.Remove (tempInstrument); //remove instrument from nextPlay
			} else { // if instrument gets not played next, fade all notes of it out
				for (int i = 0; i < tempNotes.Length; i++) {
					AudioSource n = soundCollection [tempInstrument] [i];
					StartCoroutine (FadeOut (n));

				}
			}
		}
		if(nextPlay.Count > 0) { // if in nextPlay are some instruments left, iterate trough them and set the notes to their calculated valume
			foreach (KeyValuePair<int, int[]> kvp in nextPlay) {
				int[] nextNotes = kvp.Value;
				for(int i = 0; i < numberNotes; i++) {
					if(nextNotes[i] > 0) {
						float volume =  (float)nextNotes[i]/total;
						AudioSource n = soundCollection [kvp.Key] [i];
						n.volume = volume;
					}
				}
			}
		}
		tempPlaying = helpDict; //set tempPlaying to the copy of nextPlay
	}

	/// <summary>
	/// Since we change only the volume of the AudioSource, all sounds play from the Start with valume 0 in a loop
	/// </summary>
	void playOnStart() {
		AudioSource beat = gameObject.AddComponent<AudioSource>();
		beat.clip = Resources.Load("beat") as AudioClip; 
		beat.loop = true;
		beat.volume = 0.3f;
		beat.Play ();
		foreach(KeyValuePair<int, AudioSource[]> kvp in soundCollection) {
			AudioSource[] notes = kvp.Value;
			for(int i = 0; i < numberNotes; i++) {
				AudioSource n = notes [i];
				n.volume = 0;
				n.loop = true;
				n.Play ();
			}
		}
	}

	/// <summary>
	/// Fades out an AudioSource
	/// </summary>
	/// <returns>null</returns>
	/// <param name="audio">AudioSource</param>
	private IEnumerator FadeOut(AudioSource audio) {
		while( audio.volume > 0.6 )	{
			audio.volume = Mathf.Lerp( audio.volume, 0f, fadeRate * Time.deltaTime );
			yield return null;
		}
		// Close enough, turn it off!
		audio.volume = 0f;
	}

}
