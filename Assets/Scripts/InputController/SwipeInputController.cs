using UnityEngine;
using System.Collections;
using System;

public class SwipeInputController : IInputController
{

    private const int LEFT_MOUSE_BUTTON = 0;


    private Vector3 swipeStart;
    private Vector3 swipeEnd;


    public bool CheckInput(Vector3 emptyTilePosition, ref Vector3 hitPosition)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
        {
            swipeStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
        {
            swipeEnd = Input.mousePosition;
#else
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            swipeStart = Input.touches[0].position;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            swipeEnd = Input.touches[0].position;
#endif
            Vector3 delta = swipeEnd - swipeStart;

            if (Mathf.Abs(delta.x) < 1 && Mathf.Abs(delta.y) < 1)
                return false;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                hitPosition = new Vector3(emptyTilePosition.x + Constants.TILE_SIZE * Mathf.Sign(delta.x) * -1, emptyTilePosition.y);
                return true;
            }
            else
            {
                hitPosition = new Vector3(emptyTilePosition.x, emptyTilePosition.y + Constants.TILE_SIZE * Mathf.Sign(delta.y) * -1);
                return true;
            }
        }

        return false;
    }
}