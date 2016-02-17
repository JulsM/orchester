using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {


	private ParticleSystem particle;
	// Use this for initialization
	void Start () {
		particle =  (ParticleSystem)gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem>();
	}

	void OnTriggerEnter(Collider other) {
		particle.Play();
	}
	void OnTriggerExit(Collider other) {
		particle.Stop();
	}
}
