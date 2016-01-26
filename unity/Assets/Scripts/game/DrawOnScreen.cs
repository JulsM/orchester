using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {




    public void draw(Player p, PlayerSprite s)
    {
        s.Y = p.Y/10;
		s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time *3,12)-6 , 0, p.Y / 10);//24 -12
        s.changeColor(p.Instrument);
        s.explosion();
    }

}
