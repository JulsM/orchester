using UnityEngine;
using System.Collections;

public class planeMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 2) -3, Mathf.PingPong(Time.time/3, 2) - 1);
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 2) - 3,Mathf.Sin(Time.time/3)-1);
    }
}
