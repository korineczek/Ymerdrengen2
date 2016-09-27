using UnityEngine;
using System.Collections;

using Grid;

public class GridManager : MonoBehaviour {

    public int gridSize = 5;
    public float offset = 0.5f;

    GameObject tileObj;

    // Use this for initialization
    void Start()
    {

        initFields();
        initGrid();
        createGridObj();

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
                    tile.transform.position = new Vector3(x + offset, 0, y + offset);
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

    public bool hitTile(int x, int y)
    {
        Debug.Log("Has hit tile on (" + x + ", " + y + ")");
        return false;
    }
}
