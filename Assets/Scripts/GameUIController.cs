using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour 
{

	[SerializeField] private Text swapCountText;
    [SerializeField] private Text levelNumberText;

    public ScaledUIView scaledUIView { get; set; }
    public static GameUIController Instance { get; private set;}


	void Awake() 
	{
		Instance = this;
	}

	public void SetSwapCountText()
	{
		swapCountText.text = "SWAPS: " + MainGameController.Instance.LevelEndChecker.currentSwapCount;
    }

    public void SetSwapCountText(int count)
    {
        swapCountText.text = "SWAPS: " + count;
    }

    public void SetLevelNumberText()
    {
        levelNumberText.text = "LEVEL: " + MainGameController.Instance.LevelData.LevelNumber;
    }

    public void ShowSettings()
    {
        scaledUIView.SettingsWindow.Open();
    }

    public void ShowHowTo()
    {
        scaledUIView.HowToWindow.Open();
    }

    public void ShowExit()
    {
        scaledUIView.ExitWindow.Open();
    }

    public void ShowLevelWin()
    {
        scaledUIView.LevelWinWindow.Open();
    }

    public void ShowRestart()
    {
        scaledUIView.RestartWindow.Open();
    }

    public void ShowRecords()
    {
        scaledUIView.RecordsWindow.Open();
    }
}
