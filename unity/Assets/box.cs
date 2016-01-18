using UnityEngine;
using System.Collections;

public class box : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision triggered");
        //other.transform.parent.GetComponent<GameObject>().transform.parent.GetComponent<PlayerSprite>().setParticleSystem(true);
        GameObject g = other.GetComponentInParent<GameObject>();
        PlayerSprite s = g.GetComponentInParent<PlayerSprite>();
        s.setParticleSystem(true);
        //other.GetComponentInParent<GameObject>().GetComponentInParent<PlayerSprite>().setParticleSystem(true);
       
        
    }

}
