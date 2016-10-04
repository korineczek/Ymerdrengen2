using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public float ShakeVelocity = 10;

    public float ShakeDuration = 1;

    public float ShakeIntensity = 0.1f;

    public float ExplosionShakeVelocity = 4;

    public bool ExplosionShake = true;

    public enum Orientation
    {
        horizontal = 0, vertical = 1
    }
    public Orientation ShakeOrientation;

    public bool ShakeOtherDirection = false;


    public void startShake(bool isExplosion)
    {
        StartCoroutine(startCoroutineShake(isExplosion));
    }

    // Usage StartCoroutine(startShake(ShakeOrientation, ShakeOtherDirection));
    public IEnumerator startCoroutineShake(bool isExplosion = false)
    {

        Vector3 direction;
        //horizontal shake
        if (ShakeOrientation == 0)
        {
            //start randomly
            direction = Vector3.left;
            if(Random.Range(0,2) > 0)
            {
                direction = Vector3.right;
            }
        }
        else
        {
            direction = Vector3.up;
            if (Random.Range(0, 2) > 0)
            {
                direction = Vector3.down;
            }
        }

        //need to regain control of the camera
        float elapsed = 0;
        int shakeTurn = 0;
        float oldShakeVelocity = 0;
        float oldX = 0;
        float oldY = 0;
        while (elapsed < ShakeDuration || (shakeTurn % 2 == 0))
        {

            elapsed += Time.deltaTime * 4;

            float percentComplete = ShakeDuration / elapsed;
            //adjust here

            shakeTurn++;

            //Debug.Log(shakeTurn);

            Vector3 currentDirection = direction;

            float currentShakeVelocity = ShakeVelocity * (percentComplete * Time.deltaTime);

            if (ExplosionShake && isExplosion)
            {
                currentShakeVelocity = ExplosionShakeVelocity * (percentComplete * Time.deltaTime);
            }
            
            
            //reverse direction
            if (shakeTurn % 2 != 0)
            {
                currentDirection.x *= -1;
                currentDirection.y *= -1;
                currentShakeVelocity = oldShakeVelocity;
            }

            oldShakeVelocity = currentShakeVelocity;
            
            //Debug.Log("PERCENT COMPLTE: " + percentComplete);
            //Debug.Log("Elapsed: " + elapsed + " Turn: " + shakeTurn + " Velocity:" + currentShakeVelocity);


            yield return new WaitForSeconds(ShakeIntensity);
            //shake
            Camera.main.transform.position += currentShakeVelocity * currentDirection;

            //shake in random directions too
            if (ShakeOtherDirection)
            {
                float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
                // map value to [-1, 1]
                float x = Random.value * 2.0f - 1.0f;
                float y = Random.value * 2.0f - 1.0f;

                float magnitude = 1f;
                x *= magnitude * damper;
                y *= magnitude * damper;


                if (shakeTurn % 2 != 0) {
                    x = oldX *(-1);
                    y = oldY *(-1);

                }
                oldX = x;
                oldY = y;

                //Debug.Log("DAMPER X :" + x + " Y: " + y);

                Camera.main.transform.position += (currentShakeVelocity / 4) * new Vector3(x, y, 0);
            }
        }
 
    }
}
