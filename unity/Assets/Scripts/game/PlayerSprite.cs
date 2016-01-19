using UnityEngine;
using System.Collections;


public class PlayerSprite {

    
    public string Id { get; set; }
    public float Y { get; set; }
    private GameObject sphere;
//    Material mat = Resources.Load("Materials/sphereMaterial", typeof(Material)) as Material;
//    GameObject particles = GameObject.FindGameObjectWithTag("ParticleSystem");

    public PlayerSprite( string id, float y) // Color c)
    {
        this.Id = id;
        this.Y = y;
		sphere = GameObject.Instantiate(Resources.Load("Sphere")) as GameObject;
//        this.sphere.transform.position = new Vector3(0, 0, 0);
//        this.sphere.GetComponent<MeshRenderer>().material = mat;
//        particles.transform.parent = sphere.transform;
//        this.sphere.AddComponent<SphereCollider>();
//        particles.GetComponent<ParticleSystem>().enableEmission = false;
    }
//
//    public void setParticleSystem(bool b)
//    {
//        particles.GetComponent<ParticleSystem>().enableEmission = b;
//    }

    public void changeColor(int instrument)
    {
		Material mat = this.sphere.GetComponent<Renderer> ().material;
        switch (instrument)
        {
            case 0:
                mat.color = Color.green;
                break;
            case 1:
                mat.color = Color.blue;
                break;
            case 2:
                mat.color = Color.yellow;
                break;
            case 3:
                mat.color = Color.magenta;
                break;
            case 4:
                mat.color = Color.red;
                break;
            default:
                mat.color = Color.black;
                break;
        }
    }

    public void deleteSphere()
    {
        //TODO better way to destory?
//        sphere.SetActive(false);
		GameObject.Destroy (this.sphere);
    }

    public GameObject getSphere()
    {
        return sphere; 
    }
}
