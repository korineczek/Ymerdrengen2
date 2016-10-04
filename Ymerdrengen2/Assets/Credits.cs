using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EndCredits()
    {
        SceneManager.LoadScene(0);
    }
}
