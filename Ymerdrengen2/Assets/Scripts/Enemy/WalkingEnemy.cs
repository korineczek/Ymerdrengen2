using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    Left, Right, Up, Down
}

public class WalkingEnemy : Enemy {

    protected Vector3 vectorDir;
    protected Direction direction;
    private GameObject dirIndicator;

    public bool enableDirectionIndication = true;
    public int dirIndicatorLength = 3;

    public override void behavior() { } // DO nothing

    public override void init(string name)
    {
        this.name = name;
        int rand = UnityEngine.Random.Range(0, 3);
        int laneNum = UnityEngine.Random.Range(0, 5);
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

    public override void init(int x, int y, string name)
    {
        init(name);
    }

    public override void init(int x, int y, Direction dir, string name)
    {
        this.name = name;
        transform.position = new Vector3(x + GridData.offset, 0, y + GridData.offset);
        direction = dir;

        setDirVector();

        oldPos = transform.position;
        newPos = oldPos + vectorDir;
        transform.LookAt(newPos);
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

    protected void revereseDirection()
    {
        Debug.Log(direction);
        switch (direction)
        {
            case Direction.Down: direction = Direction.Up; break;
            case Direction.Up: direction = Direction.Down; break;
            case Direction.Left: direction = Direction.Right; break;
            case Direction.Right: direction = Direction.Left; break;
        }
        transform.Rotate(new Vector3(0, 180, 0));
        setDirVector();
    }

    protected void enableDirectionIndicator(float x, float y)
    {
        if (enableDirectionIndication)
        {
            if (dirIndicator == null)
            {
                Debug.Log("No direction indicator could be loaded");
                return;
            }

            dirIndicator.SetActive(true);
            dirIndicator.transform.position = new Vector3(x, -0.01f, y) - vectorDir/2;

            if (direction == Direction.Down || direction == Direction.Up)
                dirIndicator.transform.Rotate(0, 0, 90);

            enableDirectionIndication = false;
        }
    }

    protected void disAbledirectionIndicator()
    {
        Destroy(dirIndicator);
    }

    protected void loadDirIndicator()
    {
        if (enableDirectionIndication)
        {
            dirIndicator = Instantiate(Resources.Load<GameObject>("Prefabs/" + name + "DirInd"));
            dirIndicator.SetActive(false);
        }
    }
}
