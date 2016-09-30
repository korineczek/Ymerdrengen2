using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


    public GameObject levelSelection;
    public GameObject options;

	void Start () {


        if (!PlayerPrefs.HasKey("Setup"))
        {
           
            PlayerPrefs.SetString("Language", "English");
            PlayerPrefs.SetInt("Level", 1);
            PlayerPrefs.SetFloat("SoundVolume", 1);
            PlayerPrefs.SetInt("SoundMute", 1);
            PlayerPrefs.SetString("Setup", "Done");
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("Setup"))
        {
            
        }

        if (PlayerPrefs.GetString("Setup") == "Ready")
        {
           
            
           
        }
    }
    
    public void Play()
    {
        levelSelection.SetActive(true);
        levelSelection.GetComponent<LevelSelection>().Fade();
    }	

    public void Options()
    {
        options.SetActive(true);
    }

}
