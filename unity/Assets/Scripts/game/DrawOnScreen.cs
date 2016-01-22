using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    void Start () {
    }

    public void draw(Player p, PlayerSprite s)
    {
        s.Y = p.Y/10;
        s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time *3, 24)-12 , 0, p.Y / 10);
        s.changeColor(p.Instrument);
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
