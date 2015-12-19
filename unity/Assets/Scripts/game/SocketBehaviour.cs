using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;

public class SocketBehaviour : MonoBehaviour {


	private SocketIOComponent socket;
	public List<Player> playerList;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find("SocketIO");
		socket = go.GetComponent<SocketIOComponent>();

		playerList = new List<Player> ();

		socket.On("open", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Open received: " + e.name );
		});
		socket.On("error", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Error received: " + e.name );
		});
		socket.On("close", (SocketIOEvent e) => {
			Debug.Log("[SocketIO] Close received: " + e.name );
		});
		socket.On("add player", addPlayer);
		socket.On("remove player", removePlayer);
		socket.On("update position", updatePlayerPosition);

		StartCoroutine("connect");

	}

	private IEnumerator connect()
	{
		// wait 1 seconds and continue
		yield return new WaitForSeconds(1);

		Dictionary<string, string> data = new Dictionary<string, string>();
		data["room"] = "test";
		socket.Emit("new room", new JSONObject(data), roomCreated);
	}

	/// <summary>
	/// This is the callback function of the emit "new room". If already players exist
	/// on server side they get added in the list of all players.
	/// </summary>
	/// <param name="playerArray">Player array.</param>
	private void roomCreated(JSONObject playerArray)
	{
		JSONObject players = playerArray [0] ["players"];
		for(int i = 0; i < players.Count; i++) {
			playerList.Add(new Player (players[i].ToString()));
			Debug.Log("[SocketIO] player: " + players[i].ToString());
		}
		Debug.Log("[SocketIO] new room created: " + playerList.Count);
	}

	/// <summary>
	/// Adds a specified player to the list of all players.
	/// </summary>
	/// <param name="e">E.</param>
	public void addPlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] add player  "+playerId);
		playerList.Add(new Player (playerId));
		Debug.Log("[SocketIO] players length  "+playerList.Count);
	}

	/// <summary>
	/// Removes a specified player from the list of all current players.
	/// </summary>
	/// <param name="e">E.</param>
	public void removePlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] remove player  "+playerId);
		for(int i = 0; i < playerList.Count; i++) {
			Player p = playerList [i];
			if(p.Id.Equals(playerId)) {
				playerList.RemoveAt (i);
				break;
			}
		}
		Debug.Log("[SocketIO] players length  "+playerList.Count);
	}


	private void updatePlayerPosition(SocketIOEvent e)
	{
		JSONObject player = e.data ["player"];
		for(int i = 0; i < playerList.Count; i++) {
			Player pList = playerList [i];
			if(pList.Id.Equals(player["id"].ToString())) {
				float x = float.Parse (player ["x"].ToString ());
				float y = float.Parse (player ["y"].ToString ());
				pList.updatePosition (x, y);
				Debug.Log("[SocketIO] updated player position  "+pList.Y);
				break;
			}
		}

	}

    public List<Player> getPlayerList()
    {
        return playerList;
    }






	// Update is called once per frame
	void Update () {
	
	}
}
