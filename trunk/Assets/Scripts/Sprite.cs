using UnityEngine;
using System.Collections;

public class Sprite
{
	public double ModelX;
	public double ModelY;
	public string Type;

	private Tile ground = null;

	public void Jump()
	{
		if (this.ground != null)
		{
			this.ground = null;
			this.vy = -8;
		}
	}

	public void Unjump()
	{
		if (this.ground == null && this.vy < 0)
		{
			this.vy = 0;
		}
	}

	public double DX = 0;
	private double vy = 0;

	private Transform transform = null;
	private int renderCounter = 0;

	private Color CurrentColor
	{
		get
		{
			//++this.renderCounter;
			//return new Color((this.renderCounter % 256) / 255f, 0, 0);
			return Color.white;
		}
	}

	public Sprite(string type, double x, double y)
	{
		this.Type = type;
		this.ModelX = x;
		this.ModelY = y;
	}

	public void Render(NewBehaviourScript scene, int cameraX, int cameraY)
	{
		this.transform = this.transform ?? scene.AllocateTransform();

		int x = (int)this.ModelX - 32 - cameraX;
		int y = (int)this.ModelY - 32 - cameraY;

		scene.DrawRectangle(this.transform, this.CurrentColor, x, y, 64, 64);
	}

	private double GRAVITY = 1;

	public void ApplyMovement(Level level)
	{
		double newX = this.ModelX + this.DX;
		double groundY = this.ModelY + 32;

		// Apply horizontal component

		if (this.LocationAllowed(level, newX, groundY) && this.LocationAllowed(level, newX, groundY - 48))
		{
			this.ModelX = newX;
		}

		this.DX = 0;

		int tileX = (int)(this.ModelX / 64);
		int tileY = (int)(groundY / 64);

		if (ground != null)
		{
			this.vy = 0;
		}

		// Try to revoke the ground
		if (this.ground != null)
		{
			if (this.ground.X != tileX)
			{
				// no longer standing on the same tile. Could have just walked horizontally.
				this.ground = null;

				if (groundY % 64 == 0)
				{
					if (tileY >= 0 && tileY < level.Height)
					{
						ground = level.Tiles[tileX][tileY]; // could be null
					}
				}
			}
		}

		// ground has been properly revoked or updated

		if (ground == null)
		{
			this.vy += GRAVITY;
			
			double newGroundY = this.ModelY + this.vy + 32;

			bool feetFree = this.LocationAllowed(level, this.ModelX, newGroundY);
			bool headFree = this.LocationAllowed(level, this.ModelX, newGroundY - 48);

			if (feetFree && headFree)
			{
				this.ModelY = newGroundY - 32;
			}
			else if (feetFree) // head hit the ceiling
			{
				this.vy = 0; // stop upward acceleration, allow gravity to do the rest
			}
			else // feet hit the ground
			{
				tileY = (int)(newGroundY / 64);
				this.ground = level.Tiles[tileX][tileY];
				this.ModelY = tileY * 64 - 32;
				this.vy = 0;
				Debug.Log("Hit ground!" + tileY + " [" + (++this.counter) + "] " + this.ModelY);
			}
		}

		//Debug.Log("Player at: " + this.ModelX + ", " + (this.ModelY + 32));
	}
	private int counter = 0;


	private bool LocationAllowed(Level level, double x, double y)
	{
		int col = (int)(x / 64);
		int row = (int)(y / 64);
		if (col < 0 || col >= level.Width || row < 0 || row >= level.Height) return false;

		Tile t = level.Tiles[col][row];
		if (t == null) return true;

		// TODO: passable tiles
		return false;
	}
}
