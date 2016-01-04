using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    private Dictionary<Player, PlayerSprite> playerToSprite;
    private List<Player> playerList;
   

    void Start () {
        List<Player> playerList = gameObject.GetComponent<SocketBehaviour>().PlayerList;
        foreach (Player p in playerList)
        {
            playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        }

    }
	
	// Update is called once per frame
	void Update () {
        drawPlayers();
        playSound();
    }

    public void drawPlayers()
    {
        
    }

    public void addPlayer(Player p)
    {
        playerList.Add(p);
        playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
    }

    public void removePlayer(Player p)
    {
        playerList.Remove(p);
        PlayerSprite s; 
        playerToSprite.TryGetValue(p, out s);
        s.deleteSphere();
        playerToSprite.Remove(p);
    }

    public void playSound()
    {

    }

}
