using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour {


    bool edgedL;
    bool edgedR;
    // Use this for initialization
    void Start () {
        edgedR = false;
        edgedL = false;
    }

    // Update is called once per frame
    void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        move(x, y, ref edgedL);
        x = Input.GetAxis("RightH");
        y = Input.GetAxis("RightV");
        move(x, y, ref edgedR);


    }

    void move(float x, float y, ref bool edged)
    {
        float magx = Mathf.Abs(x);
        float magy = Mathf.Abs(y);

        if (Mathf.Sqrt(Mathf.Pow(magx, 2) + Mathf.Pow(magy, 2)) > 0.8)
        {
            if (!edged)
            {
                if (x >= 0 && y >= 0)
                {
                    GridData.gridManager.TryMovePlayer(MoveDirection.RightUp);
                }
                else if (x <= 0 && y >= 0)
                {
                    GridData.gridManager.TryMovePlayer(MoveDirection.LeftUp);
                }
                else if (x >= 0 && y <= 0)
                {
                    GridData.gridManager.TryMovePlayer(MoveDirection.RightDown);
                }
                else if (x <= 0 && y <= 0)
                {
                    GridData.gridManager.TryMovePlayer(MoveDirection.LeftDown);
                }
                edged = true;
            }
        }
        else
        {
            edged = false;
        }
    }
}
