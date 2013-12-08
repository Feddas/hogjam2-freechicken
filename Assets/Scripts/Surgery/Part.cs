using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour
{
	void Start()
	{
	}

	void Update()
	{
	}

	private void OnMouseDown()
	{
		if (PartTracker.CurrentPart == null)
		{
			PartTracker.CurrentPart = this.transform;
		}
	}
}