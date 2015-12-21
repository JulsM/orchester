using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	private float y;
	private int instrument;
	public bool CurrentlyPlaying { get; set; }

	public float Y    // the Name property
	{
		get
		{
			return this.y;
		}
	}

	public Player(string id)
	{
		Id = id;
		this.y = 0;
		this.instrument = 1;
		this.CurrentlyPlaying = false;
	}

	public void updatePosition(float y, int instr) {
		this.y = y;
		this.instrument = instr;
		this.CurrentlyPlaying = true;
	}



	public override string ToString()
	{
		return "Player: " + this.Id + " Position: " + this.y + " Instrument: "+ this.instrument + " playing: "+ this.CurrentlyPlaying;
	}

}
