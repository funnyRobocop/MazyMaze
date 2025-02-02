using TMPro;
using UnityEngine;

public class WindowWinController : WindowController
{

    [SerializeField]
    private TextMeshProUGUI nextLevelText;


    void OnEnable()
    {
        SetNextLevelNumber();
        AudioController.Instance.PlayWin();
    }

    public void SetNextLevelNumber()
    {
        nextLevelText.text = "УРОВЕНЬ: " + (MainGameController.Instance.LevelData.LevelNumber).ToString();
    }

}
