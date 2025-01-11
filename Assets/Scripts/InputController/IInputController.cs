using UnityEngine;
using System.Collections;

public interface IInputController
{

    bool CheckInput(Vector3 emptyTilePosition, ref Vector3 hitPosition);
}
