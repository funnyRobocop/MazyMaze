using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PuzzlePartGameController : MonoBehaviour
{
    
	[SerializeField] private float speed;
	private float lerpValue;	
	private State state;
    private TileView emptyTileView;
	private TileView animatedTileView;
	private Vector3 startPosition;
	private Vector3 goalPosition;
	[SerializeField] private LayerMask touchLayer;
    private IInputController CurrentInputController { get; set; }


    private enum State { Playing, Animating, Waiting }


	void Awake()
	{
		enabled = false;
	}

    public void Restart(TileView emptyTileView)
	{
		enabled = true;

        state = State.Playing;
		lerpValue = 0;

        this.emptyTileView = emptyTileView;

        SetInputController();
    }

    public void SetInputController()
    {
        switch (Settings.control)
        {
            case (int)Settings.ControlsType.Touch:
                CurrentInputController = new TouchInputController();
                break;
            case (int)Settings.ControlsType.Swipe:
                CurrentInputController = new SwipeInputController();
                break;
            case (int)Settings.ControlsType.Accelerometer:
                CurrentInputController = new AccelerometerInputController();
                break;
        }
    }
	
	void Update()
	{
		switch (state)
		{
		    case State.Playing:
                Vector3 hitPosition = Vector3.zero;
                if (CurrentInputController.CheckInput(emptyTileView.ThisTransform.localPosition, ref hitPosition))
                    CheckHit(hitPosition);
                break;
		    case State.Animating:
				AnimateMovement();
				CheckIfAnimationEnded();
				break;
			default:
				break;
		}
	}

    public void CheckHit(Vector3 hitPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(hitPosition, Vector3.zero, 0, touchLayer);
        if (hit.collider != null)
        {
            animatedTileView = hit.collider.GetComponent<TileView>();
            startPosition = animatedTileView.ThisTransform.localPosition;
            goalPosition = new Vector2(((int)(emptyTileView.ThisTransform.localPosition.x / Constants.TILE_SIZE)) * Constants.TILE_SIZE,
                                       ((int)(emptyTileView.ThisTransform.localPosition.y / Constants.TILE_SIZE)) * Constants.TILE_SIZE);
            emptyTileView.ThisTransform.position = animatedTileView.ThisTransform.localPosition;
            state = State.Animating;
        }
    }

	private void AnimateMovement()
	{
		lerpValue += Time.deltaTime;
		lerpValue *= speed;

		animatedTileView.ThisTransform.localPosition = new Vector2 (Mathf.SmoothStep (animatedTileView.ThisTransform.localPosition.x, goalPosition.x, lerpValue),
		                                                            Mathf.SmoothStep (animatedTileView.ThisTransform.localPosition.y, goalPosition.y, lerpValue));
    }

    private void CheckIfAnimationEnded()
    {
		if (lerpValue >= 0.5f)
		{
			if(lerpValue >= 1f)
			{
				state = State.Waiting;

				MainGameController.Instance.LevelEndChecker.currentSwapCount++;
				GameUIController.Instance.SetSwapCountText ();

				this.Invoke(0.155f, ()=> 
				{ 
					state = State.Playing;
                    MainGameController.Instance.LevelEndChecker.CheckIsWin();
				});

				lerpValue = 0f;
				MainGameController.Instance.Animate(startPosition.x - goalPosition.x, startPosition.y - goalPosition.y);
                AudioController.Instance.PlaySwipe();
            }
		}
    }
}
