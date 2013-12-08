using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour {
	public Transform Knife;

	void Start ()
	{
	
	}

	void Update()
	{
		if (Knife == null)
			return;

		// Construct a ray from the current mouse coordinates
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		//Debug.DrawLine(ray.origin, ray.direction*1, Color.cyan);
		
		Knife.position = ray.origin;
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit))
		{
			hit.transform.renderer.material.color = Color.red;

			// Create a particle if hit
			//Instantiate (particle, transform.position, transform.rotation);
			Debug.Log("hit");
			Destroy(Knife.gameObject);
		}
		else
		{
		}

		//camera.ViewportPointToRay(Input.mousePosition);
//		//knife.position = Input.mousePosition;
//		if (Input.GetButtonDown ("Fire1"))
//		{
//			RaycastHit hit = new RaycastHit();
//
//			if (Physics.Raycast(ray, out hit))
//			{
//				// Create a particle if hit
//				//Instantiate (particle, transform.position, transform.rotation);
//				Debug.Log("hit");
//			}
//			Debug.DrawLine (transform.position, hit.point, Color.cyan);
//		}
	}
}