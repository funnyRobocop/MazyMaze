using UnityEngine;
using System.Collections;

public class PrefsSaver : MonoBehaviour
{


    public static PrefsSaver Instance { get; private set; }


    void Awake ()
    {
        Instance = this;
        Settings.soundOn = PlayerPrefs.GetInt("soundOn", 1) == 1 ? true : false;
        Settings.control = PlayerPrefs.GetInt("control", 1);
        MainGameController.Instance.LevelData.LevelNumber = PlayerPrefs.GetInt("levelNumber", 1);
    }

    public void SaveSound()
    {
        PlayerPrefs.SetInt("soundOn", Settings.soundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt("levelNumber", MainGameController.Instance.LevelData.LevelNumber);
        PlayerPrefs.Save();
    }

    public void SaveControl()
    {
        PlayerPrefs.SetInt("control", Settings.control);
        PlayerPrefs.Save();
    }
}
