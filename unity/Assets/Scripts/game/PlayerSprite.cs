using UnityEngine;
using System.Collections;

public class PlayerSprite : MonoBehaviour {

    private string id;
    private float y;

    public PlayerSprite( string id, float y)
    {
        this.id = id;
        this.y = y;
        //TODO create drawable unity GameObject itself
        //TODO how to remove? maybe seperate 'delete' function to be called before deleting it in the DrawOnScreen - Scripts map
    }

	// Update is called once per frame
	void Update () {
	    //TODO this for animation?
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
