using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for pick ups
/// </summary>
public class PickUpScript : MonoBehaviour
{
    public float PickUpStandingPos = 0.3f;
    public float PickUpGoesUpSpeed = 10f;
    public float PickUpRotateSpeed = 40f;
    public float pickUpAboveHead = 5f;
    /// <summary>
    /// The player game object
    /// </summary>
    public GameObject player;

    private float PickUpEndPos;
    private float PickUpStartPos;

    bool isPicked;

    // Use this for initialization
    public void Start() {
        
        player = GameObject.Find("Character");
        isPicked = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + PickUpStandingPos, transform.position.z);
        PickUpStartPos = transform.position.y;
        PickUpEndPos = transform.position.y + pickUpAboveHead;
  
    }

    // Update is called once per frame
    public void Update() {
        transform.Rotate(0, Time.deltaTime * PickUpRotateSpeed, 0);

        if (isPicked && !player.GetComponent<Player>().isLerping)
        {
            // set pickUp child of the character so it follows him
            this.transform.SetParent(player.transform);
            player.GetComponent<Player>().numYmer++;
            isPicked = false;
        }
    }

    public void TriggerPickUp()
    {
        
        // lerp the pick up above player's head
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), transform.position.z);
        isPicked = true;

    }
}
