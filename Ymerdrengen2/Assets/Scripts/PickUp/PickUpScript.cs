using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for pick ups
/// </summary>
public class PickUpScript : MonoBehaviour
{
    public float PickUpEndPos;
    public float PickUpStartPos;
    public float PickUpGoesUpSpeed;
    public float PickUpRotateSpeed;
    public float pickUpGoesUp;

    /// <summary>
    /// The player game object
    /// </summary>
    GameObject player;
    bool isPicked;

    // Use this for initialization
    public void Start() {
        
        player = GameObject.Find("Character");
        isPicked = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        PickUpGoesUpSpeed = 10f;
        PickUpRotateSpeed = 40f;
        pickUpGoesUp = 5f;
        PickUpStartPos = transform.position.y;
        PickUpEndPos = transform.position.y + pickUpGoesUp;
  
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
