using UnityEngine;
using System.Collections;

public class BlindController : MonoBehaviour
{

    public static BlindController Instance;


    void Awake ()
    {
        Instance = this;
    }

    public void ShowBlind()
    {
        gameObject.SetActive(true);
    }

    public void HideBlind()
    {
        gameObject.SetActive(false);
    }
}
