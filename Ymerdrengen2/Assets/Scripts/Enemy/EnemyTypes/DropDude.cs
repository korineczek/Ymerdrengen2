using UnityEngine;
using System.Collections;
using System;

public class DropDude : Enemy {

    int internalX, internalZ;
    float t = 0;

    public bool DestroyTiles = false;
    [Range(1, 3)]
    public int size = 1;
    public float startHeight = 8f;
    public float endHeight = 0.5f;
    public float dropTime = 2f;
    public float deathTime = 1f;

    public override void behavior()
    {
        if(t < 1)
        { 
            t +=  Time.deltaTime * speed / dropTime;
            transform.position = Vector3.Lerp(oldPos, newPos, t);
        }
        else
        {
            hitAllFields();
            t += Time.deltaTime;
            if (t >= 1 + deathTime)
                isDone();
        }
    }

    public override void init()
    {
        setPos(UnityEngine.Random.Range(0, GridData.gridSize), UnityEngine.Random.Range(0, GridData.gridSize));
    }

    void isDone()
    {
        if (DestroyTiles)
            removeTiles();

        transform.position = newPos;
        base.destroyThis();
    }

    public override void init(int x, int y)
    {
        setPos(x, y);
    }

    public void setPos(int x, int y)
    {
        internalX = x;
        internalZ = y;
        oldPos = new Vector3(x + (float)(size) / 2, startHeight, y + (float)(size) / 2);
        transform.position = oldPos;
        newPos = new Vector3(x + (float)(size) / 2, endHeight, y + (float)(size) / 2);
    }

    private void hitAllFields()
    {
        for(int x = 0; x < size; x++)
        {
            for(int z = 0; z < size; z++)
            {
                GridData.gridManager.hitTile((int)(internalX) + x, (int)(internalZ) + z);
            }
        }
    }

    private void removeTiles()
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                Debug.Log(x + ". " + z);
                GridData.gridManager.removeTile((int)(internalX) + x, (int)(internalZ) + z);
            }
        }
    }

    private void blockTiles(bool b)
    {
        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                Debug.Log(x + ". " + z);
                //GridData.gridManager.setTileBlocked((int)(internalX) + x, (int)(internalZ) + z, b);
            }
        }
    }


    public override void init(int x, int y, Direction dir)
    {
        init();
    }
}
