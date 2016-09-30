using UnityEngine;
using System.Collections;

public class LineGuy : WalkingEnemy
{

    public bool enableDirectionIndicator = true;

    float t = 0;

    GameObject directionIndicator;

    void Start()
    {
        //directionIndicator = transform.FindChild("GlowIndicator").gameObject;
    }

    public override void behavior()
    {

        //Collide with grid tile
        GridData.gridManager.hitTile(round(newPos.x), (round(newPos.z)));

        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(oldPos, newPos, t);

        if (t >= 1)
        {
            oldPos = newPos;
            newPos += vectorDir;
            t = 0;
        }

    }
}
