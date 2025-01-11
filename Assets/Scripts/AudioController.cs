using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{

    [SerializeField]
    private AudioSource move;
    [SerializeField]
    private AudioSource click;
    [SerializeField]
    private AudioSource swipe;
    [SerializeField]
    private AudioSource win;
    [SerializeField]
    private AudioSource water;

    public static AudioController Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }
    public void PlayThrow()
    {
        if (Settings.soundOn)
            move.Play();
    }

    public void PlayClick()
    {
        if (Settings.soundOn)
            click.Play();
    }

    public void PlaySwipe()
    {
        if (Settings.soundOn)
            swipe.Play();
    }

    public void PlayWin()
    {
        if (Settings.soundOn)
            win.Play();
    }

    public void PlayWater()
    {
        if (Settings.soundOn)
            water.Play();
    }
}
