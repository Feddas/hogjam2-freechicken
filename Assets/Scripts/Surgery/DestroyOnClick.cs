using UnityEngine;
using System.Collections;

public class DestroyOnClick : MonoBehaviour
{
	void Start()
	{
	}
	
	void Update()
	{
	}

	private void OnMouseDown()
	{
		Destroy(this.gameObject);
	}
}