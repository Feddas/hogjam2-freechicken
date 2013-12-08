using UnityEngine;
using System.Collections;

public class PartTracker : MonoBehaviour
{
	public static Transform CurrentPart;

	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (CurrentPart != null)
		{
			movePart(CurrentPart);
		}
	}

	private void movePart(Transform CurrentPart)
	{
		// Construct a ray from the current mouse coordinates
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		Vector3 pos2D = ray.origin;
		pos2D.z = 0;
		CurrentPart.position = pos2D;
	}

}
