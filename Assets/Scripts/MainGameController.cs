//using YG;
using UnityEngine;
using System.Collections;
using System;

public class MainGameController : MonoBehaviour
{

	private const int CAMERA_Z_POSITION = -10;
	private const float CAMERA_SIZE_FACTOR = 5f;
    private const float MIN_CAMERA_SIZE = 18f;


    [SerializeField] private MazePartGameController mazePart;
	[SerializeField] private PuzzlePartGameController puzzlePart;
	[SerializeField] private LevelEndChecker levelEndChecker;
	[SerializeField] private Transform puzzleParent;
	[SerializeField] private Animator panelAnimator;
	[SerializeField] private LevelData levelData;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject loading;
    public bool isGameRun;

    public PuzzlePartGameController PuzzlePart { get { return puzzlePart; } }
    public Transform PuzzleParent { get { return puzzleParent; } }
	public LevelEndChecker LevelEndChecker { get { return levelEndChecker; } }
	public LevelData LevelData { get { return levelData; } }
	public static MainGameController Instance { get; private set; }


	void Awake()
	{
		Instance = this;
		enabled = false;
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
	}

    void OnEnable()
    {
        RestartGame();
    }

    public void ShowInterstitial()
    {
#if UNITY_WEBGL
        YG2.InterstitialAdvShow();
#else
        AdsInterstitial.Instance.ShowInterstitial();
#endif
    }

    public void PauseGame()
    {
        puzzlePart.enabled = false;
    }

    public void UnPauseGame()
    {
        if (isGameRun)
            puzzlePart.enabled = true;
    }

    public void StopGame()
	{
        isGameRun = false;

        puzzlePart.enabled = false;
		levelEndChecker.enabled = false;

        this.Invoke(1f, GameUIController.Instance.ShowLevelWin);

#if UNITY_WEBGL
        YG2.SetLeaderboard("level", LevelData.LevelNumber);
#else

#endif
        //todo SetLeaderboard
    }

	public void RestartGame()
    {
        isGameRun = false;

        loading.SetActive(true);
        levelData.Setup();
        GameUIController.Instance.SetSwapCountText(0);
        GameUIController.Instance.SetLevelNumberText();

        ClearAll ();

		this.InvokeAfterWaitFrame (1, ()=>
        {
            isGameRun = true;

            mazePart.Restart(levelData);
            puzzlePart.Restart(mazePart.EmptyTileData.TileView);
            SetupCamera ();
            SetupBackground();
            loading.SetActive(false);
        });
		
		levelEndChecker.enabled = false;
        levelEndChecker.Reset(levelData);
		ParticlesManager.Instance.canPlay = false;

		this.InvokeAfterWaitFrame (2, ()=> 
		{ 
			levelEndChecker.enabled = true;
			ParticlesManager.Instance.canPlay = true;
        });
	}
	
	private void ClearAll()
	{
		foreach (Transform item in puzzleParent)
			GameObject.Destroy(item.gameObject);
	}

	public void SetupCamera()
	{
		Camera.main.transform.position = new Vector3 (levelData.Cols * Constant.TILE_SIZE / 2 - Constant.TILE_SIZE / 2, 
		                                              levelData.Rows * Constant.TILE_SIZE / 2 - Constant.TILE_SIZE / 2,
		                                              CAMERA_Z_POSITION);

		Camera.main.orthographicSize = MIN_CAMERA_SIZE + (levelData.Rows - 4) * CAMERA_SIZE_FACTOR;

        if (Camera.main.orthographicSize < MIN_CAMERA_SIZE)
            Camera.main.orthographicSize = MIN_CAMERA_SIZE;
    }

    private void SetupBackground()
    {
        background.transform.localScale = Vector3.one * ((float)levelData.Cols / 5f);
        loading.transform.localScale = background.transform.localScale * 3;
    }

	public void Animate(float deltaX, float deltaY)
	{
		string animation = (deltaX == 0) ? (deltaY < 0 ? "Up" : "Down") : (deltaX < 0 ? "Right" : "Left");
		panelAnimator.SetTrigger (animation);
	}
}
