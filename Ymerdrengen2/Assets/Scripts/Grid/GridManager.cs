using System;
using UnityEngine;

using Grid;
using System.Collections.Generic;
using System.Collections;


[Serializable]
public class GridManager : MonoBehaviour {

    [SerializeField]
    public bool[] FloorInitializer;
    [SerializeField]
    public bool[] YoghurtInitializer;
    [SerializeField]
    public bool[] NewTileInitializer;
    public int gridSize = 7;
    public float offset = 0.5f;
    public int numPickUpsCanCarry;

    System.Random rnd = new System.Random();

    Dictionary<Vector2, GameObject> PickUpDic;
    GameObject tileObj;
    GameObject[] targetPickUp;
    public int PickUpCount;
    public bool possiblePlacement;

    private bool killEventTriggered = false;

    public Player PlayerCharacter;
    public Vector2 PlayerPosition;


    public bool Godmode;
    //GameObjects
    GameObject[,] tileObjects;

    bool leftTile;
    bool behindTile;
    bool rightTile;
    bool frontTile;


    void Awake()
    {
        initFields();
        initPlayer();

        initGrid(FloorInitializer);
        createGridObj();
    }

    // Use this for initialization
    void Start()
    {
        Godmode = GameObject.Find("GodModeObject").GetComponent<GodModeScript>().Godmode;
        PickUpDic = new Dictionary<Vector2, GameObject>();
        numPickUpsCanCarry = PlayerCharacter.GetComponent<Player>().maxYmer + 1;
        targetPickUp = new GameObject[numPickUpsCanCarry];
        PickUpCount = 0;
        possiblePlacement = false;
        TriggerTiles(true);
        if (NewTileInitializer.Length > 0)
            initNewTile(NewTileInitializer);
       

    }

    void Update()
    {
        if(PickUpCount <= 0)
        {
            possiblePlacement = false;
        }
    }

    void initPlayer()
    {
        PlayerCharacter.transform.position = new Vector3(PlayerPosition.x + GridData.offset, 0, PlayerPosition.y + GridData.offset);
    }

    void initFields()
    {
        GridData.grid = new Grid<BaseTile>(gridSize);
        GridData.gridSize = gridSize;
        GridData.offset = offset;
        GridData.gridManager = this;
        tileObj = Resources.Load<GameObject>("Prefabs/TileObject");
    }

