using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

    //float PlayerSound = PlayerPrefs.GetFloat("Sound");

    public GameObject menu;
    public GameObject options;
    
  
    public void SoundVolume()
    {
        // Do stuff
    }

    public void SoundMute()
    {
        if(PlayerPrefs.GetInt("SoundMute") == 0)
        {
            PlayerPrefs.SetInt("SoundMute", 1);
        }

        if (PlayerPrefs.GetInt("SoundMute") == 1)
        {
            PlayerPrefs.SetInt("SoundMute", 0);
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Level", 1);

        menu.SetActive(true);
        options.SetActive(false);
    }

    public void Back()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }
}
