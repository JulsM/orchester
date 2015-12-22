using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;
using System;


public class SocketBehaviour : MonoBehaviour {


	private SocketIOComponent socket;
	public List<Player> PlayerList { get; set;}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		PlayerList = new List<Player> ();

		socket.On("open", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Open received: " + socket.sid );
//			Dictionary<string, string> data = new Dictionary<string, string>();
//			data["room"] = "test";
//			socket.Emit("new room", new JSONObject(data), roomCreated);
		});
		socket.On("error", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Error received: " + e.name );
		});
		socket.On("close", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Close received: " + e.name );
		});
//		socket.On("add player", addPlayer);
//		socket.On("remove player", removePlayer);
//		socket.On("update position", updatePlayerPosition);
//		socket.On("stop interaction", stopPlayerInteraction);

	}



	/// <summary>
	/// This is the callback function of the emit "new room". If already players exist
	/// on server side they get added in the list of all players.
	/// </summary>
	/// <param name="playerArray">Player array with IDs.</param>
	private void roomCreated(JSONObject playerArray)
	{
		JSONObject players = playerArray [0] ["players"];
		for(int i = 0; i < players.Count; i++) {
			PlayerList.Add(new Player (players[i].ToString()));
			Debug.Log("[SocketIO] player: " + players[i].ToString());
		}
		Debug.Log("[SocketIO] new room created: " + PlayerList.Count);
	}



	/// <summary>
	/// Adds a specified player to the list of all players.
	/// </summary>
	/// <param name="e">playerID</param>
	public void addPlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] add player  "+playerId);
		PlayerList.Add(new Player (playerId));
		Debug.Log("[SocketIO] players length  "+PlayerList.Count);
	}

	/// <summary>
	/// Removes a specified player from the list of all current players.
	/// </summary>
	/// <param name="e">playerID</param>
	public void removePlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] remove player  "+playerId);
		for(int i = 0; i < PlayerList.Count; i++) {
			Player p = PlayerList [i];
			if(p.Id.Equals(playerId)) {
				PlayerList.RemoveAt (i);
				break;
			}
		}
		Debug.Log("[SocketIO] players length  "+PlayerList.Count);
	}

	/// <summary>
	/// Updates the position of a specified player object.
	/// </summary>
	/// <param name="e">player: id, x position % (float), y position % (float)</param>
	private void updatePlayerPosition(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] updated player position  "+e.data);

		JSONObject player = e.data ["player"];
		for(int i = 0; i < PlayerList.Count; i++) {
			Player pList = PlayerList [i];
			if(pList.Id.Equals(player["id"].ToString())) {
				float y = float.Parse (player ["y"].ToString ());
				int instrument = Int32.Parse(player ["instrument"].ToString ());
				pList.updatePosition (y, instrument);
				break;
			}
		}

	}


    
	/// <summary>
	/// Stops interaction of a specified player.
	/// </summary>
	/// <param name="e">playerID</param>
	private void stopPlayerInteraction(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();

		for(int i = 0; i < PlayerList.Count; i++) {
			Player p = PlayerList [i];
			if(p.Id.Equals(playerId)) {
				p.CurrentlyPlaying = false;
				Debug.Log("[SocketIO] stop player  "+p.ToString());
				break;
			}
		}

	}





}
