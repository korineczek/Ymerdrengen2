using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Position on the grid in local space (Worldspace(X,Z) = Localspace(X,Y))
    /// </summary>
    public Vector2 Position;

    void Start()
    {

    }

    public void Move(MoveDirection dir)
    {
        switch (dir) {
            case MoveDirection.LeftUp: {
                    transform.Translate(0, 0, 1);
                    break;
                }
            case MoveDirection.RightUp: {
                    transform.Translate(1, 0, 0);
                    break;
                }
            case MoveDirection.RightDown: {
                    transform.Translate(0, 0, -1);
                    break;
                }
            case MoveDirection.LeftDown: {
                    transform.Translate(-1, 0, 0);
                    break;
                }
            default: throw new System.Exception("ERROR: Enum had unrecognizable value.");
        }
    }
}
