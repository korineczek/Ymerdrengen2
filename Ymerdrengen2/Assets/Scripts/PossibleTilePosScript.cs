using UnityEngine;
using System.Collections;

public class PossibleTilePosScript : MonoBehaviour
{

    GameObject grid;

    // Use this for initialization
    void Start()
    {
        grid = GameObject.Find("Grid");
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (grid.GetComponent<GridManager>().possiblePlacement)
        {
            this.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            this.GetComponent<Renderer>().enabled = false;
        }

    }
}
