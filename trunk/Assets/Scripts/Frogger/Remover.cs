using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D col)
	{
		Destroy (col.gameObject);	

		// If the player hits the trigger...
		if(col.gameObject.tag == "Player" && tag != "UFO")
		{
			StartCoroutine("ReloadGame");
		}
	}
	
	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		Application.LoadLevel(Application.loadedLevel);
	}
}
