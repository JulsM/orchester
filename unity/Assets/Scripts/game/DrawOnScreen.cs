using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    private Dictionary<Player, PlayerSprite> playerToSprite;
    private LinkedList<Player> playerList;
   

    void Start () {
        // playerList = SocketBehaviour.PlayerList;      SocketBehaviour muss statisch werden =/
        foreach (Player p in playerList)
        {
            playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        }

    }
	
	// Update is called once per frame
	void Update () {

        //if new player added, instantiate a new Sprite and put it into the sprites
        //remove accordingly
//        int tempSize = playerList.Count;
        //playerList = SocketBehaviour.getPlayers() ??
//        if(tempSize != playerList.Count) diffSprites(tempSize - playerList.Count);
//       
//        drawPlayers();
//        playSound();
	
	}

    public void drawPlayers()
    {
        
    }

    public void playSound()
    {

    }

    public void diffSprites(int diff)
    {

    }

}
