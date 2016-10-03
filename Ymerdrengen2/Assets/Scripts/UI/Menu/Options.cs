using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    //float PlayerSound = PlayerPrefs.GetFloat("Sound");

    public GameObject menu;
    public GameObject options;

    private Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponentInChildren<Slider>();
        volumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
    }

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
        PlayerPrefs.SetFloat("SoundVolume", volumeSlider.value);
        var newVolume = volumeSlider.value * 100;
        AudioData.SetSoundParameter(SoundParameterHandle.MasterVolume, newVolume);
        //Debug.Log("Volume: " + newVolume);
    }

    public void SoundMusic()
    {
        PlayerPrefs.SetFloat("SoundMusic", gameObject.GetComponent<Slider>().value);
    }

    public void SoundSound()
    {
        PlayerPrefs.SetFloat("SoundSound", gameObject.GetComponent<Slider>().value);
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
