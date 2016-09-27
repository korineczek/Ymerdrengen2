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

    GameObject pickUp;
    GameObject tile;
    GameObject newTile;
    GameObject player;
    bool isPicked;
    Vector3 TileEndPos;

    // Use this for initialization
    void Start () {
        tile = GameObject.FindGameObjectWithTag("Tile");
        isPicked = false;
        pickUp = GameObject.FindGameObjectWithTag("PickUp");
        pickUp.transform.position = new Vector3(pickUp.transform.position.x, pickUp.transform.position.y + 0.3f, pickUp.transform.position.z);
        player = GameObject.FindGameObjectWithTag("Player");
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
                //newTile.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                TileEndPos = Input.mousePosition;
                TileEndPos.z = 45;
                TileEndPos = Camera.main.ScreenToWorldPoint(TileEndPos);

                //}

                //if (newTile != null)
                //{
                newTile.transform.position = Vector3.Lerp(newTile.transform.position, TileEndPos, PlaceNewTileSpeed * Time.deltaTime);
            }
            if(newTile.transform.position == TileEndPos)
            {
                isPicked = false;
            }
        }
        
    }

    

    //public void PickUp()
    //{
    //    pickUp.transform.position = new Vector3(Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpSpeed), 0, 0);

    //    Instantiate(tile, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameObject.Find("Player"))
        {
            pickUp.transform.position = new Vector3(0, Mathf.Lerp(PickUpStartPos, PickUpEndPos, Time.deltaTime * PickUpGoesUpSpeed), 0);
            newTile = Instantiate(tile, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), transform.rotation) as GameObject;
            isPicked = true;
            
            //Destroy(gameObject);
        }
    }
}
