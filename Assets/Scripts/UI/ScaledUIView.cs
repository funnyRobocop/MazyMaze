using UnityEngine;
using System.Collections;

public class ScaledUIView : MonoBehaviour
{

    [SerializeField]
    private WindowController settingsWindow;
    [SerializeField]
    private WindowController howToWindow;
    [SerializeField]
    private WindowController exitWindow;
    [SerializeField]
    private WindowController levelWinWindow;
    [SerializeField]
    private WindowController restartWindow;
    [SerializeField]
    private WindowController recordsWindow;

    public WindowController SettingsWindow { get { return settingsWindow; } }
    public WindowController HowToWindow { get { return howToWindow; } }
    public WindowController ExitWindow { get { return exitWindow; } }
    public WindowController LevelWinWindow { get { return levelWinWindow; } }
    public WindowController RestartWindow { get { return restartWindow; } }
    public WindowController RecordsWindow { get { return recordsWindow; } }
}
