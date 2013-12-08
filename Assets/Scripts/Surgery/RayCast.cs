using UnityEngine;
using System.Collections;

public class RayCast : MonoBehaviour
{
	public Transform Knife;
	public ParticleSystem bloodsplat;
	public Transform currentTool;

	public const string TagTool = "Tool";
	public const string TagOrgan = "VitalOrgan";

	void Start ()
	{
	
	}

	void Update()
	{
		if (currentTool != null)
		{
			holdingTool();
		}
		else
		{
			if (Input.GetButtonDown("Fire1"))
			{
				selectATool();
			}
		}
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

	private void holdingTool()
	{
//		if (Knife == null)
//			return;
		
		// Construct a ray from the current mouse coordinates
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		Vector3 pos2D = ray.origin;
		pos2D.z = 0;
		currentTool.position = pos2D;
		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit) && hit.transform.tag == TagOrgan)
		{
			//Debug.Log("hit" + Vector3.up + "; " + hit.normal.ToString());
			Destroy(currentTool.gameObject);
			currentTool = null;
			
			// Create blood
			hit.transform.renderer.material.color = Color.red;
			var hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
			Instantiate(bloodsplat, ray.origin, hitRotation);
		}
	}

	private void selectATool()
	{
		// Construct a ray from the current mouse coordinates
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit = new RaycastHit();
		if (Physics.Raycast(ray, out hit) && hit.transform.tag == TagTool)
		{
			currentTool = hit.transform;
		}
	}
}