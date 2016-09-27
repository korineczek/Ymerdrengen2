using UnityEngine;
using System.Collections;
using System;

public class DropDude : Enemy {

    Vector3 startPoint;
    Vector3 endPoint;
    float t = 0;

    public float dropTime = 2f;

    public override void behavior()
    {
        t +=  Time.deltaTime / dropTime;
        transform.position = Vector3.Lerp(startPoint, endPoint, t);

        if(t >= 1)
        {
            hasDropped();
        }
    }

    public override void init()
    {
        startPoint = new Vector3(1 + 0.5f, 5, 1 + 0.5f);
        transform.position = startPoint;
        endPoint = new Vector3(1 + 0.5f, 0, 1 + 0.5f);
    }

    void hasDropped()
    {
        transform.position = endPoint;
        GridData.gridManager.hitTile((int)endPoint.x, (int)endPoint.z);
        base.destroyThis();
    }
}
