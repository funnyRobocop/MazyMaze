using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TileData
{
		
	private const string MAIN_TILE_PREFAB_NAME_PART = "Tiles/Tile"; 
	private const string MAIN_HINT_PREFAB_NAME_PART = "Hints/Hint";
	private const string LEFT_PREFAB_NAME_PART = "Left"; 
	private const string RIGHT_PREFAB_NAME_PART = "Right"; 
	private const string TOP_PREFAB_NAME_PART = "Top"; 
	private const string BOTTOM_PREFAB_NAME_PART = "Bottom";


	public List<Direction> Walls { get; private set;}
	public int Col { get; set;}
	public int Row { get; set;}
	public TileView TileView { get; set;}
	public int SwapPriority { get; set;}

	public enum Direction { left, right, top, bottom }


	public TileData(int col, int row)
	{
		this.Col = col;
		this.Row = row;
	}
	
	public string GenerateHintPrefabName()
	{
		string result = MAIN_HINT_PREFAB_NAME_PART;
		
		if (Walls.Contains (Direction.left))
			result += LEFT_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.right))
			result += RIGHT_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.top))
			result += TOP_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.bottom))
			result += BOTTOM_PREFAB_NAME_PART;
		
		return result;
	}

	public string GenerateTilePrefabName()
	{
		string result = MAIN_TILE_PREFAB_NAME_PART;

		if (Walls.Contains (Direction.left))
			result += LEFT_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.right))
			result += RIGHT_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.top))
			result += TOP_PREFAB_NAME_PART;
		if (Walls.Contains (Direction.bottom))
			result += BOTTOM_PREFAB_NAME_PART;

		return result;
	}

	public void InitWalls(char[,] maze)
	{
		Walls = new List<Direction>();

		if (maze[Col * 2, Row * 2 + 1] == EllerMazeGenerator.MAZE_WALL)
			Walls.Add(Direction.left);
		if (maze[Col * 2 + 2, Row * 2 + 1] == EllerMazeGenerator.MAZE_WALL)
			Walls.Add(Direction.right);
		if (maze[Col * 2 + 1, Row * 2 + 2] == EllerMazeGenerator.MAZE_WALL)
			Walls.Add(Direction.top);
		if (maze[Col * 2 + 1, Row * 2] == EllerMazeGenerator.MAZE_WALL)
			Walls.Add(Direction.bottom);
	}
	
	public bool IsBlocked()
	{
		return Walls.Count == 3;
	}
	
	public void InitWallsForEmpty()
	{
		Walls.Clear ();
		Walls.Add (TileData.Direction.left);
		Walls.Add (TileData.Direction.right);
		Walls.Add (TileData.Direction.top);
		Walls.Add (TileData.Direction.bottom);
	}
}

