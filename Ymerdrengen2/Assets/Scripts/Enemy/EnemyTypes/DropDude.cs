using UnityEngine;
using System.Collections;
using System;

public class DropDude : Enemy {

    int internalX, internalZ;
    float t = 0;

    public bool DestroyTiles = false;
    public bool BlockTiles = true;
    bool blockedTiles = false; //Flag indicating if the tiles has already been blocked
    [Range(1, 3)]
    public int size = 1;
    public float startHeight = 8f;
    public float endHeight = 0.5f;
    public float dropTime = 2f;
    public float deathTime = 1f;

    Animator anim;

    void animationControl()
    {
        if (anim != null && anim.GetCurrentAnimatorStateInfo(0).IsName("EndState"))
        {
            GameObject CherrySplosion = Instantiate(Resources.Load("Prefabs/CherrySplosion") as GameObject);
            CherrySplosion.transform.position = transform.position;
            isDone();
        }
    }

    void Start()
    {
        anim = transform.GetComponent<Animator>();
    }

    public override void behavior()
    {
        animationControl();

        if (t < 1)
        { 
            t +=  Time.deltaTime * speed / dropTime;
            transform.position = Vector3.Lerp(oldPos, newPos, t);
        }
        else
        {

            hitAllFields();

            //Hit Floor Event
            if (BlockTiles && !blockedTiles)
            {
                //Triggers landing sound
                //GridData.gridManager.triggerLandEvent();
                transform.position = newPos;
                blockTiles(true);
                blockedTiles = true;
            }

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
        Debug.Log("HERE");

        if (BlockTiles)
           blockTiles(false);

        if (DestroyTiles)
            removeTiles();
        
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
                int posX = (int)(internalX) + x;
                int posZ = (int)(internalZ) + z;
                if (GridData.grid[posX, posZ].IsBlocked() != b)
                    GridData.grid[posX, posZ].ToggleFlags(Grid.FieldStatus.Blocked);
                    
            }
        }
    }


    public override void init(int x, int y, Direction dir)
    {
        init();
    }
}
