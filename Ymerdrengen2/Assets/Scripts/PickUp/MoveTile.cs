using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public void Move(MoveDirection dir, int NumberofTiles)
    {
        switch (dir)
        {
            case MoveDirection.LeftUp:
                {
                    transform.Translate(0, 0, NumberofTiles);
                    break;
                }
            case MoveDirection.RightUp:
                {
                    transform.Translate(NumberofTiles, 0, 0);
                    break;
                }
            case MoveDirection.RightDown:
                {
                    transform.Translate(0, 0, -NumberofTiles);
                    break;
                }
            case MoveDirection.LeftDown:
                {
                    transform.Translate(-NumberofTiles, 0, 0);
                    break;
                }
            default: throw new System.Exception("ERROR: Enum had unrecognizable value.");
        }
    }
}
