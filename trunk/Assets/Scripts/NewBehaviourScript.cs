using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{
	public Material wallMaterial;
	public Material doorMaterial;
	public Material wall2Material;
	
	public Material cutscenePage0;
	public Material cutscenePage1;
	public Material cutscenePage2;

	public Material chickenRightBeak0;
	public Material chickenRightBeak1;
	public Material chickenRightBeak2;
	public Material chickenRightNoBeak0;
	public Material chickenRightNoBeak1;
	public Material chickenRightNoBeak2;

	public Material beak0;
	public Material beak1;
	public Material beak2;
	public Material beak3;

	public Material alienLeg1M1;
	public Material alienLeg1M2;
	public Material alienLeg1M3;
	public Material alienLeg2M1;
	public Material alienLeg2M2;
	public Material alienLeg2M3;
	public Material alienLeg3M1;
	public Material alienLeg3M2;
	public Material alienLeg3M3;

	public Material alienFiring;


	//public Transform cube;
	public Transform quad;
	public Transform textThing;

	private Sprite player;

	private Level level;

	public Level Level { get { return this.level; } }

	void Start()
	{
		this.quad.transform.position = new Vector3(999999, 999999, 0); // hide!
		this.InitializeLevel("level_1");
	}

	private void InitializeLevel(string levelId)
	{
		if (this.level != null)
		{
			foreach (Sprite sprite in this.sprites)
			{
				sprite.Kill(this);
			}
			this.sprites = new List<Sprite>();
			this.level.FreeAllAssets(this);
		}

		this.level = new Level(levelId);
		this.player = new Sprite("player", 130, 300, null);
		this.sprites.Add(this.player);

		this.textThing.transform.position = new Vector3(this.ConvertX(10), this.ConvertY(10), 0);

		foreach (int[] alienSpawn in this.level.alienSpawns)
		{
			int x = alienSpawn[0] * 64 + 32;
			int y = alienSpawn[1] * 64 + 32;

			this.sprites.Add(new Sprite("alien", x, y, null));
		}
	}


	private double lastTime = 0;
	private const int INTENDED_FPS = 30;
	private bool SkipTurn()
	{
		double currentTime = System.DateTime.Now.Ticks / 10000000.0;
		double delay = 1.0 / INTENDED_FPS;
		double diff = currentTime - lastTime;
		return diff < delay;
		//return false;
	}

	private int z = 0;

	private double previousX = 0;
	private double previousY = 0;

	private bool spacePressed = false;

	private void ProcessInput()
	{
		double pdx = Input.GetAxis("Horizontal");
		double pdy = -Input.GetAxis("Vertical");

		bool leftPressed = false;
		bool rightPressed = false;
		bool upPressed = false;
		bool downPressed = false;
		if (pdx != 0)
		{
			if (pdx < 0 && pdx <= this.previousX)
			{
				leftPressed = true;
			}
			else if (pdx > 0 && pdx >= this.previousX)
			{
				rightPressed = true;
			}
		}

		if (pdy != 0)
		{
			if (pdy < 0 && pdy <= this.previousY)
			{
				upPressed = true;
			}
			else if (pdy > 0 && pdy >= this.previousY)
			{
				downPressed = true;
			}
		}

		this.previousX = pdx;
		this.previousY = pdy;

		bool shiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

		double dx = 0;
		double dy = 0;
		double v = shiftPressed ? 8 : 3;
		if (leftPressed) dx -= v;
		if (rightPressed) dx += v;
		if (upPressed) dy -= v;
		if (downPressed) dy += v;

		this.player.DX = dx;

		bool spacePressed = Input.GetKey(KeyCode.Space);
		if (spacePressed != this.spacePressed)
		{
			if (spacePressed)
			{
				this.player.Jump();
			}
			else
			{
				this.player.Unjump();
			}
			this.spacePressed = spacePressed;
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			this.player.RemoveBeak(this);
		}

	}

	List<Sprite> sprites = new List<Sprite>();

	public Transform AllocateTransform()
	{
		Transform t;
		int size = this.freeTransforms.Count;
		if (size > 0)
		{
			t = this.freeTransforms[size - 1];
			this.freeTransforms.RemoveAt(size - 1);
		}
		else
		{
			t = Instantiate(this.quad) as Transform;
		}
		t.transform.position = new Vector3(-999999, 0, 0);
		return t;
	}

	private List<Transform> freeTransforms = new List<Transform>();
	public void RemoveTransform(Transform transform)
	{
		if (transform == null) return;
		transform.transform.position = new Vector3(-999999, 0, 0);
		this.freeTransforms.Add(transform);
	}

	private static float[] conversionOut = new float[2];
	private void ConvertCoords(int x, int y)
	{
		conversionOut[0] = x * 40 / 3f / 1024 - 20 / 3f;
		conversionOut[1] = y * 40 / 3f / 768 - 20 / 3f;
	}

	private Material GetTexture(string id)
	{
		switch (id)
		{
			case "tile_wall": return this.wallMaterial;
			case "door": return this.doorMaterial;
			case "wtile": return this.wall2Material;
			case "player_beak_stand": return this.chickenRightBeak0;
			case "player_beak_walk_1": return this.chickenRightBeak1;
			case "player_beak_walk_2": return this.chickenRightBeak2;
			case "player_nobeak_stand": return this.chickenRightNoBeak0;
			case "player_nobeak_walk_1": return this.chickenRightNoBeak1;
			case "player_nobeak_walk_2": return this.chickenRightNoBeak2;
			case "beak_0": return this.beak0;
			case "beak_1": return this.beak1;
			case "beak_2": return this.beak2;
			case "beak_3": return this.beak3;
			case "alien_walk0_mouth0": return this.alienLeg1M1;
			case "alien_walk0_mouth1": return this.alienLeg1M2;
			case "alien_walk0_mouth2": return this.alienLeg1M3;
			case "alien_walk1_mouth0": return this.alienLeg2M1;
			case "alien_walk1_mouth1": return this.alienLeg2M2;
			case "alien_walk1_mouth2": return this.alienLeg2M3;
			case "alien_stand_mouth0": return this.alienLeg3M1;
			case "alien_stand_mouth1": return this.alienLeg3M2;
			case "alien_stand_mouth2": return this.alienLeg3M3;
			case "win_0": return this.cutscenePage0;
			case "win_1": return this.cutscenePage1;
			case "win_2": return this.cutscenePage2;
			default: throw new System.Exception("Unknown texture: " + id);
		}
	}

	public void AddSprite(Sprite sprite)
	{
		this.sprites.Add(sprite);
	}

	private float ConvertX(int pixelX)
	{
		return pixelX * 40 / 3f / 1024 - 20 / 3f;
	}

	private float ConvertY(int pixelY)
	{
		return -((pixelY + 128) * 40 / 3f / 1024 - 20 / 3f);
	}

	public void DrawRectangle(Transform rect, Color color, int x, int y, int width, int height)
	{
		float unityWidth = width * 40 / 3f / 1024;
		float unityHeight = height * 40 / 3f / 1024;
		float unityX = this.ConvertX(x) + unityWidth / 2;
		float unityY = this.ConvertY(y) - unityHeight / 2;
		rect.transform.position = new Vector3(unityX, unityY, this.z-- / 10000f);
		rect.transform.localScale = new Vector3(unityWidth, unityHeight, 1);
		rect.renderer.material.color = color;
	}

	public void DrawImageTiled(Transform rect, string image, int x, int y, int width, int height, int tiledWidth, int tiledHeight)
	{
		float unityWidth = tiledWidth * 40 / 3f / 1024;
		float unityHeight = tiledHeight * 40 / 3f / 1024;
		float unityX = this.ConvertX(x) + unityWidth / 2;
		float unityY = this.ConvertY(y) - unityHeight / 2;
		rect.transform.position = new Vector3(unityX, unityY, this.z-- / 10000f);
		rect.transform.localScale = new Vector3(unityWidth, unityHeight, 1);
		rect.renderer.material = this.GetTexture(image);
		rect.renderer.material.SetTextureScale("_MainTex", new Vector2(tiledWidth / width, tiledHeight / height));
	}

	public void DrawImage(Transform rect, string image, int x, int y, int width, int height, bool reverse)
	{
		float unityWidth = width * 40 / 3f / 1024;
		float unityHeight = height * 40 / 3f / 1024;
		float unityX = this.ConvertX(x) + unityWidth / 2;
		float unityY = this.ConvertY(y) - unityHeight / 2;
		rect.transform.position = new Vector3(unityX, unityY, this.z-- / 10000f);
		rect.transform.localScale = new Vector3(reverse ? -unityWidth : unityWidth, unityHeight, 1);
		rect.renderer.material = this.GetTexture(image);
	}

	void Update()
	{
		if (this.SkipTurn()) return;

		this.z = 0;

		this.ProcessInput();
		this.UpdateImpl();
		this.Render();
	}

	private long cutsceneStart = 0;

	private void DoCutsceneUpdate()
	{
		if (this.cutsceneIndex < 2)
		{
			double seconds = (System.DateTime.Now.Ticks - this.cutsceneStart) / 10000000.0;
			if (seconds > 5)
			{
				this.cutsceneIndex++;
				this.cutsceneStart = System.DateTime.Now.Ticks;
			}
		}
	}

	private void UpdateImpl()
	{
		if (this.cutsceneIndex >= 0)
		{
			this.DoCutsceneUpdate();
			return;
		}

		foreach (Sprite sprite in this.sprites)
		{
			sprite.ApplyAutomation(this);
		}

		List<Sprite> newSprites = new List<Sprite>();
		foreach (Sprite sprite in this.sprites)
		{
			sprite.ApplyMovement(this.level);

			if (!sprite.isDead)
			{
				newSprites.Add(sprite);
			}
		}

		this.sprites = newSprites;

		int tileX = (int)(this.player.ModelX / 64);
		int tileY = (int)(this.player.ModelY / 64);

		Tile tile = this.level.Tiles[tileX][tileY];
		if (tile != null && tile.IsDoor)
		{
			string targetLevel = this.level.DoorHookup[tile.ID - '0'];
			if (targetLevel != null)
			{
				string targetDoor = this.level.TargetDoorInfo[tile.ID - '0'];
				this.InitializeLevel(targetLevel);
				if (targetDoor == "*")
				{
					this.YouWin();
				}
				else
				{
					bool onRight = targetDoor[targetDoor.Length - 1] == '+';
					int targetDoorNum = targetDoor[0] - '0';
					int dTileX = this.level.DoorLocations[targetDoorNum][0];
					int dTileY = this.level.DoorLocations[targetDoorNum][1];
					dTileX += onRight ? 1 : -1;
					this.player.ModelX = dTileX * 64 + 32;
					this.player.ModelY = dTileY * 64 + 32;
				}
			}
		}
	}

	private int cutsceneIndex = -1;

	private Transform finalCutsceneTransform = null;

	private void YouWin()
	{
		this.cutsceneIndex = 0;
		this.cutsceneStart = System.DateTime.Now.Ticks;
		this.finalCutsceneTransform = this.AllocateTransform();
	}

	private const int SCREEN_WIDTH = 1024;
	private const int SCREEN_HEIGHT = 768;

	private void DoCutsceneRender()
	{
		this.DrawImage(this.finalCutsceneTransform, "win_" + this.cutsceneIndex, 0, 0, 1024, 768, false);
	}

	private void Render()
	{
		if (this.cutsceneIndex >= 0)
		{
			this.DoCutsceneRender();
			return;
		}

		int cameraX = (int)this.player.ModelX - SCREEN_WIDTH / 2;
		int cameraY = (int)this.player.ModelY - SCREEN_HEIGHT / 2;

		int maxWidth = this.level.Width * 64;
		int maxHeight = this.level.Height * 64;

		if (cameraX < 0) cameraX = 0;
		if (cameraY < 0) cameraY = 0;
		if (cameraX > maxWidth - SCREEN_WIDTH) cameraX = maxWidth - SCREEN_WIDTH;
		if (cameraY > maxHeight - SCREEN_HEIGHT) cameraY = maxHeight - SCREEN_HEIGHT;

		if (maxWidth < SCREEN_WIDTH)
		{
			cameraX = -(SCREEN_WIDTH - maxWidth) / 2;
		}

		if (maxHeight < SCREEN_HEIGHT)
		{
			cameraY = -(SCREEN_HEIGHT - maxHeight) / 2;
		}

		this.level.Render(this, cameraX, cameraY);

		foreach (Sprite sprite in this.sprites)
		{
			sprite.Render(this, cameraX, cameraY);
		}
	}
}
