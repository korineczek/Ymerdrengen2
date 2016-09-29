using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour {

    public GameObject tracker;
    public Animator anim;
    public GameObject winText;
    public GameObject deathText;

    public int nextLevel;
    public bool progressTimer = false;
    public float trackerTime;
    float levelTime;



    // Use this for initialization
    void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {

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

                StartCoroutine("LevelTransition");
            }
        }
       
       

	}

    public void StartGame()
    {
        progressTimer = true;
    }

    public void Death()
    {
        progressTimer = false;
        deathText.SetActive(true);
    }

    IEnumerator LevelTransition()
    {
        winText.SetActive(true);
        yield return new WaitForSeconds(2);
        Application.LoadLevel(nextLevel);

    }
   
}
