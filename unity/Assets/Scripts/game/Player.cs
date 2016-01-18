using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	public string Name { get; set; }
	private float y;
	public int Instrument { get; set; }
	public int Note { get; set; }
	public bool CurrentlyPlaying { get; set; }

	public float Y    // the Name property
	{
		get
		{
			return this.y;
		}
	}

	public Player(string id, string name)
	{
		this.Id = id;
		this.Name = name;
		this.y = 50;
		this.Instrument = 1;
		this.Note = 1;
		this.CurrentlyPlaying = false;
	}

	public void updatePlayer(float y, int instr, int note) {
		this.y = y;
		this.Instrument = instr;
		this.Note = note;
		this.CurrentlyPlaying = true;
	}



	public override string ToString()
	{
		return "Player: " + this.Id + " Position: " + this.y + " Instrument: "+ this.Instrument + " note: "+this.Note+ " playing: "+ this.CurrentlyPlaying;
	}

}
