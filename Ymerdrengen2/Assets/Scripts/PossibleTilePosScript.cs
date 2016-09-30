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
            StartCoroutine(Blink());
        }
        else
        {
            this.GetComponent<Renderer>().enabled = false;
        }

    }

    IEnumerator Blink()
    {
        //float endTime = Time.time + waitTime;
        while (grid.GetComponent<GridManager>().possiblePlacement)
        {
            this.GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(0.8f);
            this.GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(0.8f);
        }
    }
}
