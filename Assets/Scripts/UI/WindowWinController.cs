using TMPro;
using UnityEngine;
using YG;

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
        nextLevelText.text = (YG2.lang == "ru" ? "УРОВЕНЬ " : "LEVEL ") + 
            MainGameController.Instance.LevelData.LevelNumber;
    }

}
