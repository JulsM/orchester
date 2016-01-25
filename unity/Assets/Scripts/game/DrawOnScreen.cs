﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    int wait;

    void Start () {  
        wait = 0;
    }

    public void draw(Player p, PlayerSprite s)
    {
        s.Y = p.Y/10;
		s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time *3,24)-12 , 0, p.Y / 10);
        s.changeColor(p.Instrument);
        if (wait == 15)
        {
            s.addAfterImage(p.Instrument);
            wait = 0;
        }
        wait++;
    }

}
