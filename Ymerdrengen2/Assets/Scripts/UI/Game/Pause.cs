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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
