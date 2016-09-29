using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour {

    public GameObject tracker;
    public Animator anim;

    public int nextLevel;
    public bool progressTimer = false;
    public float trackerTime;
    float levelTime;


    // Use this for initialization
    void Start ()
    {
        StartGame();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (progressTimer)
        {
            levelTime += Time.deltaTime;
            anim.Play("Tracker60", -1, levelTime / trackerTime);

            if((int)levelTime == trackerTime)
            {

                if(PlayerPrefs.GetInt("Level") < nextLevel)
                {
                    PlayerPrefs.SetInt("Level", nextLevel);
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                //Application.LoadLevel(Application.loadedLevel + 1);
            }
        }
	}

    public void StartGame()
    {
        progressTimer = true;
    }
}
