using UnityEngine;
using System.Collections;

public class PossibleTilePosScript : MonoBehaviour
{

    GameObject grid;
    bool onoff;

    // Use this for initialization
    void Start()
    {
        grid = GameObject.Find("Grid");
        TileBlink();
        // StartCoroutine(Blink(2.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //IEnumerator Blink(float waitTime)
    //{
    //    float endTime = Time.time + waitTime;
    //    while (Time.time < waitTime)
    //    {
    //        this.GetComponent<Renderer>().enabled = false;
    //        yield return new WaitForSeconds(0.2f);
    //        this.GetComponent<Renderer>().enabled = true;
    //        yield return new WaitForSeconds(0.2f);
    //    }
    //}

    void TileBlink()
    {
        //if (Time.time > timer)
        /*if (grid.GetComponent<GridManager>().PickUpCount < 0)
        {

            //timer = Time.time + .4;
            onoff = !onoff;
            this.GetComponent<Renderer>().enabled = onoff;

        }//Time.time > timer ends */
    }
}
