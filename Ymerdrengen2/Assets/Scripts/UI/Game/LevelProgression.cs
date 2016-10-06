using UnityEngine;
using UnityEngine.SceneManagement;
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


    //Death Screens
    public GameObject deathPie;
    public GameObject deathDrop;
    public GameObject deathCoffie;
    public GameObject deathFall;

    public int nextLevel;
    public bool progressTimer = false;
    public float trackerTime;
    public bool showText;
    float levelTime;



    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        levelInfo.GetComponent<Text>().text = SceneManager.GetActiveScene().buildIndex.ToString();

        GridData.lvlProgression = this;
        if (GridData.gridManager.isIntroAnimationPresent)
        {
            StartCoroutine(LevelBegin(7.5f));
            Debug.Log("Animation detected adding start time");
        }
        else
        {
            GridData.gridManager.triggerTileAnimations();
            StartCoroutine(LevelBegin(5));
        }
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
        showText = false;
    }

    public void Death()
    {
        progressTimer = false;
        deathText.SetActive(true);
        progressionTracker.SetActive(false);
        Time.timeScale = 0f;
    }

    IEnumerator LevelBegin(float time)
    {
        showText = true;
        GameObject.Find("Managers").transform.FindChild("inputManager").GetComponent<SwipeManager>().enabled = false;
        yield return new WaitForSeconds(time);
        StartGame();
        GridData.enemyManager.startLevel();
        if (GridData.gridManager.isIntroAnimationPresent)
        {
            GridData.gridManager.triggerTileAnimations();
            yield return new WaitForSeconds(2);
        }
        GameObject.Find("Managers").transform.FindChild("inputManager").GetComponent<SwipeManager>().enabled = true;
        showText = false;
    }

    IEnumerator LevelTransition()
    {
        // Animation plz
        GridData.gridManager.TriggerTiles(false);
        levelInfo.GetComponent<Text>().color = Color.yellow;
        GameObject.Find("Managers").transform.FindChild("inputManager").GetComponent<SwipeManager>().enabled = false;
        yield return new WaitForSeconds(4);
        AudioData.PlaySound(SoundHandle.Win); // Hack to play winning sound.
        tracker.SetActive(false);
        pause.SetActive(false);
        winText.SetActive(true);
        yield break;
    }

    public void NextLevel()
    {
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio

        SceneManager.LoadScene(nextLevel);
    }

    public void BackMenu()
    {
        if (AudioData.audioManager != null)
            GameObject.Destroy(AudioData.audioManager.gameObject); // Hacked to reset audio

        SceneManager.LoadScene(0);
    }

    public void CherryDeath()
    {
        deathFall.SetActive(false);
        deathDrop.SetActive(true);
    }

    public void PieDeath()
    {
        deathFall.SetActive(false);
        deathPie.SetActive(true);
    }

    public void CoffieDeath()
    {
        deathFall.SetActive(false);
        deathCoffie.SetActive(true);
    }
}
