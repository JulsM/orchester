using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;
using System;


public class SocketBehaviour : MonoBehaviour {


	private SocketIOComponent socket;
	private List<Player> playerList;
	private DrawOnScreen draw;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		playerList = gameObject.GetComponent<Orchestra> ().PlayerList;
		draw = gameObject.GetComponent<DrawOnScreen> ();

		socket.On("open", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Open received: " + socket.sid );
			if(socket.sid.Length != 0) {
				socket.Emit("unity connect", new JSONObject(), connectCallback);
			}
		});
		socket.On("error", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Error received: " + e.name );
		});
		socket.On("close", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Close received: " + e.name );
		});
		socket.On("add player", addPlayer);
		socket.On("remove player", removePlayer);
		socket.On("update player", updatePlayer);
		socket.On("stop interaction", stopPlayerInteraction);



	}



	/// <summary>
	/// This is the callback function of the emit "connect unity". If already players exist
	/// on server side they get added in the list of all players.
	/// </summary>
	/// <param name="playerArray">answer with success and Player array with IDs.</param>
	private void connectCallback(JSONObject answer)
	{
		if (answer [0] ["connected"].ToString ().Equals ("true")) {
			JSONObject players = answer [0] ["players"];
			JSONObject names = answer [0] ["names"];
			for(int i = 0; i < players.Count; i++) {
				playerList.Add(new Player (players[i].ToString(), names[i].ToString()));
				Debug.Log("[SocketIO] player: " + players[i].ToString()+" name "+ names[i].ToString());
			}
			Debug.Log("[SocketIO] unity connected: player list length " + playerList.Count);
		} else {
			Debug.Log ("error connecting");
		}

	}



	/// <summary>
	/// Adds a specified player to the list of all players.
	/// </summary>
	/// <param name="e">playerID</param>
	public void addPlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		string playerName = e.data ["name"].ToString ();
		Debug.Log("[SocketIO] add player  "+playerId);
		playerList.Add(new Player (playerId, name));
		Debug.Log("[SocketIO] players length  "+playerList.Count);
        
        // DrawOnScreen method
        draw.addPlayerSprite(new Player(playerId, name));

	}

	/// <summary>
	/// Removes a specified player from the list of all current players.
	/// </summary>
	/// <param name="e">playerID</param>
	public void removePlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] remove player  "+playerId);
        Player p = new Player("", "");
		for(int i = 0; i < playerList.Count; i++) {
			p = playerList [i];
			if(p.Id.Equals(playerId)) {
				playerList.RemoveAt (i);
				break;
			}
		}
		Debug.Log("[SocketIO] players length  "+playerList.Count);

        // DrawOnScreen method
        draw.removePlayerSprite(p);
    }

	/// <summary>
	/// Updates the a specified player object.
	/// </summary>
	/// <param name="e">player: id, x position % (float), y position % (float)</param>
	private void updatePlayer(SocketIOEvent e)
	{
		

		JSONObject player = e.data ["player"];
		for(int i = 0; i < playerList.Count; i++) {
			Player pList = playerList [i];
			if(pList.Id.Equals(player["id"].ToString())) {
				float y = float.Parse (player ["y"].ToString ());
				int instrument = Int32.Parse(player ["instrument"].ToString ());
				int note = Int32.Parse(player ["note"].ToString ());
				pList.updatePlayer (y, instrument, note);
                
                // DrawOnScreen method
                //dos.updatePlayerSprite(pList,y);

//				Debug.Log("[SocketIO] updated player position  "+pList.ToString());
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

		for(int i = 0; i < playerList.Count; i++) {
			Player p = playerList [i];
			if(p.Id.Equals(playerId)) {
				p.CurrentlyPlaying = false;
				Debug.Log("[SocketIO] stop player  "+p.ToString());
				break;
			}
		}

	}





}
