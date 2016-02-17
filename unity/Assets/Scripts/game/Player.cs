using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	public string Name { get; set; }
	private float y;
	public int Instrument { get; set; }
	public int Note { get; set; }
	public bool CurrentlyPlaying { get; set; }

	public float Y 
	{
		get
		{
			return this.y;
		}
	}

	/// <summary>
	/// Constructor of Player
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="name">Name.</param>
	public Player(string id, string name)
	{
		this.Id = id;
		this.Name = name;
		this.y = 50; // start in middle
		this.Instrument = 0;
		this.Note = 1;
		this.CurrentlyPlaying = false;
	}

	/// <summary>
	/// Updates all properties of a player instance
	/// </summary>
	/// <param name="y">The y coordinate.</param>
	/// <param name="instr">Instrument</param>
	/// <param name="note">Note.</param>
	public void updatePlayer(float y, int instr, int note) {
		this.y = y;
		this.Instrument = instr;
		this.Note = note;
		this.CurrentlyPlaying = true;
	}


	/// <summary>
	/// prints all properties of a player object for debugging purposes.
	/// </summary>
	/// <returns>String representing a player.</returns>
	public override string ToString()
	{
		return "Player: " + this.Id + " Position: " + this.y + " Instrument: "+ this.Instrument + " note: "+this.Note+ " playing: "+ this.CurrentlyPlaying;
	}

}
