using System.Collections;
using UnityEngine;

public class ResizeScreenChecker : MonoBehaviour
{
    public float lastScreenWidth = 0f;

    void Start()
    {
        lastScreenWidth = Screen.width;
    }

    void Update()
    {
        if( lastScreenWidth != Screen.width)
        {
            lastScreenWidth = Screen.width;
            StartCoroutine(AdjustScale());
        }    
    }

    private IEnumerator AdjustScale()
    {
        yield return null;
        MainGameController.Instance.SetupCamera();
    }
}
