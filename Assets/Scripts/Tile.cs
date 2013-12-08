using UnityEngine;
using System.Collections.Generic;

public class Tile   {

	public int X;
	public int Y;
	public char ID;
	public Transform Transform;
	public bool IsPassable;
	public bool IsDoor;

	public int Optimizations = 1;
	public bool OptimizedOut = false;

	private static readonly HashSet<char> PASSABLE_TILES = new HashSet<char>("123456789".ToCharArray());
	private static readonly HashSet<char> DOOR_TILES = new HashSet<char>("123456789".ToCharArray());
	public Tile(char id, int x, int y)
	{
		this.ID = id;
		this.X = x;
		this.Y = y;
		this.IsPassable = PASSABLE_TILES.Contains(id);
		this.IsDoor = DOOR_TILES.Contains(id);

	}

	public void Render(NewBehaviourScript scene, int cameraX, int cameraY)
	{
		if (this.OptimizedOut) return;

		if (this.Transform == null)
		{
			this.Transform = scene.AllocateTransform();
		}

		int x = this.X * 64 - cameraX;
		int y = this.Y * 64 - cameraY;

		string imageId = "tile_wall";
		if (this.IsDoor)
		{
			imageId = "door";
		}

		if (this.Optimizations == 1)
		{
			scene.DrawImage(this.Transform, imageId, x, y, 64, 64, false);
		}
		else
		{
			scene.DrawImageTiled(this.Transform, imageId, x, y, 64, 64, this.Optimizations * 64, 64);
		}
	}
}
