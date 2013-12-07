using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public Transform cube;

	// Use this for initialization
	void Start ()
	{
		var test = Instantiate(cube) as Transform;
		Vector3 pos = new Vector3(0,0,0);
		test.transform.position = pos;
		test.renderer.material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {
	//Time.deltaTime
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			;
	}
}
