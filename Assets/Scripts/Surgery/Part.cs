using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
	
	}
	
	// Update is called once per frame
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