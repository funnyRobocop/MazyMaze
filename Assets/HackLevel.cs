using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HackLevel : MonoBehaviour
{

    public int level;
    public Button btn;

	void OnGUI()
    {
        if (GUI.Button(new Rect(10,10,50,50), ""))
        {
            /*btn.onClick.Invoke();
            return;*/
            
            if (level < 0)
                return;
            MainGameController.Instance.LevelData.LevelNumber = level;
            PrefsSaver.Instance.SaveLevel();

            /*for (int LevelNumber = 1; LevelNumber < 100; LevelNumber++)
            {
                int SwapCount = (int)Mathf.Ceil(Mathf.Sqrt(LevelNumber) * 1.5f);

                int levelWithAnotherSwapsCount = LevelNumber - 1;
                while (true)
                {
                    if ((int)Mathf.Ceil(Mathf.Sqrt(levelWithAnotherSwapsCount) * 1.5f) != SwapCount)
                        break;
                    levelWithAnotherSwapsCount--;
                }

                int Rows = LevelNumber - levelWithAnotherSwapsCount + (int)(Mathf.Ceil(Mathf.Sqrt(SwapCount) / 2));

                Debug.Log("SwapCount: " + SwapCount + " Rows: " + Rows);
            }*/
        }
    }
}
