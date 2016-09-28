using System.Collections;
using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Script for pick ups
/// </summary>
public class PickUpScript : MonoBehaviour
{
    //public bool isPickUp;
    public float PickUpEndPos;
    public float PickUpStartPos;
    public float PickUpGoesUpSpeed;
    public float PickUpRotateSpeed;
    public float pickUpGoesUp;
    public float PlaceNewTileSpeed;

    //GameObject pickUp;
    GameObject player;
    GameObject tile;
    GameObject newTile;
    bool isPicked;
    Vector3 TileEndPos;

    Dictionary<Vector2, GameObject> PickUpDic;


    // Use this for initialization
    void Start () {
        tile = GameObject.Find("Tile");
        player = GameObject.Find("Character");
        isPicked = false;
        //pickUp = GameObject.FindGameObjectWithTag("PickUp");
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
        PickUpGoesUpSpeed = 10f;
        PickUpRotateSpeed = 40f;
        pickUpGoesUp = 5f;
        PickUpStartPos = transform.position.y;
        PickUpEndPos = transform.position.y + pickUpGoesUp;
        PlaceNewTileSpeed = 2f;


        //PickUpDic = new Dictionary<Vector2, GameObject>();
        //PickUpDic.Add(new Vector2(1, 0), pickUp);
        //PickUpDic.Add(new Vector2(1, 1), pickUp);
        //GameObject potato;
        //PickUpDic.TryGetValue(new Vector2(1, 0), out potato);
        //Destroy(potato.gameObject);
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(0, Time.deltaTime * PickUpRotateSpeed, 0);

        if (isPicked && !player.GetComponent<Player>().isLerping)
        {
            // set pickUp child of the character so it follows him
            this.transform.SetParent(player.transform);
            isPicked = false;
        }


        //if (isPicked)
        //{
        //    if (Input.GetMouseButton(0))
        //    {

        //        TileEndPos = Input.mousePosition;
        //        TileEndPos.z = 45;
        //        TileEndPos = Camera.main.ScreenToWorldPoint(TileEndPos);

        //        newTile.transform.position = Vector3.Lerp(newTile.transform.position, TileEndPos, PlaceNewTileSpeed * Time.deltaTime);
  
        //    }
        //    if (Vector3.Distance(newTile.transform.position, TileEndPos) < 0.01f)
        //    {
        //        Debug.Log("should be snapped");
        //        isPicked = false;
        //        Destroy(this.gameObject);
        //    }
        //}
        
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == GameObject.Find("Player"))
    //    {
    //        pickUp.transform.position = new Vector3(0, Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), 0);
    //        newTile = Instantiate(tile, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), transform.rotation) as GameObject;
    //        isPicked = true;

    //        //Destroy(gameObject);
    //    }
    //}

    public void triggerPickUp()
    {
        
        // lerp the pick up above player's head
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), transform.position.z);
        isPicked = true;
        




        //newTile = Instantiate(tile, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation) as GameObject;
        //isPicked = true;

        //Destroy(gameObject);

    }
}
