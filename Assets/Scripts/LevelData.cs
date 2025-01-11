using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class LevelData 
{

    public int LevelNumber;
    public int Rows;
    public int Cols;
    public int SwapCount;


    public void Setup()
    {
        Rows = Cols = 3 + (int)Mathf.Ceil(LevelNumber / 10f);

        SwapCount = 5 + LevelNumber;

        /*SwapCount = (int)Mathf.Ceil(Mathf.Sqrt(LevelNumber) * 1.8f);

        int levelWithAnotherSwapsCount = LevelNumber - 1;
        while (true)
        {
            if ((int)Mathf.Ceil(Mathf.Sqrt(levelWithAnotherSwapsCount) * 1.8f) != SwapCount)
                break;
            levelWithAnotherSwapsCount--;
        }

        Rows = Cols = LevelNumber - levelWithAnotherSwapsCount + (int)(Mathf.Ceil(Mathf.Sqrt(SwapCount) / 2));*/
    }
}
