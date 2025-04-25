using UnityEngine;

public class ScaledUISwitcher : MonoBehaviour
{

    private const float SMALL_ITCH_SIZE = 5f;
    private const float MEDIUM_ITCH_SIZE = 7.5f;
    private const float SMALL_PIXEL_SIZE = 800;
    private const float MEDIUM_PIXEL_SIZE = 1200f;

    [SerializeField]
    private ScaledUIView smallView;
    [SerializeField]
    private ScaledUIView mediumView;
    [SerializeField]
    private ScaledUIView largeView;


    void Start ()
    {
        if (Screen.dpi == 0)
        {
            SwitchView(Screen.height, SMALL_PIXEL_SIZE, MEDIUM_PIXEL_SIZE);
        }
        else
        {
            SwitchView(Screen.height / Screen.dpi, SMALL_ITCH_SIZE, MEDIUM_ITCH_SIZE);
        }
    }

    private void SwitchView(float height, float smallSize, float mediumSize)
    {
        if (height <= smallSize)
        {
            GameUIController.Instance.scaledUIView = smallView;
            smallView.gameObject.SetActive(true);
        }
        else if (height <= mediumSize)
        {
            GameUIController.Instance.scaledUIView = mediumView;
            mediumView.gameObject.SetActive(true);
        }
        else
        {
            GameUIController.Instance.scaledUIView = largeView;
            largeView.gameObject.SetActive(true);
        }
    }
}
