using UnityEngine;
using System.Collections;

public class TileView : MonoBehaviour 
{

	public Transform ThisTransform { get; private set; }
	public GameObject ThisGameObject { get; private set; }

	[SerializeField] private Animator fullSpriteAnimator;


	void Awake()
	{
		ThisGameObject = gameObject;
		ThisTransform = transform.parent;
	}

	public void AnimateForWin()
	{
		fullSpriteAnimator.SetTrigger (Constant.FADE_IN_ANIM_TRIGGER);
	}
	
	public static void CreateTile(TileData[,] tiles, int col, int row)
	{
		var tileViewObject = GameObject.Instantiate(Resources.Load(tiles [col, row].GenerateTilePrefabName()), 
		                                            new Vector2 (col * Constant.TILE_SIZE, row * Constant.TILE_SIZE),
		                                            Quaternion.identity) as GameObject;

		tiles [col, row].TileView = tileViewObject.GetComponentInChildren<TileView> ();
		tiles [col, row].TileView.ThisTransform.parent = MainGameController.Instance.PuzzleParent;
	}
}
