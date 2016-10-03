// <copyright file="PickUpScript.cs" company="Team4">
// CC
// </copyright>
// <author>Angeliki</author>
// <summary>Script attached to every pick up.</summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for pick ups
/// </summary>
public class PickUpScript : MonoBehaviour
{
    /// <summary>
    /// float that makes pick up stand above ground instead of touching it at the beginning
    /// </summary>
    public float PickUpStandingHeight;

    /// <summary>
    /// speed that pickup goes above player's head
    /// </summary>
    public float PickUpGoesUpSpeed;

    /// <summary>
    /// speed for pick up rotation
    /// </summary>
    public float PickUpRotateSpeed;

    /// <summary>
    /// how high pick up goes above player's head
    /// </summary>
    public float PickUpAboveHead;

    /// <summary>
    /// The player game object
    /// </summary>
    public GameObject Player;

    /// <summary>
    /// where pick up spawns
    /// </summary>
    private float pickUpStartPos;

    /// <summary>
    /// final position of pickup above head
    /// </summary>
    private float pickUpEndPos;

    /// <summary>
    /// boolean is picked
    /// </summary>
    private bool isPicked;

    /// <summary>
    /// starting position of ymer
    /// </summary>
    private Vector3 startPos;

    /// <summary>
    /// timer
    /// </summary>
    private float timer;
    /// <summary>
    /// start function
    /// </summary>
    public void Awake()
    {
        Player = GameObject.Find("Character");
        isPicked = false;
        transform.position = new Vector3(transform.position.x, transform.position.y + PickUpStandingHeight, transform.position.z);
        pickUpStartPos = transform.position.y;
        pickUpEndPos = transform.position.y + PickUpAboveHead;
        startPos = transform.position;
    }

    /// <summary>
    /// Update function
    /// </summary>
    public void Update()
    {
        transform.Rotate(0, Time.deltaTime * PickUpRotateSpeed, 0);
        timer += Time.deltaTime;


        if (isPicked && !Player.GetComponent<Player>().isLerping)
        {
            // set pickUp child of the character so it follows him
            this.transform.SetParent(Player.transform);
            Player.GetComponent<Player>().numYmer++;
            isPicked = false;
        }
       
            //transform.RotateAround(new Vector3(0, startPos.y, 0), Vector3.right, 30 * Time.deltaTime);
        

    }

    /// <summary>
    /// is called when a pick up is picked
    /// </summary>
    public void TriggerPickUp()
    {
        Debug.Log("PICKUP TRIGGERD");
        isPicked = true;
        // lerp the pick up above player's head
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(pickUpStartPos, pickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), transform.position.z);

        if (GridData.gridManager.GetComponent<GridManager>().PickUpCount > 1)
        {
            //transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            //transform.RotateAround(startPos, Vector3.right, 30 * Time.deltaTime);
            this.transform.position = new Vector3((transform.position.x + Mathf.Sin(timer) ), transform.position.y, ((transform.position.z + Mathf.Cos(timer) )));

        }

    }
}
