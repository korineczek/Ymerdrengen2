using System.Collections;
using UnityEngine;

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

    public MoveTile NewTile;

    GameObject pickUp;
    GameObject tile;
    GameObject newTile;
    GameObject player;
    bool isPicked;
    Vector3 TileEndPos;

    // Use this for initialization
    void Start () {
        tile = GameObject.Find("TileObject");
        isPicked = false;
        pickUp = GameObject.FindGameObjectWithTag("PickUp");
        pickUp.transform.position = new Vector3(pickUp.transform.position.x, pickUp.transform.position.y + 0.3f, pickUp.transform.position.z);
        player = GameObject.Find("Character");
        PickUpGoesUpSpeed = 10f;
        PickUpRotateSpeed = 40f;
        pickUpGoesUp = 5f;
        PickUpStartPos = pickUp.transform.position.y;
        PickUpEndPos = player.transform.position.y + pickUpGoesUp;
        PlaceNewTileSpeed = 2f;
        //isPickUp = false;

    }

    // Update is called once per frame
    void Update() {
        pickUp.transform.Rotate(0, Time.deltaTime * PickUpRotateSpeed, 0);


        if (isPicked)
        {
            if (Input.GetMouseButton(0))
            {
                //NewTile.Move(MoveDirection.LeftUp, 3);

                TileEndPos = Input.mousePosition;
                TileEndPos.z = 45;
                TileEndPos = Camera.main.ScreenToWorldPoint(TileEndPos);

                newTile.transform.position = Vector3.Lerp(newTile.transform.position, TileEndPos, PlaceNewTileSpeed * Time.deltaTime);
  
            }
            if (Vector3.Distance(newTile.transform.position, TileEndPos) < 0.01f)
            {
                Debug.Log("should be snapped");
                isPicked = false;
                Destroy(pickUp.gameObject);
            }
        }
        
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

    void triggerPickUp()
    { 
            pickUp.transform.position = new Vector3(0, Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), 0);
            newTile = Instantiate(tile, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation) as GameObject;
            isPicked = true;

            //Destroy(gameObject);
        
    }
}
