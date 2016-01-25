using UnityEngine;
using System.Collections;

public class middlePlaneMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void Update()
    {
        //transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time, 2) -3, Mathf.PingPong(Time.time/3, 2) - 1);
        transform.position = new Vector3(Mathf.PingPong(Time.time*3, 20) -5,transform.position.y, transform.position.z);
    }
}
