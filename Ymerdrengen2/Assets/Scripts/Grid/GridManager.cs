using System;
using UnityEngine;

using Grid;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

    public int gridSize = 5;
    public float offset = 0.5f;

    System.Random rnd = new System.Random();


    GameObject tileObj;

    public Player PlayerCharacter;
    public Vector2 PlayerPosition;

    // Use this for initialization
    void Start()
    {

        initFields();
        initGrid();
        createGridObj();
        //SpawnPickUp();

    }

    void initFields()
    {
        GridData.grid = new Grid<BaseTile>(gridSize);
        GridData.gridSize = gridSize;
        GridData.offset = offset;
        GridData.gridManager = this;
        tileObj = Resources.Load<GameObject>("Prefabs/TileObject");
    }

    void initGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                setTile(x, y, FieldStatus.Floor);
            }
        }

        setTile(2, 2, FieldStatus.None);
        setTile(2, 0, FieldStatus.None);

    }

    void createGridObj()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                //Objects
                if (GridData.grid[x, y].GetValue())
                { 
                    GameObject tile = Instantiate(tileObj, this.transform) as GameObject;
                    tile.transform.position = new Vector3(x + offset, -0.5f, y + offset);
                }
            }
        }
    }

    public void setTile(int x, int y, FieldStatus status)
    {
        BaseTile bt = GridData.grid[x, y];
        bt.Value = status;
        GridData.grid[x, y] = bt;
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
        bool isPlayerHit = PlayerPosition.x == x && PlayerPosition.y == y;
        if (isPlayerHit)
        {
            Debug.Log("Has hit player on tile on (" + x + ", " + y + ")");
            PlayerCharacter.gameObject.SetActive(false);
        }
        return isPlayerHit;
    }

    public void TryMovePlayer(MoveDirection dir)
    {
        if (!PlayerCharacter.gameObject.activeSelf) {
            PlayerCharacter.gameObject.SetActive(true);
            return;
        }
            

        Vector2 newPos = PlayerPosition + TransformMoveDirection(dir);
        bool newPosValue = false;
        try {
            Debug.Log(newPos);
            newPosValue = getTile(newPos).GetValue();
        } catch (IndexOutOfRangeException) {
            Debug.LogWarning("New playerposition outside possible range.");
        }

        if (newPosValue)
        {
            PlayerCharacter.Move(dir);
            PlayerPosition = newPos;
        }
        else {
            PlayerCharacter.gameObject.SetActive(false);
        }
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


    public void SpawnPickUp()
    {
        List<Vector2> FlooredTiles = new List<Vector2>();
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector2 currentTile = new Vector2(x, y);
                if (getTile(currentTile).GetValue())
                {
                    FlooredTiles.Add(currentTile);
                }
            }
        }


        int tileIndex = rnd.Next(FlooredTiles.Count);
        Vector2 nextTile = FlooredTiles[tileIndex];
        

        setTile((int)nextTile.x, (int)nextTile.y, FieldStatus.PickUp);
        createPickUp((int)nextTile.x, (int)nextTile.y);

    }

    void createPickUp(int x, int y)
    {
        if (GridData.grid[x, y].IsPickUp())
        {
            GameObject pickUp = Instantiate(Resources.Load("YogurtCarton") as GameObject);
            pickUp.transform.position = new Vector3(x, 1, y);
        }
     }
        
    

    public BaseTile ToggleFlags(BaseTile tile, FieldStatus flags)
    {
        return new BaseTile() { Value = tile.Value ^ flags }; // '^' ís a bitwise XOR operator.
    }

}
