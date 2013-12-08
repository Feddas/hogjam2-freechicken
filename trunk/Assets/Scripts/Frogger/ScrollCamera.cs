using UnityEngine;
using System.Collections;

public class ScrollCamera : MonoBehaviour
{
	public AnimationCurve curve ;
	Vector3 cameraTarget;
	Vector3 cameraOriginal;
	bool isCameraMoving = false;
	float timerScroll = 0;
	bool doneMove = false;
	public float targetY;

	void Start()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			cameraOriginal = Camera.main.transform.position;
			cameraTarget = new Vector3(cameraOriginal.x,targetY,cameraOriginal.z);

			if (doneMove == false)
			{
				doneMove = true;
				col.gameObject.rigidbody2D.velocity = new Vector2(0,0.5f);
				isCameraMoving = true;
			}

		}
	}

	void Update()
	{
		if (isCameraMoving) {
						timerScroll += Time.deltaTime / 2;
						Camera.main.transform.position = Vector3.Lerp (cameraOriginal, cameraTarget, curve.Evaluate( timerScroll));
				}

		if (Camera.main.transform.position.y >= cameraTarget.y)
						isCameraMoving = false;
	}
}
