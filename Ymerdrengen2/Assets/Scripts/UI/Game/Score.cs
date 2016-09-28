using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    int score;
    public int scoreEachSec;
    public bool getScore;

	// Use this for initialization
	void Start () {

        getScore = true;
	}
	
	// Update is called once per frame
	void Update () {

        if(getScore)
        {
            Point(scoreEachSec);
        }
        
        
    }

    public void Point (int points)
    {
        score = points + score;
        gameObject.GetComponent<Text>().text = score.ToString();


    }
}
