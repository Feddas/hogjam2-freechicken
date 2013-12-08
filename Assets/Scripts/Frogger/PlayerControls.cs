using UnityEngine;
using System;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
	public static float moveForce = 365f;			// Amount of force added to move the player left and right.
	public static float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	//private Animator anim;				// Reference to the player's animator component.
	
	void Awake()
	{

	}

	void Update()
	{
		float h = rigidbody2D.velocity.x;
		float v = rigidbody2D.velocity.y;

		float tiltAroundZ = Mathf.Atan2(v, h) * (180f / Mathf.PI) + (90f * Mathf.PI / 180f);
		var target = Quaternion.Euler(0, 0, tiltAroundZ);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 6.0F);

	}

	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		if(v * rigidbody2D.velocity.y < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.up * v * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		if(Mathf.Abs(rigidbody2D.velocity.y) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, Mathf.Sign(rigidbody2D.velocity.y) * maxSpeed);
	
	}
}
