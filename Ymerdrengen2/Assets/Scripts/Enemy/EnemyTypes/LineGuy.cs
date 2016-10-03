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
        GridData.gridManager.hitTile(round(newPos.x), (round(newPos.z)));

        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(oldPos, newPos, t);

        if (t >= 1)
        {
            t = 0;
            oldPos = newPos;
            newPos += vectorDir;
            indicatorCalc();
        }

        var maxDistance = Vector3.Distance(new Vector3(3, 35, 0), GridData.gridManager.PlayerCharacter.transform.position);
        var distance = Vector3.Distance(transform.position, GridData.gridManager.PlayerCharacter.transform.position);
        var normalizedDistance = (distance / maxDistance) * 100f; // 100f because we normalize between 0 and 100.
        //Debug.Log(string.Format("Distance: {0}; Normalized: {1}", distance, normalizedDistance));
        //Debug.Log(string.Format("{0}: {1} - {2} = {3}", gameObject.name, transform.position, Vector3.zero, distance));
        AudioData.SetSoundParameter(SoundParameterHandle.Distance, normalizedDistance, gameObject);
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
