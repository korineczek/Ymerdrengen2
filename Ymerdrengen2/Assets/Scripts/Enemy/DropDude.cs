using UnityEngine;
using System.Collections;
using System;

public class DropDude : Enemy {

    Vector3 startPoint;
    Vector3 endPoint;
    float t = 0;

    public float startHeight = 8f;
    public float dropTime = 2f;
    public float deathTime = 1f;

    public override void behavior()
    {
        if(t < 1)
        { 
            t +=  Time.deltaTime * speed / dropTime;
            transform.position = Vector3.Lerp(startPoint, endPoint, t);
        }
        else
        {
            Debug.Log(t);
            GridData.gridManager.hitTile((int)endPoint.x, (int)endPoint.z);
            t += Time.deltaTime;
            if (t >= 1 + deathTime)
                hasDropped();
        }
    }

    public override void init()
    {
        int randX = UnityEngine.Random.Range(0, GridData.gridSize);
        int randY = UnityEngine.Random.Range(0, GridData.gridSize);
        startPoint = new Vector3(randX + GridData.offset, startHeight, randY + GridData.offset);
        transform.position = startPoint;
        endPoint = new Vector3(randX + GridData.offset, 0, randY + GridData.offset);
    }

    void hasDropped()
    {
        transform.position = endPoint;
        base.destroyThis();
    }

    public override void init(int x, int y)
    {
        startPoint = new Vector3(x + GridData.offset, startHeight, y + GridData.offset);
        transform.position = startPoint;
        endPoint = new Vector3(x + GridData.offset, 0, y + GridData.offset);
    }

    public override void init(int x, int y, Direction dir)
    {
        init();
    }
}
