using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewBehaviourScript : MonoBehaviour
{
	public Transform cube;

	private double x = 0;
	private double y = 0;

	private Sprite player;

	private Level level;

	void Start()
	{
		this.cube.transform.position = new Vector3(999999, 999999, 0); // hide!

		this.level = new Level("level_1");
		this.player = new Sprite("player", 100, 100);
		this.sprites.Add(this.player);
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
	private bool shiftPressed = false;

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

		double dx = 0;
		double dy = 0;
		double v = 3;
		if (leftPressed) dx -= v;
		if (rightPressed) dx += v;
		if (upPressed) dy -= v;
		if (downPressed) dy += v;

		this.player.DX = dx;

		bool spacePressed = Input.GetKeyDown(KeyCode.Space);
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
			t = Instantiate(this.cube) as Transform;
		}
		t.transform.position = new Vector3(-999999, 0, 0);
		return t;
	}

	private List<Transform> freeTransforms = new List<Transform>();
	public void RemoveTransform(Transform transform)
	{
		transform.transform.position = new Vector3(-999999, 0, 0);
		this.freeTransforms.Add(transform);
	}

	private static float[] conversionOut = new float[2];
	private void ConvertCoords(int x, int y)
	{
		conversionOut[0] = x * 40 / 3f / 1024 - 20 / 3f;
		conversionOut[1] = y * 40 / 3f / 768 - 20 / 3f;
	}

	public void DrawRectangle(Transform rect, Color color, int x, int y, int width, int height)
	{
		x += width / 2;
		y += height / 2;
		float unityX = x * 40 / 3f / 1024 - 20 / 3f;
		float unityY = -(y * 40 / 3f / 1024 - 20 / 3f);
		float unityWidth = width * 40 / 3f;
		float unityHeight = height * 40 / 3f;
		rect.transform.position = new Vector3(unityX, unityY, this.z-- / 10000f);
		rect.transform.localScale = new Vector3(width / 64f, height / 64f, 1);
		rect.renderer.material.color = color;
	}

	void Update()
	{
		if (this.SkipTurn()) return;

		this.z = 0;

		this.ProcessInput();
		this.UpdateImpl();
		this.Render();
	}


	private void UpdateImpl()
	{
		foreach (Sprite sprite in this.sprites)
		{
			sprite.ApplyMovement(this.level);
		}

	}

	private const int SCREEN_WIDTH = 1024;
	private const int SCREEN_HEIGHT = 768;

	private void Render()
	{
		int cameraX = (int)this.player.ModelX - SCREEN_WIDTH / 2;
		int cameraY = (int)this.player.ModelY - SCREEN_HEIGHT / 2;

		int maxWidth = this.level.Width * 64;
		int maxHeight = this.level.Height * 64;

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
