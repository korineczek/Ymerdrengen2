using UnityEngine;
using System.Collections;
using System;

public abstract class Enemy : MonoBehaviour
{

    protected string name;

    protected Vector3 newPos;
    protected Vector3 oldPos;

    float despawnTime = 10;
    float timer = 0;

    public float speed = 0;

    void Update()
    {
        var maxDistance = Vector3.Distance(new Vector3(3, 40, 0), GridData.gridManager.PlayerCharacter.transform.position);
        var distance = Vector3.Distance(transform.position, GridData.gridManager.PlayerCharacter.transform.position);
        var normalizedDistance = (distance / maxDistance) * 100f; // 100f because we normalize between 0 and 100.

        AudioData.SetSoundParameter(SoundParameterHandle.Distance, normalizedDistance, gameObject);
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

    protected bool checkNextTile()
    {
        if (round(newPos.x) >= 0 && round(newPos.z) >= 0 && round(newPos.x) < GridData.gridSize && round(newPos.z) < GridData.gridSize)
        {
            if (GridData.grid[round(newPos.x), round(newPos.z)].HasFloor())
            {
                return true;
            }
        }
        return false;
    }

    protected bool checkTile(float x, float z)
    {
        if (round(x) >= 0 && round(z) >= 0 && round(x) < GridData.gridSize && round(z) < GridData.gridSize)
        {
            if (GridData.grid[round(x), round(z)].HasFloor())
            {
                return true;
            }
        }
        return false;
    }

    protected int round(float f)
    {
        if (f >= 0)
            return (int)f;
        else
            return (int)f - 1;
    }

    public abstract void behavior();
    public abstract void init(string name);
    public abstract void init(int x, int y, string name);
    public abstract void init(int x, int y, Direction dir, string name);

}
