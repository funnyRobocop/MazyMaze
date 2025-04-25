using UnityEngine;
using System.Collections;

public class TouchInputController : IInputController
{

    private const int LEFT_MOUSE_BUTTON = 0;


    public bool CheckInput(Vector3 emptyTilePosition, ref Vector3 hitPosition)
    {
        if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
        {
            Vector3 inputPosition = Input.mousePosition;

            Ray inputRay = Camera.main.ScreenPointToRay(inputPosition);

            float deltaX = Mathf.Abs(inputRay.origin.x - emptyTilePosition.x);
			float deltaY = Mathf.Abs(inputRay.origin.y - emptyTilePosition.y);

            if ((deltaX < Constant.TILE_SIZE * 1.5f && deltaY < Constant.TILE_SIZE * 0.5f) || 
			    (deltaY < Constant.TILE_SIZE * 1.5f && deltaX < Constant.TILE_SIZE * 0.5f))
            {
                hitPosition = inputRay.origin;
                return true;
            }
        }

        return false;
    }
}

