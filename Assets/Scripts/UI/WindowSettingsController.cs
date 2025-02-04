using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WindowSettingsController : WindowController
{

    [SerializeField]
    private Toggle soundOnToggle;
    [SerializeField]
    private Toggle touchToggle;
    [SerializeField]
    private Toggle swipeToggle;
    [SerializeField]
    private Toggle accelerometerToggle;
    [SerializeField]
    private GameObject soundOnSprite;


    void Start ()
    {
        soundOnToggle.isOn = Settings.soundOn;
        soundOnSprite.SetActive(Settings.soundOn);

        touchToggle.isOn = (Settings.control == (int)Settings.ControlsType.Touch);
        swipeToggle.isOn = (Settings.control == (int)Settings.ControlsType.Swipe);
        accelerometerToggle.isOn = (Settings.control == (int)Settings.ControlsType.Accelerometer);

        ChangeInteractable();

        soundOnToggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(ChangeSoundOn));
        touchToggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(ChangeTouch));
        swipeToggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(ChangeSwipe));
        accelerometerToggle.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<bool>(ChangeAccelerometer));
    }

    private void ChangeSoundOn(bool value)
    {
        Settings.soundOn = value;
        PrefsSaver.Instance.SaveSound();
        soundOnSprite.SetActive(value);
    }

    private void ChangeTouch(bool value)
    {
        if (value)
            swipeToggle.isOn = accelerometerToggle.isOn = false;

        ChangeInteractable();
        ChangeControls();
    }

    private void ChangeSwipe(bool value)
    {
        if (value)
            touchToggle.isOn = accelerometerToggle.isOn = false;

        ChangeInteractable();
        ChangeControls();
    }

    private void ChangeAccelerometer(bool value)
    {
        if (value)
            touchToggle.isOn = swipeToggle.isOn = false;

        ChangeInteractable();
        ChangeControls();
    }

    private void ChangeInteractable()
    {
        touchToggle.interactable = !touchToggle.isOn;
        swipeToggle.interactable = !swipeToggle.isOn;
        accelerometerToggle.interactable = !accelerometerToggle.isOn;
    }

    private void ChangeControls()
    {
        if (touchToggle.isOn)
            Settings.control = (int)Settings.ControlsType.Touch;
        else if (swipeToggle.isOn)
            Settings.control = (int)Settings.ControlsType.Swipe;
        else if (accelerometerToggle.isOn)
            Settings.control = (int)Settings.ControlsType.Accelerometer;

        PrefsSaver.Instance.SaveControl();

        MainGameController.Instance.PuzzlePart.SetInputController();
    }
}
