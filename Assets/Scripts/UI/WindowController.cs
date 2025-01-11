using UnityEngine;
using System.Collections;

public class WindowController : MonoBehaviour
{

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
        animator.SetTrigger("Open");
        BlindController.Instance.ShowBlind();
        MainGameController.Instance.PauseGame();
        EscapeController.Instance.CurrentWindow = this;
        AudioController.Instance.PlayThrow();
    }

    public void Close()
    {
        animator.SetTrigger("Close");
        AudioController.Instance.PlayThrow();
    }

    public void OnClosed()
    {
        this.gameObject.SetActive(false);
        BlindController.Instance.HideBlind();
        MainGameController.Instance.UnPauseGame();
        EscapeController.Instance.CurrentWindow = null;
    }
}
