using UnityEngine;
using System.Collections;

public class Sprite
{
	public double ModelX;
	public double ModelY;
	public string Type;

	private bool hasBeak = true;
	private bool moving = false;
	private bool faceRight = true;

	private Tile ground = null;

	private double GRAVITY = 1.2;

	public bool isDead = false;
	public void Kill(NewBehaviourScript scene)
	{
		if (this.isDead) return;
		this.isDead = true;
		scene.RemoveTransform(this.transform);
		this.transform = null;
	}

	public void Jump()
	{
		if (this.ground != null)
		{
			this.ground = null;
			this.vy = -24;
		}
	}

	public void Unjump()
	{
		if (this.ground == null && this.vy < 0)
		{
			this.vy = 0;
		}
	}

	public void RemoveBeak(NewBehaviourScript scene)
	{
		if (this.hasBeak)
		{
			this.hasBeak = false;
			scene.AddSprite(new Sprite("beak", this.ModelX, this.ModelY, this));
		}
	}

	public double DX = 0;
	private double vy = 0;
	private Sprite player;

	private Transform transform = null;
	private int renderCounter = 0;

	private const int POINT_COUNT = 120;
	private double[] xs;
	private double[] ys;
	private int beakCounter = 0;

	public Sprite(string type, double x, double y, object arg)
	{
		this.Type = type;
		this.ModelX = x;
		this.ModelY = y;

		if (type == "beak")
		{
			xs = new double[POINT_COUNT];
			ys = new double[POINT_COUNT];
			Sprite player = arg as Sprite;
			this.player = player;
			double radius = 64;
			int reverse = player.faceRight ? 1 : -1;
			double centerX = this.ModelX + radius * reverse;
			double centerY = this.ModelY;

			for (int i = 0; i < POINT_COUNT; ++i)
			{
				xs[i] = centerX - reverse * System.Math.Cos(3.14159 * 2 * i / POINT_COUNT) * radius;
				ys[i] = System.Math.Sin(3.14159 * 2 * i / POINT_COUNT) * radius + centerY;
			}
		}
	}

	public void Render(NewBehaviourScript scene, int cameraX, int cameraY)
	{
		if (this.isDead) return;

		++this.renderCounter;
		this.transform = this.transform ?? scene.AllocateTransform();

		int x = (int)this.ModelX - 32 - cameraX;
		int y = (int)this.ModelY - 32 - cameraY;

		bool reverse = false;
		string imageId = null;
		switch (this.Type)
		{
			case "player":
				reverse = !this.faceRight;
				imageId = "player_" + (this.hasBeak ? "" : "no") + "beak_" + (this.moving ? ("walk_" + (((this.renderCounter / 8) % 2) + 1)) : "stand");
				break;

			case "beak":
				imageId = "beak_" + ((this.renderCounter / 6) % 4);
				break;

			default:
				throw new System.Exception("Unknown sprite ID");
		}
		scene.DrawImage(this.transform, imageId, x, y, 64, 64, reverse);
	}

	public void ApplyAutomation(NewBehaviourScript scene)
	{
		switch (this.Type)
		{
			case "beak":
				Level level = scene.Level;
				if (this.beakCounter < POINT_COUNT)
				{
					this.ModelX = xs[this.beakCounter];
					this.ModelY = ys[this.beakCounter];
					//*
					//*/
				}
				else
				{
					// TODO: return to chicken
					double dx = this.player.ModelX - this.ModelX;
					double dy = this.player.ModelY - this.ModelY;
					double distance = System.Math.Sqrt(dx * dx + dy * dy);
					double v = 12;
					if (distance < v)
					{
						this.Kill(scene);
						this.player.hasBeak = true;
					}
					else
					{
						this.ModelX += v * dx / distance;
						this.ModelY += v * dy / distance;
					}
				}

				if (this.ModelX < 0) this.ModelX = 0;
				if (this.ModelX >= level.Width * 64) this.ModelX = level.Width * 64 - 1;
				if (this.ModelY < 0) this.ModelY = 0;
				if (this.ModelY >= level.Height * 64) this.ModelY = level.Height * 64 - 1;

				this.beakCounter++;
				break;

			default: break;
		}
	}

	public void ApplyMovement(Level level)
	{
		double newX = this.ModelX + this.DX;
		double groundY = this.ModelY + 32;

		this.moving = true;
		if (this.DX > 0) this.faceRight = true;
		else if (this.DX < 0) this.faceRight = false;
		else this.moving = false;
		

		// Apply horizontal component

		if (this.LocationAllowed(level, newX, groundY - .000001) && 
			this.LocationAllowed(level, newX, groundY - 48))
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
			}
		}
	}


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
