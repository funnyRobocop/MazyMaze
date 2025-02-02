using UnityEngine;
using YG;

public class PrefsSaver : MonoBehaviour
{

    public static PrefsSaver Instance { get; private set; }

    void Awake ()
    {
        Instance = this;

#if UNITY_ANDROID
        Settings.soundOn = PlayerPrefs.GetInt("soundOn", 1) == 1;
        Settings.control = PlayerPrefs.GetInt("control", 1);
        MainGameController.Instance.LevelData.LevelNumber = PlayerPrefs.GetInt("levelNumber", 1);
#else        
        Settings.soundOn = YG2.saves.soundOn == 0;
        Settings.control = YG2.saves.control;
        MainGameController.Instance.LevelData.LevelNumber = YG2.saves.levelNumber;

        if (MainGameController.Instance.LevelData.LevelNumber < 1)
            MainGameController.Instance.LevelData.LevelNumber = 1;
#endif
    }

    public void SaveSound()
    {
#if UNITY_ANDROID
        PlayerPrefs.SetInt("soundOn", Settings.soundOn ? 0 : 1);
        PlayerPrefs.Save();
#else 
        YG2.saves.soundOn = Settings.soundOn ? 0 : 1;
        YG2.SaveProgress();
#endif
    }

    public void SaveLevel()
    {
#if UNITY_ANDROID
        PlayerPrefs.SetInt("levelNumber", MainGameController.Instance.LevelData.LevelNumber);
        PlayerPrefs.Save();
#else 
        YG2.saves.levelNumber = MainGameController.Instance.LevelData.LevelNumber;
        YG2.SaveProgress();
#endif
    }

    public void SaveControl()
    {
#if UNITY_ANDROID
        PlayerPrefs.SetInt("control", Settings.control);
        PlayerPrefs.Save();
#else         
        YG2.saves.control = Settings.control;
        YG2.SaveProgress();
 #endif
    }
}

