﻿using UnityEngine;
using System.Collections.Generic;

public class Level
{

	public Tile[][] Tiles;

	public int Width;
	public int Height;

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
				column.Add(this.GetTile(rawTileData[y][x], x, y));
			}
			columns.Add(column.ToArray());
			column.Clear();
		}

		this.Tiles = columns.ToArray();
		this.Width = width;
		this.Height = height;
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