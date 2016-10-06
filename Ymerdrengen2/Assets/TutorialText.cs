using UnityEngine;
using System.Collections;

public class TutorialText : MonoBehaviour {

    public GameObject UI;
    public GameObject text;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
        if(UI.GetComponent<LevelProgression>().showText)
        {
            text.SetActive(true);
        } else
        {
            text.SetActive(false);
        }

	}

   
}
