using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("Setup");
	}
}
