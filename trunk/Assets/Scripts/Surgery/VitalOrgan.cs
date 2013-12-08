using UnityEngine;
using System.Collections;

public class VitalOrgan : MonoBehaviour
{
	public ParticleSystem bloodsplat;

	void Start()
	{
	
	}

	void Update()
	{
	
	}

	private void OnCollisionEnter2D(Collision2D hitInfo)
	{
		if (bloodsplat == null)
			return;

		foreach (var contact in hitInfo.contacts)
		{
			Vector3 contact3d = new Vector3(contact.point.x, contact.point.y, 0);

			// Set the sorting layer of the particle system. http://answers.unity3d.com/questions/579490/unity-43-particle-system-not-visible-in-2d-mode.html
			bloodsplat.renderer.sortingLayerName = "Foreground";
			bloodsplat.renderer.sortingOrder = 2;
			Instantiate(bloodsplat, contact3d, this.transform.rotation);
		}
	}
}
