using UnityEngine;
using System.Collections;

public class Language : MonoBehaviour {

    public GameObject language;
    public GameObject menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Dansk()
    {
        PlayerPrefs.SetString("Setup", "Ready");
        PlayerPrefs.SetString("Language", "Dansk");
        menu.SetActive(true);
        language.SetActive(false);
    }

    public void English()
    {
        PlayerPrefs.SetString("Setup", "Ready");
        PlayerPrefs.SetString("Language", "English");
        menu.SetActive(true);
        language.SetActive(false);
    }
}
