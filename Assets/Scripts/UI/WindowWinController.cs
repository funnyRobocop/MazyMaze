using UnityEngine;
using UnityEngine.UI;

public class WindowWinController : WindowController
{

    [SerializeField]
    private Text nextLevelText;


    void OnEnable()
    {
        SetNextLevelNumber();
        AudioController.Instance.PlayWin();
    }

    public void SetNextLevelNumber()
    {
        nextLevelText.text = "Level: " + (MainGameController.Instance.LevelData.LevelNumber).ToString();
    }

}