    void initGrid(bool[] floorInitializer)
    {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                setTile(x, y, (FieldStatus)Convert.ToInt32(floorInitializer[x + (y * gridSize)]));
            }
        }
    }

    void initPickups(bool[] pickupInitializer)
    {
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (pickupInitializer[x + (y * gridSize)]) {
                    SpawnPickUp(x, y);
                }
            }
        }
    }

    void initNewTile(bool[] newTileInitializer)
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (!getTile(x, y).HasFloor() && newTileInitializer[x + (y * gridSize)])
                {
                    NewTilePossiblePlace(new Vector2(x, y));
                }
            }
        }
    }

    void createGridObj()
    {
        tileObjects = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                //Objects
                if (getTile(x,y).HasFloor())
                { 
                    GameObject tile = Instantiate(tileObj, this.transform) as GameObject;
                    tileObjects[x, y] = tile;
                    tile.transform.position = new Vector3(x + offset, 0, y + offset);
                    if((int)PlayerPosition.x == x && (int)PlayerPosition.y == y)
                    {
                        tile.transform.GetChild(0).gameObject.SetActive(true);
                        tile.transform.GetChild(0).GetComponent<Animator>().SetTrigger("StartTile");
                    }
                }
            }
        }
    }

    public void removeTile(int x, int y)
    {
        if (getTile(x,y).HasFloor())
        {
            Destroy(tileObjects[x, y]);
            tileObjects[x, y] = null;
            getTile(x,y).ToggleFlags(FieldStatus.Floor);
        }
        NewTilePossiblePlace(new Vector2(x, y));

        //if (possiblePlacement)
        //{
        //    object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        //    foreach (object o in obj)
        //    {
        //        GameObject g = (GameObject)o;
        //        if (g.name == "PossTileObject(Clone)")
        //        {
        //            Destroy(g.gameObject);
        //        }
        //    }
        //    NewTileInitializer[x + (y * gridSize)] = true;
        //    initNewTile(NewTileInitializer);
        //}
    }

    public void addTile(int x, int y)
    {   
        if(!getTile(x,y).HasFloor())
        {
            GameObject tile = Instantiate(tileObj, this.transform) as GameObject;
            tileObjects[x, y] = tile;
            tile.transform.position = new Vector3(x + offset, 0f, y + offset);
            tile.transform.GetChild(0).gameObject.SetActive(true);
            tile.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Raise");
            tile.transform.GetChild(0).GetComponent<Animator>().speed = 3;
            getTile(x, y).ToggleFlags(FieldStatus.Floor);
        } 
    }

    public void setTile(int x, int y, FieldStatus status)
    {
        GridData.grid[x, y].Value = status;
        //BaseTile bt = GridData.grid[x, y];
        //bt.Value = status;
        //GridData.grid[x, y] = bt;
    }

    public ITile getTile(int x, int y)
    {
        return GridData.grid[x, y];
    }

    public ITile getTile(Vector2 coord)
    {
        return GridData.grid[(int)coord.x, (int)coord.y];
    }

    public bool hitTile(int x, int y, string monsterTag)
    {
        if (!Godmode)
        {
            bool isPlayerHit = PlayerPosition.x == x && PlayerPosition.y == y;
            if (isPlayerHit && !killEventTriggered)
            {
                killEventTriggered = true;
                killPlayer();
                AudioData.PlaySound(SoundHandle.Death);

                if(monsterTag == "ConeBuddy")
                {
                    GameObject.FindGameObjectWithTag("Progression").GetComponent<LevelProgression>().CoffieDeath();
                }

                if (monsterTag == "DropDude")
                {
                    GameObject.FindGameObjectWithTag("Progression").GetComponent<LevelProgression>().CherryDeath();
                }

                if (monsterTag == "LineGuy")
                {
                    GameObject.FindGameObjectWithTag("Progression").GetComponent<LevelProgression>().PieDeath();
                }

            }
            return isPlayerHit;
        }
        else
            return false;
    }

    public void TryMovePlayer(MoveDirection dir)
    {
        if (!PlayerCharacter.gameObject.activeSelf) {
            //revivePlayer();
            return;
        }
            
        Vector2 newPos = PlayerPosition + TransformMoveDirection(dir);
        Debug.Log("newPos" + newPos);

        bool newPosHasFloor = false;
        try
        {
            newPosHasFloor = getTile(newPos).HasFloor();
        } catch (IndexOutOfRangeException) {
            Debug.LogWarning("New playerposition outside possible range.");
        }

        if (getTile(newPos).IsBlocked())
            return;

        AudioData.PlaySound(SoundHandle.Jump);
        if (newPosHasFloor)
        {
            PlayerCharacter.isLerping = true;
            PlayerCharacter.Move(dir,false);
            PlayerPosition = newPos;

            // if player steps in a tile where a pick up exists
            //if (GridData.grid[(int)newPos.x, (int)newPos.y].IsPickUp())
            //if (getTile(newPos).IsPickUp() && targetPickUp == null) /*this is for carrying only one pickup each time*/
            if (getTile(newPos).IsPickUp() && PickUpCount < numPickUpsCanCarry - 1)
            {
                PickUpCount++;
                // identify which pick up player touches (if there are a lot)
                PickUpDic.TryGetValue(new Vector2((int)newPos.x, (int)newPos.y), out targetPickUp[PickUpCount]);
                // say to the grid that this tile doesn't have a pick up anymore
                getTile(newPos).ToggleFlags(FieldStatus.PickUp);
                // call the triggerPickUp function from PickUpScript
                targetPickUp[PickUpCount].GetComponent<PickUpScript>().TriggerPickUp();
                AudioData.PlaySound(SoundHandle.PowerUp);
                // start blinking possible positions
                //NewTilePossiblePlace(newPos);
                possiblePlacement = true;

                // remove ymer from dict
                PickUpDic.Remove(new Vector2((int)newPos.x, (int)newPos.y));
            }
        }
        //else if (targetPickUp[PickUpCount] != null && possiblePlacement)
        else if (PickUpCount > 0 && getTile(newPos).IsNewTile())
        {
            AudioData.PlaySound(SoundHandle.PlaceTile);
            // add a new tile if there is a charge
            addTile((int)newPos.x, (int)newPos.y);
            // Move the player to the new tile
            PlayerCharacter.isLerping = true;
            PlayerCharacter.Move(dir,false);
            PlayerPosition = newPos;
            // destroy the pick up above player's head
            Destroy(targetPickUp[PickUpCount]);
            // inform counter that you placed a tile
            PickUpCount--;

            object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
            foreach (object o in obj)
            {
                GameObject g = (GameObject)o;
                if (g.name == "PossTileObject(Clone)")
                {
                    Destroy(g.gameObject);
                }
            }
            NewTileInitializer[(int)newPos.x + ((int)newPos.y * gridSize)] = false;
            initNewTile(NewTileInitializer);

            if (getTile(newPos).IsPickUp() && PickUpCount < numPickUpsCanCarry - 1)
            {
                PickUpCount++;
                // identify which pick up player touches (if there are a lot)
                PickUpDic.TryGetValue(new Vector2((int)newPos.x, (int)newPos.y), out targetPickUp[PickUpCount]);
                // say to the grid that this tile doesn't have a pick up anymore
                getTile(newPos).ToggleFlags(FieldStatus.PickUp);
                // call the triggerPickUp function from PickUpScript
                targetPickUp[PickUpCount].GetComponent<PickUpScript>().TriggerPickUp();
                AudioData.PlaySound(SoundHandle.PowerUp);
                // start blinking possible positions
                //NewTilePossiblePlace(newPos);
                possiblePlacement = true;

                // remove ymer from dict
                PickUpDic.Remove(new Vector2((int)newPos.x, (int)newPos.y));
            }
        }
        else {
            if(!Godmode) { 
                AudioData.PlaySound(SoundHandle.FallDeath);
                PlayerCharacter.isLerping = true;
                PlayerCharacter.Move(dir,true);
                Debug.Log("diwjaoida");
                StartCoroutine(falling());
            }
        }
    }

    IEnumerator falling()
    {
        GameObject.Find("Managers").transform.FindChild("inputManager").GetComponent<SwipeManager>().enabled = false;
        yield return new WaitForSeconds(1f);
        GameObject.Find("Managers").transform.FindChild("inputManager").GetComponent<SwipeManager>().enabled = true;
        killPlayer();

    }

    public void killPlayer()
    {
        PlayerCharacter.GetComponent<Player>().loseYogurt();
        PlayerCharacter.gameObject.SetActive(false);
        //Debug.Log("diwjaoida");

        GameObject.FindGameObjectWithTag("Progression").GetComponent<LevelProgression>().Death();
        AudioData.StopMusic();
    }

    public void revivePlayer()
    {
        PlayerCharacter.gameObject.SetActive(true);
    }

    public Vector2 TransformMoveDirection(MoveDirection dir)
    {
        switch (dir) {
            case MoveDirection.LeftUp:      return new Vector2(0, 1);
            case MoveDirection.RightUp:     return new Vector2(1, 0);
            case MoveDirection.RightDown:   return new Vector2(0, -1);
            case MoveDirection.LeftDown:    return new Vector2(-1, 0);
            default: throw new Exception("ERROR: Enum had unrecognizable value.");
        }
    }

    public void SpawnPickUp(int x, int y)
    {
        getTile(x, y).ToggleFlags(FieldStatus.PickUp);
        createPickUp(x, y);
    }

    public void SpawnPickUp()
    {
        List<Vector2> FlooredTiles = new List<Vector2>();

        // go through all the tiles and save into a list the ones that have floor
        // and the ones that have a pick up
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2 currentTile = new Vector2(x, y);
                if (getTile(currentTile).HasFloor() && !getTile(currentTile).IsPickUp())
                {
                    FlooredTiles.Add(currentTile);
                }
            }
        }

        // generate a pickup on a random tile
        int tileIndex = rnd.Next(FlooredTiles.Count);
        Vector2 nextTilePos = FlooredTiles[tileIndex];
        ITile nextTile = getTile(nextTilePos);

        nextTile.ToggleFlags(FieldStatus.PickUp);
        //setTile((int)nextTile.x, (int)nextTile.y, FieldStatus.PickUp);

        createPickUp((int)nextTilePos.x, (int)nextTilePos.y);

    }

    public void createPickUp(int x, int y)
    {
        if ((int)PlayerPosition.x == x && (int)PlayerPosition.y == y && PickUpCount < numPickUpsCanCarry - 1)
        {
            GameObject pickUp = Instantiate(Resources.Load("Prefabs/ymerkarton") as GameObject);
            pickUp.transform.position = new Vector3(x + offset, 0, y + offset);
            PickUpCount++;
            targetPickUp[PickUpCount] = pickUp;
            targetPickUp[PickUpCount].GetComponent<PickUpScript>().TriggerPickUp();
            AudioData.PlaySound(SoundHandle.PowerUp);
            possiblePlacement = true;


        }
        else if (!getTile(x,y).IsPickUp())
        {
            // instantiate the pick up on the randomly chosen tile
            GameObject pickUp = Instantiate(Resources.Load("Prefabs/ymerkarton") as GameObject);
            // put pick up on the center of the tile
            pickUp.transform.position = new Vector3(x + offset, 0, y + offset);
            getTile(x, y).ToggleFlags(FieldStatus.PickUp);
            // associate the pickup with its coordinates (so we know which one to destroy when picked)
            PickUpDic.Add(new Vector2(x, y), pickUp);
        }

    }

    public void NewTilePossiblePlace(Vector2 pos)
    {
        // this is used for sandra to choose which positions to glow
        GameObject possibleTile = Instantiate(Resources.Load("Prefabs/PossTileObject") as GameObject);
        possibleTile.transform.position = new Vector3(pos.x + offset, 0, pos.y + offset);

        if (!getTile(pos).IsNewTile())
            getTile(pos).ToggleFlags(FieldStatus.NewTile);

        // this is used for glowing all possible positions
        //for (int x = 0; x < gridSize; x++)
        //{
        //    for (int y = 0; y < gridSize; y++)
        //    {
        //        if (x > 1)
        //        {
        //            leftTile = getTile(x - 1, y).HasFloor();
        //        }
        //        if (y > 1)
        //        {
        //            behindTile = getTile(x, y - 1).HasFloor();
        //        }
        //        if (x < 5)
        //        {
        //            rightTile = getTile(x + 1, y).HasFloor();
        //        }
        //        if (y < 5)
        //        {
        //            frontTile = getTile(x, y + 1).HasFloor();
        //        }

        //        if (leftTile || behindTile || rightTile || frontTile)
        //        {
        //            GameObject possibleTile = Instantiate(Resources.Load("Prefabs/PossTileObject") as GameObject);
        //            possibleTile.transform.position = new Vector3(x + offset, 0, y + offset);
        //        }
        //    }
        //}
    }

    /// <summary>
    /// Triggers events that are associated with landing of drop dude
    /// </summary>
    public void triggerLandEvent()
    {
        Debug.Log("Triggered landing event");
    }

    /// <summary>
    /// Triggers events that are associated with killing the character
    /// </summary>
    public void triggerKillEvent()
    {
        Debug.Log("Triggered kill event");
    }

    /// <summary>
    /// Triggers events that are associated with the conebuddy fire
    /// </summary>
    internal void triggerConeFireEvent()
    {
        Debug.Log("Triggered kill event");
    }

    public void DropTiles()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {

                if (GridData.grid[x, y].HasFloor()) {

                    tileObjects[x, y].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Drop");
                    //Debug.Log(tileObjects[x, y].transform.GetChild(0).name);
                }
            }
        }
    }

    SortedDictionary<int, List<GameObject>> listVecs;

    public void TriggerTiles(bool started)
    {

        listVecs = new SortedDictionary<int, List<GameObject>>();
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {

                if (GridData.grid[x, y].HasFloor())
                {

                    int mag = (int)Vector2.SqrMagnitude(new Vector2(x, y) - PlayerPosition);
                    if (!started)
                    {
                        mag *= -1;
                    }
                    Vector2 vec = new Vector2(x, y);
                    if (mag != 0) {
                        if (listVecs.ContainsKey(mag))
                        {
                            listVecs[mag].Add(tileObjects[x, y]);
                        }
                        else
                        {
                            listVecs.Add(mag, new List<GameObject> { tileObjects[x, y] });
                        }
                    }
                }
            }
        }
        StartCoroutine(trigger(started));
    }

    IEnumerator trigger(bool started) {


            foreach (var entry in listVecs)
            {
            yield return new WaitForSeconds(0.1f);

                foreach (var tile in entry.Value)
                {
                    tile.transform.GetChild(0).gameObject.SetActive(true);
                    if(started)
                        tile.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Raise");
                    else
                        tile.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Drop");

                }
            
        }
    }

}
