using UnityEngine;
using System.Collections;
using System;

public class ConeBuddy : Enemy
{
    Vector3 vectorDir;
    Direction direction;

    float t = 0;
    float timer = 0;

    bool hold = false;
    public float holdTime = 2f;

    /*
     *  Controls the shape of attack of the conebuddy
     */
    int startPointX = -2; //DO NOT TOUCH
    int[] dirLookup = { -90, 90, 0, 180 }; //DO NOT TOUCH
    bool[,] attackPattern = { { false, false, true, false, false},
                              { false, true, true, true, false },
                              { true, true, true, true, true } };

    public override void behavior()
    {

        timer += Time.deltaTime;
        if (!hold) { 
            t += timer * speed;
            transform.position = Vector3.Lerp(oldPos, newPos, t);

            if (t >= 1 && !hold)
            {
                oldPos = newPos;
                newPos += vectorDir;

                t = 0;
                timer = 0;

                hold = base.checkNextTile();

                if (hold) fire();
            }
        }
        else if (hold && timer > holdTime)
        {
            revereseDirection();
            Debug.Log(vectorDir);
            newPos = oldPos + vectorDir;
            hold = false;
            timer = 0;
        }
    }

    private void fire()
    {

        for(int x = 0; x < attackPattern.GetLength(1); x++)
        {
            for(int z = 0; z < attackPattern.GetLength(0); z++)
            {
                if (attackPattern[z, x])
                {
                    Vector3 rotVector = new Vector3((x + oldPos.x + startPointX) - oldPos.x, 0, (1 + z + oldPos.z) - oldPos.z);
                    rotVector = Quaternion.AngleAxis((float)dirLookup[(int)direction], Vector3.up) * rotVector;
                    Vector3 point = rotVector + oldPos;
                    int intX = round(point.x);
                    int intZ = round(point.z);
                    if (x == 2)
                        SPAWNCUBE(point, z + 1);
                    if (intX >= 0 && intZ >= 0 && intX < GridData.gridSize && intZ < GridData.gridSize)
                    {
                        if (GridData.grid[intX, intZ].GetValue())
                        {
                            GridData.gridManager.hitTile(intX, intZ);
                        }
                    }
                }
            }
        }
    }

    void SPAWNCUBE(Vector3 p, float f)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = p;
        
        if(direction == Direction.Down || direction == Direction.Up)
            cube.transform.localScale = new Vector3(f, 0.5f, 0.5f);
        else
            cube.transform.localScale = new Vector3(0.5f, 0.5f, f);

        cube.transform.SetParent(this.transform);
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

    void revereseDirection()
    {
        Debug.Log(direction);
        switch (direction)
        {
            case Direction.Down:  direction = Direction.Up; break;
            case Direction.Up:    direction = Direction.Down; break;
            case Direction.Left:  direction = Direction.Right; break;
            case Direction.Right: direction = Direction.Left; break;
        }
        Debug.Log(direction);
        setDirVector();
    }
}
