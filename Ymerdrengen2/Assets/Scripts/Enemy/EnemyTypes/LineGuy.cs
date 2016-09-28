using UnityEngine;
using System.Collections;

public class LineGuy : Enemy
{

    Vector3 vectorDir;
    Direction direction;

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
            newPos += vectorDir;
            t = 0;
        }

    }

    public override void init(int x, int y, Direction dir)
    {
        transform.position = new Vector3(x + GridData.offset, 0, y + GridData.offset);
        direction = dir;

        setDirVector();

        oldPos = transform.position;
        newPos = oldPos + vectorDir;
        transform.LookAt(newPos);
    }

    public override void init(int x, int y)
    {
        init();
    }

    public override void init()
    {
        int rand = Random.Range(0, 3);
        int laneNum = Random.Range(0, 5);
        switch (rand)
        {
            case 0:
                transform.position = new Vector3(20 + GridData.offset, 0, GridData.offset + laneNum);
                direction = Direction.Left;
                break;
            case 1:
                transform.position = new Vector3(-20 - GridData.offset, 0, GridData.offset + laneNum);
                direction = Direction.Right;
                break;
            case 2:
                transform.position = new Vector3(GridData.offset + laneNum, 0, 20 + GridData.offset);
                direction = Direction.Down;
                break;
            case 3:
                transform.position = new Vector3(GridData.offset + laneNum, 0, -20 - GridData.offset);
                direction = Direction.Up;
                break;
        }

        setDirVector();
    }

    public void setDirVector()
    {
        if (direction == Direction.Down)
            vectorDir = new Vector3(0, 0, -1);
        else if (direction == Direction.Up)
            vectorDir = new Vector3(0, 0, 1);
        else if (direction == Direction.Left)
            vectorDir = new Vector3(-1, 0, 0);
        else if (direction == Direction.Right)
            vectorDir = new Vector3(1, 0, 0);
    }
}
