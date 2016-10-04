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
    /// boolean is rotating above head
    /// </summary>
    private bool isRotating;

    /// <summary>
    /// timer
    /// </summary>
    private float timer;

    GameObject charge;

    /// <summary>
    /// start function
    /// </summary>
    public void Awake()
    {
        Player = GameObject.Find("Character");
        isPicked = false;
        isRotating = false;

        transform.position = new Vector3(transform.position.x, transform.position.y + PickUpStandingHeight, transform.position.z);
        pickUpStartPos = transform.position.y;
        pickUpEndPos = transform.position.y + PickUpAboveHead;
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
            isRotating = true;
        }

        if (isRotating)
        {
            // rotate ymers around player's head
            transform.RotateAround(Player.transform.position, Vector3.up, 20 * Time.deltaTime);

        }



    }

    /// <summary>
    /// is called when a pick up is picked
    /// </summary>
    public void TriggerPickUp()
    {

        isPicked = true;
        // lerp the pick up above player's head
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(pickUpStartPos, pickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), transform.position.z);

        // change to the sphere
        //if(this.gameObject.name == "ymerkarton(Clone)")
        //{
            this.gameObject.SetActive(false);
            
            charge = Instantiate(Resources.Load("Prefabs/Charge") as GameObject);
            charge.transform.position = this.transform.position;
            charge.transform.rotation = this.transform.rotation;
        //}

        //if (GridData.gridManager.PickUpCount > 1)
        //{
        //    // place picked ymers on a circle 
        //    this.transform.position = new Vector3((transform.position.x + Mathf.Sin(timer)), transform.position.y, ((transform.position.z + Mathf.Cos(timer))));

        //}

    }
}
