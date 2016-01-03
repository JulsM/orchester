using UnityEngine;
using System.Collections;

public class PlayerSprite : MonoBehaviour {

    private string id;
    private float y;
    private GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    private Material myMaterial = new Material(Shader.Find("Particles/Alpha Blended"));


    public PlayerSprite( string id, float y) // Color c)
    {
        this.id = id;
        this.y = y;
        sphere.transform.position = new Vector3(0, 0, 0);
        sphere.transform.localScale = new Vector3(1, 1, 1);
        myMaterial.color = Color.blue;
        sphere.GetComponent<MeshRenderer>().material = myMaterial;
        //TODO how to remove? maybe seperate 'delete' function to be called before deleting it in the DrawOnScreen - Scripts map
    }

	// Update is called once per frame
	void Update () {
	    //TODO this for animation?
	}

    public void changeColor(Color c)
    {
        myMaterial.color = c;
    }

    public void deleteSphere()
    {
        sphere.SetActive(false);
    }

    public string getID()
    {
        return id;
    }

    public float getY()
    {
        return y;
    }

    public void setY(float y)
    {
        this.y = y;
    }

}
