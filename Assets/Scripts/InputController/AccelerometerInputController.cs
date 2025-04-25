using UnityEngine;
using System.Collections;


public class AccelerometerInputController : IInputController
{

    private const float OFFSET = 0.5f;
    private const float MIN_DELTA = 0.25f;


    private bool canCheck = true;


    public bool CheckInput(Vector3 emptyTilePosition, ref Vector3 hitPosition)
    {
        Vector2 delta = new Vector2(Input.acceleration.x, Input.acceleration.y + OFFSET);

        if (Mathf.Abs(delta.x) < MIN_DELTA && Mathf.Abs(delta.y) < MIN_DELTA)
        {
            canCheck = true;
            return false;
        }

        if (!canCheck)
            return false;

        canCheck = false;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            hitPosition = new Vector3(emptyTilePosition.x + Constant.TILE_SIZE * Mathf.Sign(delta.x) * -1, emptyTilePosition.y);            
            return true;
        }
        else
        {
            hitPosition = new Vector3(emptyTilePosition.x, emptyTilePosition.y + Constant.TILE_SIZE * Mathf.Sign(delta.y) * -1);
            return true;
        }
    }
}
