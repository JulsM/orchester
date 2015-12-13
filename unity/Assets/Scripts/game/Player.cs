using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	private float x;
	private float y;

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
		this.x = 0;
		this.y = 0;
	}

	public void updatePosition(float x, float y) {
		this.x = x;
		this.y = y;
	}

}
