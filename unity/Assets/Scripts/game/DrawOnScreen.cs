using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawOnScreen : MonoBehaviour {

    //private float x;
    Vector3 startPosition = new Vector3 (-10,0,5);
    int maxSpeed=3;
    int neg = 1;

    void Start () {

    }

    public void draw(Player p, PlayerSprite s)
    {
        
        s.Y = p.Y/10;
        //s.getSphere().transform.position = new Vector3(0, 0, p.Y / 10);
        //s.getSphere().transform.position = new Vector3(startPosition.x + (Mathf.Sin(Time.deltaTime)*neg), 0, p.Y/10);
        s.getSphere().transform.position = new Vector3(Mathf.PingPong(Time.time, 10)-5 , 0, p.Y / 10);
 //       if (s.getSphere().transform.position.x > 10f || transform.position.x < -10f)
 /*       {
            neg = -neg;
            //transform.position = new Vector3(transform.position.x, 0, p.Y/10);
        }*/
        //Debug.Log("xPos: " + startPosition.x + Mathf.Sin(Time.time * maxSpeed));
//        Debug.Log("s.Y: " + s.Y);
//        Debug.Log("p.Y: " + p.Y);
    }


}
