using UnityEngine;
using System.Collections;

public class introAnimation : MonoBehaviour {

    Animator anim;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("EndState"))
        {

            this.transform.parent.gameObject.SetActive(false);
            GridData.gridManager.revivePlayer();

        }

    }
}
