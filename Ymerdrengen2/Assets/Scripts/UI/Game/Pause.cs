using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Pause : MonoBehaviour {

    public GameObject score;
    public GameObject progress;

    public GameObject pauseScreen;
    public GameObject pauseButton;
    public GameObject unPauseButton;
    public GameObject resetButton;
    public GameObject menuButton;

    bool isPaused;

    public void Start()
    {
        isPaused = false;
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) 
        {
            Debug.Log("Hey");
            PauseGame();
            isPaused = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnPauseGame();
            isPaused = false;
        }

       
    }

    public void PauseGame()
    {
        AudioData.StopSound(StopSoundHandle.Pause);
        Time.timeScale = 0f;
        progress.GetComponent<LevelProgression>().progressTimer = false;
        score.GetComponent<Score>().getScore = false;

        pauseScreen.SetActive(true);
        unPauseButton.SetActive(true);
        resetButton.SetActive(true);
        menuButton.SetActive(true);
        pauseButton.SetActive(false);
        
    }

    public void UnPauseGame()
    {
        AudioData.PlaySound(SoundHandle.Resume);
        Time.timeScale = 1f;
        progress.GetComponent<LevelProgression>().progressTimer = true;
        score.GetComponent<Score>().getScore = true;


        pauseButton.SetActive(true);
        pauseScreen.SetActive(false);
        unPauseButton.SetActive(false);
        resetButton.SetActive(false);
        menuButton.SetActive(false);
    }


    public void ResetLevel()
    {
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio

        SceneManager.LoadScene(0);
    }

}
