using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


    public GameObject levelSelection;
    public GameObject options;

	void Start () {


        if (PlayerPrefs.GetString("Setup") == "Ready")
        {
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.SetInt("SoundMute", 1);
            PlayerPrefs.SetString("Setup", "Done");
            PlayerPrefs.Save();
            
           
        }
    }
    
    public void Play()
    {
        levelSelection.SetActive(true);
    }	

    public void Options()
    {
        options.SetActive(true);
    }

}
