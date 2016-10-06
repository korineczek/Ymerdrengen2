using UnityEngine;
using UnityEngine.SceneManagement;
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
            PlayerPrefs.SetFloat("SoundMusic", 1);
            PlayerPrefs.SetFloat("SoundSound", 1);
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Play()
    {
        AudioData.PlaySound(SoundHandle.MenuClickForward);
        levelSelection.SetActive(true);
        levelSelection.GetComponent<LevelSelection>().Fade();
    }	

    public void Options()
    {
        AudioData.PlaySound(SoundHandle.MenuClickForward);
        options.SetActive(true);
    }

    public void BackToMenu()
    {
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio

        SceneManager.LoadScene(0);
    }

}
