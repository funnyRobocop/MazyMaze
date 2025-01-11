using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class MazePartGameController : MonoBehaviour
{

	private TileData[,] tileDataList;
	public TileData[,] TileDataList { get { return tileDataList; }}
    public TileData EmptyTileData { get; private set; }
	private int rows;
	private int cols;
	private int swapCount;


	public void Restart(LevelData levelData)
	{
		rows = levelData.Rows;
		cols = levelData.Cols;
		swapCount = levelData.SwapCount;

		CreateTileDatas();
		CreateEmptyTile(FindBlockedTiles());
		ShuffleTileDatas();
		CreateTileViews();
	}
	
	private void ShuffleTileDatas()
	{
		for (int i = 0; i < swapCount; i++) 
		{			
			var allVariants = new List<TileData>();

			if (EmptyTileData.Col > 0)
				allVariants.Add(tileDataList[EmptyTileData.Col - 1, EmptyTileData.Row]);
			if (EmptyTileData.Col < cols - 1)
				allVariants.Add(tileDataList[EmptyTileData.Col + 1, EmptyTileData.Row]);
			if (EmptyTileData.Row > 0)
				allVariants.Add(tileDataList[EmptyTileData.Col, EmptyTileData.Row - 1]);
			if (EmptyTileData.Row < rows - 1)
				allVariants.Add(tileDataList[EmptyTileData.Col, EmptyTileData.Row + 1]);

			List<TileData> itemsWithMinSwapPriority = allVariants.Where(x => x.SwapPriority == allVariants.Min(y => y.SwapPriority)).ToList();

			SwapTileDatas (EmptyTileData, itemsWithMinSwapPriority[UnityEngine.Random.Range(0, itemsWithMinSwapPriority.Count)]);
		}
	}
	
	private void SwapTileDatas (TileData firstTileData, TileData secondTileData)
	{
		secondTileData.SwapPriority++;

		int firstCol = firstTileData.Col;
		int firstRow = firstTileData.Row;
		int secondCol = secondTileData.Col;
		int secondRow = secondTileData.Row;
		
		tileDataList [firstCol, firstRow] = secondTileData;
		tileDataList [secondCol, secondRow] = firstTileData;
		
		firstTileData.Col = secondCol;
		firstTileData.Row = secondRow;
		secondTileData.Col = firstCol;
		secondTileData.Row = firstRow;
	}

	private void CreateTileDatas()
	{
		char[,] maze = new EllerMazeGenerator (cols, rows).Maze;
		tileDataList = new TileData[cols, rows];

		for (int col = 0; col < cols; col++) 
		{			
			for (int row = 0; row < rows; row++) 
			{
				tileDataList[col,row] = new TileData(col, row);
				tileDataList[col,row].InitWalls(maze);
			}
		}
	}

	private void CreateTileViews()
	{
		for (int col = 0; col < cols; col++) 
		{			
			for (int row = 0; row < rows; row++)
			{
				TileView.CreateTile(tileDataList, col, row);
			}
		}
	}
	
	private List<TileData> FindBlockedTiles()
	{
		var blockedTiles = new List<TileData> ();
		
		for (int col = 0; col < tileDataList.GetLength(0); col++) 
		{			
			for (int row = 0; row < tileDataList.GetLength(1); row++) 
			{				
				if (tileDataList[col,row].IsBlocked())
					blockedTiles.Add(tileDataList[col,row]);
			}
		}

		return blockedTiles;
	}

	private void CreateEmptyTile(List<TileData> blockTiles)
	{
		EmptyTileData = blockTiles [UnityEngine.Random.Range (0, blockTiles.Count)];
		EmptyTileData.InitWallsForEmpty();

		if (EmptyTileData.Col > 0) 
			tileDataList[EmptyTileData.Col - 1, EmptyTileData.Row].Walls.Add(TileData.Direction.right);
		if (EmptyTileData.Col < cols - 1) 
			tileDataList[EmptyTileData.Col + 1, EmptyTileData.Row].Walls.Add(TileData.Direction.left);
		if (EmptyTileData.Row > 0) 
			tileDataList[EmptyTileData.Col, EmptyTileData.Row - 1].Walls.Add(TileData.Direction.top);
		if (EmptyTileData.Row < rows - 1) 
			tileDataList[EmptyTileData.Col, EmptyTileData.Row + 1].Walls.Add(TileData.Direction.bottom);
	}
}
