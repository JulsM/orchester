using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    private Dictionary<Player, PlayerSprite> playerToSprite;
    private List<Player> playerList;
   

    void Start () {
        List<Player> playerList = gameObject.GetComponent<SocketBehaviour>().PlayerList;
        playerList.Add(new Player("tim"));
        foreach (Player p in playerList)
        {
            playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        }

    }
	
	// Update is called once per frame
	void Update () {
         movePlayers();
        playSound();
    }

    public void movePlayers()
    {
        
    }

    public void addPlayerSprite(Player p)
    {
        playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
    }

    public void removePlayerSprite(Player p)
    {
        PlayerSprite s; 
        playerToSprite.TryGetValue(p, out s);
        s.deleteSphere();
        playerToSprite.Remove(p);
    }

    public void updatePlayerSprite(Player p, float y)
    {
        PlayerSprite s;
        playerToSprite.TryGetValue(p, out s);
        s.setY(y);
    }

    public void playSound()
    {

    }

}
