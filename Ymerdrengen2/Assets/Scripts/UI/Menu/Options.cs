using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    //float PlayerSound = PlayerPrefs.GetFloat("Sound");

    public GameObject menu;
    public GameObject options;
    public int credits;

    void Start()
    {
        //volumeSlider = GetComponentInChildren<Slider>();
        //volumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

    public void Dansk()
    {
        PlayerPrefs.SetString("Language", "Dansk");
    }

    public void English()
    {
        PlayerPrefs.SetString("Language", "English");
    }
  
    public void SoundVolume(GameObject obj)
    {
        var newVolume = (int)(obj.GetComponent<Slider>().value * 100f);
        PlayerPrefs.SetInt("SoundVolume", newVolume);
        AudioData.SetSoundParameter(SoundParameterHandle.MasterVolume, newVolume);
    }

    public void SoundMusic(GameObject obj)
    {
        var newVolume = (int)(obj.GetComponent<Slider>().value * 100f);
        PlayerPrefs.SetInt("SoundMusic", newVolume);
        AudioData.SetSoundParameter(SoundParameterHandle.MusicVolume, newVolume);
    }

    public void SoundSound(GameObject obj)
    {
        var newVolume = (int)(obj.GetComponent<Slider>().value * 100f);
        PlayerPrefs.SetInt("SoundSound", newVolume);
        AudioData.SetSoundParameter(SoundParameterHandle.SoundVolume, newVolume);
    }

    public void SoundMute()
    {
        if(PlayerPrefs.GetInt("SoundMute") == 0)
        {
            PlayerPrefs.SetInt("SoundMute", 1);
        } else if (PlayerPrefs.GetInt("SoundMute") == 1)
        {
            PlayerPrefs.SetInt("SoundMute", 0);
        }
    }

    public void StartCredits()
    {
        SceneManager.LoadScene(credits);
    }


    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
        //Application.LoadLevel(0);

        //PlayerPrefs.SetInt("Level", 1);
        //menu.SetActive(true);
        //options.SetActive(false);
    }

    public void Back()
    {
        PlayerPrefs.Save();

        AudioData.PlaySound(SoundHandle.MenuClickBack);
        menu.SetActive(true);
        options.SetActive(false);
    }
}
