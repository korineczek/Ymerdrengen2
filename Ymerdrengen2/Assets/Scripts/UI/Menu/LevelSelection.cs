using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour {

    bool fadeIn;

    float playTimer = 0f;
    float duration = 10f;

    Animator[] levels;  

    //public Animator[] levelsAnimator;

	void Update () {
        if(fadeIn)
        {
            foreach (Animator level in levels)
            {
                level.SetBool("Fade", true);
            }

        }
    }

    public void Fade()
    { 
        levels = this.gameObject.GetComponentsInChildren<Animator>();
        fadeIn = true;
    }

  
}
