using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    private Dictionary<Player, PlayerSprite> playerToSprite;
    private LinkedList<Player> newPlayerList;
    private LinkedList<Player> deletePlayerList;
   

    void Start () {
        // playerList = SocketBehaviour.PlayerList;      SocketBehaviour muss statisch werden =/
        // SocketBehaviour.PlayerList = new List<Player>();
        //foreach (Player p in newPlayerList)
        //{
        //    playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        //}

    }
	
	// Update is called once per frame
	void Update () {
        //Modify SocketBehaviour accordingliy:
        //SocketBehaviour has a list of new Players that is cleared after its been loaded into this playerToSprite Dictionary
        //newPlayerList = SocketBehaviour.newPlayerList;
        //SocketBehaviour.newPlayerList = new List<Player>();
        //deletePlayerList = SocketBehaviour.deletePlayerList;
        //SocketBehaviour.deletePlayerList = new List<Player>();
        //foreach (Player p in newPlayerList)
        //{
        //    playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        //}
        //foreach (Player p in deletePlayerList)
        //{
        //    playerToSprite.Remove(p);
        //}

              
        //drawPlayers();
        //playSound();

    }

    public void drawPlayers()
    {
        
    }

    public void playSound()
    {

    }

}
