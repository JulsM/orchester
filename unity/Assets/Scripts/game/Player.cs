using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	private float y;
	private int instrument;

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
	}

	public void updatePosition(float y, int instr) {
		this.y = y;
		this.instrument = instr;
	}

	public override string ToString()
	{
		return "Player: " + this.Id + " Position: " + this.y + " Instrument: "+ this.instrument;
	}

}
