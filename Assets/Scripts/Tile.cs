using UnityEngine;
using System.Collections;

public class Tile   {

	public int X;
	public int Y;
	public char ID;
	public Transform Transform;

	public int Optimizations = 1;
	public bool OptimizedOut = false;

	public Tile(char id, int x, int y)
	{
		this.ID = id;
		this.X = x;
		this.Y = y;
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

		if (this.Optimizations == 1)
		{
			scene.DrawImage(this.Transform, "tile_wall", x, y, 64, 64, false);
		}
		else
		{
			scene.DrawImageTiled(this.Transform, "tile_wall", x, y, 64, 64, this.Optimizations * 64, 64);
		}
	}
}
