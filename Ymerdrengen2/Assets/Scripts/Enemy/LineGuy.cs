using UnityEngine;
using System.Collections;
using System;

public class LineGuy : Enemy
{

    Vector3 newPos;
    Vector3 oldPos;

    Vector3 dir;

    float t = 0;

    public override void behavior()
    {

        //Collide with grid tile
        GridData.gridManager.hitTile((int)newPos.x, (int)newPos.z);

        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(oldPos, newPos, t);

        if (t >= 1)
        {
            oldPos = newPos;
            newPos += dir;
            t = 0;
        }

    }

    public override void init()
    {
        if (base.direction == Direction.Down)
        {
            dir = new Vector3(0, 0, -1);
            
        }else if (base.direction == Direction.Up)
        {
            dir = new Vector3(0, 0, 1);
        }
        else if (base.direction == Direction.Left)
        {
            dir = new Vector3(-1, 0, 0);
        }
        else if (base.direction == Direction.Right)
        {
            dir = new Vector3(1, 0, 0);
        }
        oldPos = transform.position;
        newPos = oldPos + dir;
        transform.LookAt(newPos);
    }
}
