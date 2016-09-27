using UnityEngine;
using System.Collections;

public class SetupData : MonoBehaviour {

    public GameObject language;
    public GameObject menu;

	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        if (!PlayerPrefs.HasKey("Setup"))
        {
            language.SetActive(true);
        }

        if (PlayerPrefs.HasKey("Setup"))
        {
            menu.SetActive(true);
        }
    }
}
