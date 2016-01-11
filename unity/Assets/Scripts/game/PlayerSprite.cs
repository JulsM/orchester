using UnityEngine;
using System.Collections;

public class PlayerSprite{

    public string Id { get; set; }
    public float Y { get; set; }
    private GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //private Material myMaterial = new Material(Shader.Find("Particles/AlphaBlended"));


    public PlayerSprite( string id, float y) // Color c)
    {
        this.Id = id;
        this.Y = y;
        this.sphere.transform.position = new Vector3(0, 0, 0);
        /*
        this.sphere.transform.localScale = new Vector3(5, 5, 5);
        this.myMaterial.color = Color.blue;
        this.sphere.GetComponent<MeshRenderer>().material = myMaterial;
        */
        //TODO how to remove? maybe seperate 'delete' function to be called before deleting it in the DrawOnScreen - Scripts map
    }

	// Update is called once per frame
	void Update () {
	    //TODO this for animation?
	}

    public void changeColor(Color c)
    {
        //myMaterial.color = c;
    }

    public void deleteSphere()
    {
        //TODO destroy really!
        sphere.SetActive(false);
    }

    public GameObject getSphere()
    {
        return sphere; 
    }

}
