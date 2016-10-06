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
public class ChargeScript : MonoBehaviour
{
    /// <summary>
    /// float that makes pick up stand above ground instead of touching it at the beginning
    /// </summary>
    public float PickUpStandingHeight;

    /// <summary>
    /// speed that pickup goes above player's head
    /// </summary>
    //public float PickUpGoesUpSpeed;

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
    //private float pickUpStartPos;

    /// <summary>
    /// final position of pickup above head
    /// </summary>
    //private float pickUpEndPos;

    /// <summary>
    /// boolean is picked
    /// </summary>
    private bool isPicked;

    /// <summary>
    /// boolean is rotating above head
    /// </summary>
    private bool isRotating;

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
    public void Start()
    {
        Player = GameObject.Find("Character");
        isPicked = false;
        isRotating = false;

        this.transform.SetParent(Player.transform);

        if (GridData.gridManager.PickUpCount== 1)
        {
            // lerp the pick up above player's head
            transform.position = new Vector3(Player.transform.position.x, transform.position.y + PickUpAboveHead, Player.transform.position.z);
            // place picked ymers on a circle 
            //this.transform.position = new Vector3((transform.position.x + Mathf.Sin(timer)), transform.position.y, ((transform.position.z + Mathf.Cos(timer))));
        }
            
    }

    /// <summary>
    /// Update function
    /// </summary>
    public void Update()
    {
        //transform.Rotate(0, Time.deltaTime * PickUpRotateSpeed, 0);
        timer += Time.deltaTime;

        // rotate ymers around player's head
        transform.RotateAround(Player.transform.position, Vector3.up, 20 * Time.deltaTime);

        if (GridData.gridManager.tileAdded)
        {
            GridData.gridManager.tileAdded = false;
            Destroy(this.gameObject);
        }
    }

}
