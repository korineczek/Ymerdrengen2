using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    Left, Right, Up, Down
}

public abstract class Enemy : MonoBehaviour
{

    float despawnTime = 10;
    float t = 0;

    public float speed = 0;

    public Direction direction;
    int[][] attackPattern;

    void Update()
    {
        behavior();
        despawnCheck();
    }

    private void despawnCheck()
    {
        t += Time.deltaTime;
        if (t >= despawnTime)
            destroyThis();
    }

    public void destroyThis()
    {
        Destroy(this.gameObject);
    }

    public abstract void behavior();
    public abstract void init();
    public abstract void init(int x, int y);
    public abstract void init(int x, int y, Direction dir);

}
