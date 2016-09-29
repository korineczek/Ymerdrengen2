using UnityEngine;
using System.Collections;
using System;

public enum Direction
{
    Left, Right, Up, Down
}

public abstract class Enemy : MonoBehaviour
{

    protected Vector3 newPos;
    protected Vector3 oldPos;

    float despawnTime = 10;
    float timer = 0;

    public float speed = 0;

    void Update()
    {
        behavior();
        despawnCheck();
    }

    private void despawnCheck()
    {
        timer += Time.deltaTime;
        if (timer >= despawnTime)
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
