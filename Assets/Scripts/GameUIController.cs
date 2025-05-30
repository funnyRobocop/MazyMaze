﻿using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour 
{

	[SerializeField] private TextMeshProUGUI swapCountText;
    [SerializeField] private TextMeshProUGUI levelNumberText;

    public ScaledUIView scaledUIView { get; set; }
    public static GameUIController Instance { get; private set;}


	void Awake() 
	{
		Instance = this;
	}

	public void SetSwapCountText()
	{
        if (swapCountText != null)
		    swapCountText.text = "SWAPS: " + MainGameController.Instance.LevelEndChecker.currentSwapCount;
    }

    public void SetSwapCountText(int count)
    {
        if (swapCountText != null)
            swapCountText.text = "SWAPS: " + count;
    }

    public void SetLevelNumberText()
    {            
#if UNITY_WEBGL
        levelNumberText.text = (YG2.lang == "ru" ? "УРОВЕНЬ " : "LEVEL ") + 
            MainGameController.Instance.LevelData.LevelNumber;
#else
        levelNumberText.text = (Application.systemLanguage == SystemLanguage.Russian ? "LEVEL " : "LEVEL ") + 
            MainGameController.Instance.LevelData.LevelNumber;
#endif
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
