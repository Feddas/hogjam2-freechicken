using UnityEngine;
using System.Collections;

public class Tile   {

	public int X;
	public int Y;
	public char ID;
	public Transform TransformA;
	public Transform TransformB;

	public Tile(char id, int x, int y)
	{
		this.ID = id;
		this.X = x;
		this.Y = y;
	}

	public void Render(NewBehaviourScript scene, int cameraX, int cameraY)
	{
		if (this.TransformA == null)
		{
			this.TransformA = scene.AllocateTransform();
			this.TransformB = scene.AllocateTransform();
		}

		int x = this.X * 64 - cameraX;
		int y = this.Y * 64 - cameraY;

		//scene.DrawRectangle(this.TransformA, Color.white, x, y, 64, 64);
		//scene.DrawRectangle(this.TransformB, Color.red, x + 8, y + 8, 48, 48);
		scene.DrawImage(this.TransformA, "tile_wall", x, y, 64, 64, false);
	}
}
