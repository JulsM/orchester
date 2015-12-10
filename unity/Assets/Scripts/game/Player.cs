using UnityEngine;
using System.Collections;

public class Player{

	public string Id { get; set; }
	public string X { get; set; }
	public string Y { get; set; }

	public Player(string id)
	{
		Id = id;
	}
}
