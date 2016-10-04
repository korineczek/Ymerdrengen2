using UnityEngine;
using System.Collections;

public class LineGuy : WalkingEnemy
{

    float t = 0;

    GameObject directionIndicator;

    void Start()
    {
        AudioData.PlaySound(SoundHandle.PieCharge, gameObject);
        loadDirIndicator();
        indicatorCalc();
    }

    public override void behavior()
    {

        //Collide with grid tile
        GridData.gridManager.hitTile(round(newPos.x), (round(newPos.z)), "LineGuy");

        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(oldPos, newPos, t);

        if (t >= 1)
        {
            t = 0;
            oldPos = newPos;
            newPos += vectorDir;
            indicatorCalc();
        }
    }

    void indicatorCalc()
    {
        if (!enableDirectionIndication && checkTile(newPos.x, newPos.z))
        {
            disAbledirectionIndicator();
            return;
        }
            
        for(int i = 0; i < dirIndicatorLength; i++)
        {
            float x = newPos.x + vectorDir.x * i;
            float z = newPos.z + vectorDir.z * i;

            if (checkTile(x, z))
            {
                enableDirectionIndicator(x, z);
                return;
            }
        }
        
    }
}
