using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Winner : MonoBehaviour
{
	public GameObject ufo;
	public AudioClip winMusic;

	private IList<KeyCode> cheatCode = new List<KeyCode>() {KeyCode.W, KeyCode.I, KeyCode.N};

	void Start()
	{

	}

	void Update()
	{
		checkCheat();
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
			StartCoroutine(loadNextLevel());
		}
	}

	void SendUFO()
	{
		if (winMusic != null)
		{
			audio.Stop();
			audio.clip = winMusic;
			audio.Play();
		}
		var clone = Instantiate(ufo, new Vector2(10,transform.position.y), Quaternion.identity);
		((GameObject) clone).rigidbody2D.velocity = new Vector2 (-2.5f, 0);
	}

	IEnumerator loadNextLevel()
	{
		yield return new WaitForSeconds(8);
		
		Application.LoadLevel("Surgery");
	}

	private void checkCheat()
	{
		if (Input.GetKeyDown(cheatCode[0]))
		{
			cheatCode.RemoveAt(0);
			if (cheatCode.Count == 0)
			{
				Application.LoadLevel("Surgery");
			}
		}
	}
}
