﻿using UnityEngine;
using System.Collections.Generic;

public class Level
{

	public Tile[][] Tiles;

	public int Width;
	public int Height;

	public List<int[]> alienSpawns = new List<int[]>();

	public Dictionary<int, string> DoorHookup = new Dictionary<int, string>();
	public Dictionary<int, string> TargetDoorInfo = new Dictionary<int, string>();
	public Dictionary<int, int[]> DoorLocations = new Dictionary<int, int[]>();
	public Level(string title)
	{
		MapData data = MapData.RAW_LEVEL_DATA[title];
		string[] rawTileData = data.Tiles;
		int width = rawTileData[0].Length;
		int height = rawTileData.Length;
		List<Tile[]> columns = new List<Tile[]>();
		List<Tile> column = new List<Tile>();
		int y;
		for (int x = 0; x < width; ++x)
		{
			for (y = 0; y < height; ++y)
			{
				char id = rawTileData[y][x];
				if (id == 'A')
				{
					id = ' ';
					this.alienSpawns.Add(new int[] { x, y });
				}
				else if (id >= '0' && id <= '9')
				{
					int doorNum = id - '0';
					this.DoorHookup[doorNum] = data.DoorLookup.ContainsKey(doorNum) ? data.DoorLookup[doorNum] : null;
					this.DoorLocations[doorNum] = new int[] { x, y };
					this.TargetDoorInfo[doorNum] = data.TargetDoorInfo.ContainsKey(doorNum) ? data.TargetDoorInfo[doorNum] : null;
				}
				column.Add(this.GetTile(id, x, y));
			}
			columns.Add(column.ToArray());
			column.Clear();
		}

		this.Tiles = columns.ToArray();
		this.Width = width;
		this.Height = height;
		this.Optimize();
	}

	public void FreeAllAssets(NewBehaviourScript scene)
	{
		Tile tile;
		for (int y = 0; y < this.Height; ++y)
		{
			for (int x = 0; x < this.Width; ++x)
			{
				tile = this.Tiles[x][y];
				if (tile != null)
				{
					if (tile.Transform != null)
					{
						scene.RemoveTransform(tile.Transform);
					}
				}
			}
		}
	}

	private void Optimize()
	{
		int width = this.Width;
		int height = this.Height;
		Tile tile, previousTile;
		for (int y = 0; y < height; ++y)
		{
			tile = null;
			previousTile = null;
			for (int x = 0; x < width; ++x)
			{
				tile = this.Tiles[x][y];
				if (tile != null && previousTile != null && tile.ID == previousTile.ID)
				{
					tile.OptimizedOut = true;
					previousTile.Optimizations++;
				}
				else
				{
					previousTile = tile;
				}
			}
		}
	}

	private Tile GetTile(char tileId, int x, int y)
	{
		if (tileId != ' ')
		{
			return new Tile(tileId, x, y);
		}
		return null;
	}

	public void Render(NewBehaviourScript scene, int cameraX, int cameraY)
	{
		int width = this.Width;
		int height = this.Height;
		int x, y;

		Tile[][] tiles = this.Tiles;
		Tile tile;
		for (y = 0; y < height; ++y)
		{
			for (x = 0; x < width; ++x)
			{
				tile = tiles[x][y];
				if (tile != null)
				{
					tile.Render(scene, cameraX, cameraY);
				}
			}
		}
	}
}
