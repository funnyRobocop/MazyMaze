using UnityEngine;
using System.Collections;

public class EscapeController : MonoBehaviour
{

    public static EscapeController Instance { get; private set; }
    public WindowController CurrentWindow { get; set; }


    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentWindow == null)
                GameUIController.Instance.ShowExit();
            else
                CurrentWindow.Close();
        }
    }
}
