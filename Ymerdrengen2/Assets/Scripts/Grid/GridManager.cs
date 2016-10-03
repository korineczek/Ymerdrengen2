using System;
using UnityEngine;

using Grid;
using System.Collections.Generic;

[Serializable]
public class GridManager : MonoBehaviour {

    [SerializeField]
    public bool[] FloorInitializer;
    [SerializeField]
    public bool[] YoghurtInitializer;
    public int gridSize = 7;
    public float offset = 0.5f;
    public int numPickUpsCanCarry;

    System.Random rnd = new System.Random();

    Dictionary<Vector2, GameObject> PickUpDic;
    GameObject tileObj;
    GameObject[] targetPickUp;
    int PickUpCount;
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
        numPickUpsCanCarry = 3;
        targetPickUp = new GameObject[numPickUpsCanCarry];
        PickUpCount = 0;
        possiblePlacement = false;

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
    }

    public void addTile(int x, int y)
    {   
        if(!getTile(x,y).HasFloor())
        {
            GameObject tile = Instantiate(tileObj, this.transform) as GameObject;
            tileObjects[x, y] = tile;
            tile.transform.position = new Vector3(x + offset, 0f, y + offset);
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

    public bool hitTile(int x, int y)
    {
        if (!Godmode)
        {
            bool isPlayerHit = PlayerPosition.x == x && PlayerPosition.y == y;
            if (isPlayerHit && !killEventTriggered)
            {
                killEventTriggered = true;
                killPlayer();
                AudioData.PlaySound(SoundHandle.Death);
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
            PlayerCharacter.Move(dir);
            PlayerPosition = newPos;

            // if player steps in a tile where a pick up exists
            //if (GridData.grid[(int)newPos.x, (int)newPos.y].IsPickUp())
            //if (getTile(newPos).IsPickUp() && targetPickUp == null) /*this is for carrying only one pickup each time*/
            if (getTile(newPos).IsPickUp())
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
                NewTilePossiblePlace(newPos);
                // remove ymer from dict
                PickUpDic.Remove(new Vector2((int)newPos.x, (int)newPos.y));
            }
        }
        //else if (targetPickUp[PickUpCount] != null && possiblePlacement)
        else if (PickUpCount > 0)
        {
            AudioData.PlaySound(SoundHandle.PlaceTile);
            // add a new tile if there is a charge
            addTile((int)newPos.x, (int)newPos.y);
            // Move the player to the new tile
            PlayerCharacter.isLerping = true;
            PlayerCharacter.Move(dir);
            PlayerPosition = newPos;
            // destroy the pick up above player's head
            Destroy(targetPickUp[PickUpCount]);
            // inform counter that you placed a tile
            PickUpCount--;
        }
        else {
            if(!Godmode) { 
                killPlayer();
                AudioData.PlaySound(SoundHandle.FallDeath);
            }
        }
    }

    public void killPlayer()
    {
        PlayerCharacter.GetComponent<Player>().loseYogurt();
        PlayerCharacter.gameObject.SetActive(false);

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
        if (!getTile(x,y).IsPickUp())
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

    [Obsolete("Use ITile.ToggleFlags(FieldStatus) instead.")]
    public void ToggleFlags(Vector2 tilePos, FieldStatus flags)
    {
        var curTile = GridData.grid[(int)tilePos.x, (int)tilePos.y];
        GridData.grid[(int)tilePos.x, (int)tilePos.y] = ToggleFlags(GridData.grid[(int)tilePos.x, (int)tilePos.y], FieldStatus.PickUp);
        //GridData.grid[(int)tilePos.x, (int)tilePos.y] = new BaseTile() { Value = curTile.Value ^ flags };
    }

    [Obsolete("Use ITile.ToggleFlags(FieldStatus) instead.")]
    public BaseTile ToggleFlags(BaseTile tile, FieldStatus flags)
    {
        return new BaseTile() { Value = tile.Value ^ flags }; // '^' ís a bitwise XOR operator.
    }

    public void NewTilePossiblePlace(Vector2 newPos)
    {
        possiblePlacement = true;
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {

                //if (!getTile(x, y).HasFloor())
                //{
                //    GameObject possibleTile = Instantiate(Resources.Load("Prefabs/PossTileObject") as GameObject);
                //    possibleTile.transform.position = new Vector3(x + offset, -offset, y + offset);
                //}

                if (x > 1)
                {
                    leftTile = getTile(x - 1, y).HasFloor();
                }
                if (y > 1)
                {
                    behindTile = getTile(x, y - 1).HasFloor();
                }
                if (x < 5)
                {
                    rightTile = getTile(x + 1, y).HasFloor();
                }
                if (y < 5)
                {
                    frontTile = getTile(x, y + 1).HasFloor();
                }

                if (leftTile || behindTile || rightTile || frontTile)
                {
                    GameObject possibleTile = Instantiate(Resources.Load("Prefabs/PossTileObject") as GameObject);
                    possibleTile.transform.position = new Vector3(x + offset, 0, y + offset);
                }
            }
        }
    }

    /// <summary>
    /// Triggers events that are associated with landing of drop dude
    /// </summary>
    public void triggerLandEvent()
    {
        AudioData.PlaySound(SoundHandle.TomatoSplat);
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

}
