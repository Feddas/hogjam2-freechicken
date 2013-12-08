﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class VitalOrgan : MonoBehaviour
{
	public ParticleSystem bloodsplat;
	public GameObject ValidChip;

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

		if (ValidChip != null && hitInfo.gameObject == ValidChip)
		{
			if (PartTracker.MicroChipsRemainingList != null
			    && PartTracker.MicroChipsRemainingList.Contains(ValidChip))
			{
				PartTracker.MicroChipsRemainingList.Remove(ValidChip);
				if (PartTracker.MicroChipsRemainingList.Count == 0)
				{
					//Debug.Log("level completed");
					Application.LoadLevel("Platformer");
				}
			}

			Destroy(ValidChip);
		}
		else
		{
			gushBlood(hitInfo.contacts);
		}
	}

	private void gushBlood(ContactPoint2D[] contacts)
	{
		foreach (var contact in contacts)
		{
			Vector3 contact3d = new Vector3(contact.point.x, contact.point.y, 0);
			
			// Set the sorting layer of the particle system. http://answers.unity3d.com/questions/579490/unity-43-particle-system-not-visible-in-2d-mode.html
			bloodsplat.renderer.sortingLayerName = "Foreground";
			bloodsplat.renderer.sortingOrder = 2;
			var bloodParticles = Instantiate(bloodsplat, contact3d, this.transform.rotation) as ParticleSystem;
			Destroy(bloodParticles.gameObject, 3.0f);
		}
	}
}