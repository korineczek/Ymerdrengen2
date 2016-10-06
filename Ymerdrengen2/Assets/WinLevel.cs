using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinLevel : MonoBehaviour {

    public int credits;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Credits()
    {
        SceneManager.LoadScene(credits);
    }
}
