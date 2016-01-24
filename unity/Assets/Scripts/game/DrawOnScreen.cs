using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    int i,y;
    GameObject[] explosions;

    void Start () {
        i = 0;
        y = 0;
        explosions = new GameObject[5];
    }

    public void draw(Player p, PlayerSprite s)
    {
        s.Y = p.Y/10;
		s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time *3,24)-12 , 0, p.Y / 10);
        s.changeColor(p.Instrument);
        if (i == 30)
        {
            if (y >= 5) y = 0;
            GameObject.Destroy(explosions[y]);
            explosions[y] = s.addAfterImage(p.Instrument);
            explosions[y].transform.position = s.getSphere().transform.position;
            y++;
            i = 0;
        }
        i++;
        //GameObject ai = s.addAfterImage();
        //ai.transform.position = s.getSphere().transform.position;
        //s.OnCollisionEnter();
        /*
        if(s.getSphere().transform.position.x > 0)
        {
            s.setParticleSystem(true);
        }
        else
        {
            s.setParticleSystem(false);
        }
        */
    }


}
