using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {


    void Start () {

    }

    public void draw(Player p, PlayerSprite s)
    {
        s.Y = p.Y/10;
        s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time, 10)-5 , 0, p.Y / 10);
    }


}
