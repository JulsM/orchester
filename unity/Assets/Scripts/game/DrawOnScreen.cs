using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    private Dictionary<Player, PlayerSprite> playerToSprite = new Dictionary<Player, PlayerSprite>();
    private List<Player> playerList;
    private bool start = true;
   

    void StartAlternative () {
     /*   Debug.Log("DrawOnScreen is initialized");
        List<Player> playerList = gameObject.GetComponent<SocketBehaviour>().PlayerList;
        //List<Player> playerList = new List<Player>();
        playerList.Add(new Player("tim"));
        foreach (Player p in playerList)
        {
            playerToSprite.Add(p, new PlayerSprite(p.Id, p.Y));
        }
        */
    }

	
	// Update is called once per frame
	void Update () {
      /*  if (start)
        {
            StartAlternative();
            start = false;
        }
        
        movePlayers();
        playSound();
        */
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
