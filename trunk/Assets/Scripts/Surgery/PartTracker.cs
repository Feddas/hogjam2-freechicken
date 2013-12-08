using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PartTracker : MonoBehaviour
{
	public static Transform CurrentPart;
	public static IList<GameObject> MicroChipsRemainingList;

	public GameObject[] MicroChipsRemaining;

	void Start()
	{
		MicroChipsRemainingList = new List<GameObject>(MicroChipsRemaining);
	}

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