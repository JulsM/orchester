using UnityEngine;
using System.Collections;


public class PlayerSprite {

    
    public string Id { get; set; }
    public float Y { get; set; }
    private GameObject sphere;
//    Material mat = Resources.Load("Materials/sphereMaterial", typeof(Material)) as Material;
//    GameObject particles = GameObject.FindGameObjectWithTag("ParticleSystem");

	public PlayerSprite(Player p) // Color c)
    {
		this.Id = p.Id;
		this.Y = p.Y;
		sphere = GameObject.Instantiate(Resources.Load("Sphere")) as GameObject;
		sphere.transform.GetChild (1).gameObject.GetComponent<TextMesh>().text = p.Name.Replace("\"", "");
		Debug.Log (p.Name);
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
		ParticleSystem particle = this.sphere.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
        switch (instrument)
        {
			case 0:
				mat.color = Color.green;
				particle.startColor = Color.green;
                break;
            case 1:
                mat.color = Color.blue;
				particle.startColor = Color.blue;
                break;
            case 2:
                mat.color = Color.yellow;
				particle.startColor = Color.yellow;
                break;
            case 3:
                mat.color = Color.magenta;
				particle.startColor = Color.magenta;
                break;
            case 4:
                mat.color = Color.red;
				particle.startColor = Color.red;
                break;
            default:
                mat.color = Color.black;
				particle.startColor = Color.black;
                break;
        }
    }

    public void deleteSphere()
    {
		GameObject.Destroy (this.sphere);
    }

    public GameObject getSphere()
    {
        return sphere; 
    }
}
