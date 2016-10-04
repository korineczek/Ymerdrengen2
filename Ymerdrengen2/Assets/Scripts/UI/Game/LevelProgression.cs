using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelProgression : MonoBehaviour {

    public GameObject tracker;
    public GameObject progressionTracker;
    public GameObject levelInfo;
    public GameObject pause;
    //public Animator anim;
    public GameObject winText;
    public GameObject deathText;

    public int nextLevel;
    public bool progressTimer = false;
    public float trackerTime;
    float levelTime;



    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        levelInfo.GetComponent<Text>().text = Application.loadedLevel.ToString();
        StartCoroutine(LevelBegin());
    }
	
	// Update is called once per frame
	void Update () {

        if (progressTimer)
        {
            levelTime += Time.deltaTime;
            //anim.Play("Tracker60", -1, levelTime / trackerTime);
            progressionTracker.GetComponent<Image>().fillAmount += 1.0f / trackerTime * Time.deltaTime;

            if ((int)levelTime == trackerTime)
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
        progressionTracker.SetActive(false);
        Time.timeScale = 0f;
    }

    IEnumerator LevelBegin()
    {
        yield return new WaitForSeconds(5);
        StartGame();
        yield break;
    }

    IEnumerator LevelTransition()
    {
        // Animation plz


        GridData.gridManager.TriggerTiles(false);
        levelInfo.GetComponent<Text>().color = Color.yellow;
        yield return new WaitForSeconds(4);
        tracker.SetActive(false);
        pause.SetActive(false);
        winText.SetActive(true);
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio
        yield return new WaitForSeconds(2);
        Application.LoadLevel(nextLevel);
        yield break;
    }
   
}
