using UnityEngine;
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
            gameObject.GetComponent<Image>().color = new Color(255, 0, 0);
        }
	}

    public void PlayLevel()
    {
        if (PlayerPrefs.GetInt("Level") >= level)
        {
            //EditorSceneManager.LoadScene(level);
            Application.LoadLevel(level);
        }
    }
}
