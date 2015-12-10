using UnityEngine;
using System.Collections;
using SocketIO;
using System.Collections.Generic;

public class SocketBehaviour : MonoBehaviour {


	private SocketIOComponent socket;
	private List<Player> playerList;

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



	public void addPlayer(SocketIOEvent e)
	{
		string playerId = e.data ["player"].ToString ();
		Debug.Log("[SocketIO] add player  "+playerId);
		playerList.Add(new Player (playerId));
		Debug.Log("[SocketIO] players .length  "+playerList.Count);
	}

	public void roomCreated(JSONObject playerArray)
	{
		JSONObject players = playerArray [0] ["players"];
		for(int i = 0; i < players.Count; i++) {
			playerList.Add(new Player (players[i].ToString()));
			Debug.Log("[SocketIO] player: " + players[i].ToString());
		}
		Debug.Log("[SocketIO] new room created: " + playerList.Count);
	}




	// Update is called once per frame
	void Update () {
	
	}
}
