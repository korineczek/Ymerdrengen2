using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor.SceneManagement;

public class UnlockLevel : MonoBehaviour {

    public int level;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerPrefs.GetInt("Level") < level)
        {
            gameObject.SetActive(false);
        }
	}

    public void PlayLevel()
    {
        if (PlayerPrefs.GetInt("Level") >= level)
        {
            StartCoroutine(CoStartLevel(level));
        }
    }

    private IEnumerator CoStartLevel(int level)
    {
        AudioData.PlaySound(SoundHandle.MenuClickForward);
        yield return new WaitForSeconds(0.4f);
        GameObject.Destroy(AudioData.audioManager.gameObject); // Unload SoundBank
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(level);
    }

}
