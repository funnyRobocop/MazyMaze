using UnityEngine;
using System.Collections;

public class WindowHowToController : WindowController
{

    [SerializeField]
    GameObject firstPageObject;
    [SerializeField]
    GameObject secondPageObject;


    public void SwitchToFirstPage()
    {
        firstPageObject.SetActive(true);
        secondPageObject.SetActive(false);
    }

    public void SwitchToSecondPage()
    {
        firstPageObject.SetActive(false);
        secondPageObject.SetActive(true);
    }
}
