using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class LevelEndChecker : MonoBehaviour
{

	public int currentConnectedTilesCount;
	public int goalConnectedTilesCount;
	public int currentSwapCount;
	public Action onWin;
	public Action onLose;

    
	public void Reset(LevelData levelData)
	{
		currentConnectedTilesCount = 0;
		goalConnectedTilesCount = levelData.Cols * levelData.Rows - 1;
		currentSwapCount = 0;
	}
	
	public bool CheckIsWin()
	{
		if (currentConnectedTilesCount >= goalConnectedTilesCount) 
		{
			MainGameController.Instance.StopGame();
            AudioController.Instance.PlayWater();
            MainGameController.Instance.LevelData.LevelNumber++;
            PrefsSaver.Instance.SaveLevel();

			foreach (var item in GameObject.FindObjectsOfType<TileView>())
			{
				item.AnimateForWin();
			}

			return true;
		}

		return false;
	}
}
