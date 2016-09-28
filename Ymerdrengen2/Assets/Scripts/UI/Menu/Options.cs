using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    //float PlayerSound = PlayerPrefs.GetFloat("Sound");

    public GameObject menu;
    public GameObject options;
    


    public void Dansk()
    {
        PlayerPrefs.SetString("Language", "Dansk");
    }

    public void English()
    {
        PlayerPrefs.SetString("Language", "English");
    }
  
    public void SoundVolume()
    {
        PlayerPrefs.SetFloat("SoundVolume", gameObject.GetComponent<Slider>().value);
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
        Application.LoadLevel(0);

        //PlayerPrefs.SetInt("Level", 1);
        //menu.SetActive(true);
        //options.SetActive(false);
    }

    public void Back()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }
}
