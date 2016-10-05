using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    //float PlayerSound = PlayerPrefs.GetFloat("Sound");

    public GameObject menu;
    public GameObject options;
    public int credits;

    private Slider masterVolSlider;
    private Slider musicVolSlider;
    private Slider soundVolSlider;

    void Start()
    {
        var allSliders = GetComponentsInChildren<Slider>();
        foreach (Slider s in allSliders) {
            switch (s.gameObject.name) {
                case "Master Slider":
                    masterVolSlider = s; break;
                case "Music Slider":
                    musicVolSlider = s; break;
                case "Sounds Slider":
                    soundVolSlider = s; break;
                default: Debug.LogWarning("Found unknown slider in '" + s.gameObject.name + "'!"); break;
            }
        }

        masterVolSlider.value = System.Convert.ToSingle(PlayerPrefs.GetInt("SoundVolume")) / 100f;
        musicVolSlider.value = System.Convert.ToSingle(PlayerPrefs.GetInt("SoundMusic")) / 100f;
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
        AudioData.SetSoundParameter(SoundParameterHandle.MasterVolume, newVolume);
        AudioData.MasterVolume = newVolume;
    }

    public void SoundMusic(GameObject obj)
    {
        var newVolume = (int)(obj.GetComponent<Slider>().value * 100f);
        AudioData.SetSoundParameter(SoundParameterHandle.MusicVolume, newVolume);
        AudioData.MusicVolume = newVolume;
    }

    public void SoundSound(GameObject obj)
    {
        var newVolume = (int)(obj.GetComponent<Slider>().value * 100f);
        AudioData.SetSoundParameter(SoundParameterHandle.SoundVolume, newVolume);
    }

    public void SoundMute()
    {
        if(PlayerPrefs.GetInt("SoundMute") == 0)
        {
            PlayerPrefs.SetInt("SoundMute", 1);
            AudioData.StopMenuMusic();
        }
        else if (PlayerPrefs.GetInt("SoundMute") == 1)
        {
            PlayerPrefs.SetInt("SoundMute", 0);
            AudioData.StartMenuMusic();
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
        var newMaster = masterVolSlider.value * 100f;
        var newMusic = musicVolSlider.value * 100f;
        var newSound = soundVolSlider.value * 100f;
        PlayerPrefs.SetInt("SoundMusic", (int)newMaster);
        PlayerPrefs.SetInt("SoundVolume", (int)newMusic);
        PlayerPrefs.SetInt("SoundSound", (int)newSound);
        PlayerPrefs.Save();

        AudioData.PlaySound(SoundHandle.MenuClickBack);
        menu.SetActive(true);
        options.SetActive(false);
    }
}
