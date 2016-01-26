using UnityEngine;
using System.Collections;


public class PlayerSprite {

    
    public string Id { get; set; }
    public float Y { get; set; }
    private GameObject sphere;
    private GameObject explosionSprite;
    private ParticleSystem particle;
    private Player p;
    private int wait=0;

    public PlayerSprite(Player p)
    {
        this.p = p;
		this.Id = p.Id;
		this.Y = p.Y;
		sphere = GameObject.Instantiate(Resources.Load("Sphere")) as GameObject;
		sphere.transform.GetChild (1).gameObject.GetComponent<TextMesh>().text = p.Name.Replace("\"", "");
        particle = (ParticleSystem)sphere.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
    }

    public void explosion()
    {
        if (wait == 20)
        {
            wait = 0;
            switch (p.Instrument)
            {
                case 0:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_green")) as GameObject;
                    break;
                case 1:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_blue")) as GameObject;
                    break;
                case 2:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_yellow")) as GameObject;
                    break;
                case 3:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_purple")) as GameObject;
                    break;
                case 4:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_red")) as GameObject;
                    break;
                default:
                    explosionSprite = GameObject.Instantiate(Resources.Load("explosion_green")) as GameObject;
                    break;
            }
            explosionSprite.transform.position = getSphere().transform.position;
            GameObject.Destroy(explosionSprite, 1.8F);
        }
        wait++;
    }

    public void changeColor(int instrument)
    {
		Material mat = this.sphere.GetComponent<Renderer> ().material;
		ParticleSystem particle = this.sphere.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ();
        switch (instrument)
        {
		case 0:
				Color g = new Color (0f, 255f, 0f);
				mat.color = g;
				particle.startColor = g;
                break;
            case 1:
				Color b = new Color (0f, 255f, 255f);
                mat.color = b;
				particle.startColor = b;
                break;
            case 2:
				Color y = new Color (255f, 255f, 0f);
                mat.color = y;
				particle.startColor = y;
                break;
            case 3:
				Color m = new Color (204f, 0f, 110f);
                mat.color = m;
				particle.startColor = m;
                break;
            case 4:
				Color r = new Color (255f, 0f, 0f);
                mat.color = r;
				particle.startColor = r;
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

	public void stopParticle() {
		this.sphere.transform.GetChild (0).gameObject.GetComponent<ParticleSystem> ().Stop ();
	}

    public void effectOn()
    {
        particle.Play();
    }

    public void effectOff()
    {
        particle.Stop();
    }
}
