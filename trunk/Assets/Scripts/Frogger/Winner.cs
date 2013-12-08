using UnityEngine;
using System.Collections;

public class Winner : MonoBehaviour
{
	public GameObject ufo;

	void Start()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			col.gameObject.rigidbody2D.velocity = new Vector2(0,0);
			PlayerControls.moveForce = 0;
			//col.gameObject.rigidbody2D.isKinematic = true;

			SendUFO();
		}
	}

	void SendUFO()
	{
		var clone = Instantiate(ufo, new Vector2(10,transform.position.y), Quaternion.identity);
		((GameObject) clone).rigidbody2D.velocity = new Vector2 (-2.5f, 0);
	}
}
