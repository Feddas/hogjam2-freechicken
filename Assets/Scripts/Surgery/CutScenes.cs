//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class CutScenes : MonoBehaviour
//{
//	public List<Material> IntroScreens;
//	public Transform quad;
//
//	private long cutsceneStart = 0;
//	private int cutsceneIndex = -1;
//	private Transform finalCutsceneTransform = null;
//	private List<Transform> freeTransforms = new List<Transform>();
//	private int z = 1;
//
//	void Start()
//	{
//		this.cutsceneIndex = 0;
//		this.cutsceneStart = System.DateTime.Now.Ticks;
//		this.finalCutsceneTransform = this.AllocateTransform();
//	}
//
//	void Update()
//	{		
////		if (this.cutsceneIndex >= 0)
////		{
//		//this.DoCutsceneUpdate();
//		//}
//	}
//
//	public void DrawImage(Transform rect, Material mat, int x, int y, int width, int height, bool reverse)
//	{
//		float unityWidth = width * 40 / 3f / 1024;
//		float unityHeight = height * 40 / 3f / 1024;
//		float unityX = this.ConvertX(x) + unityWidth / 2;
//		float unityY = this.ConvertY(y) - unityHeight / 2;
//		rect.transform.position = new Vector3(unityX, unityY, this.z-- / 10000f);
//		rect.transform.localScale = new Vector3(reverse ? -unityWidth : unityWidth, unityHeight, 1);
//		rect.renderer.material = mat;
//	}
//	
//	private void YouWin()
//	{
//		this.cutsceneIndex = 0;
//		this.cutsceneStart = System.DateTime.Now.Ticks;
//		this.finalCutsceneTransform = this.AllocateTransform();
//	}
//
//	private void DoCutsceneUpdate()
//	{
//		if (IntroScreens != null || IntroScreens.Count > 0)
//		//if (this.cutsceneIndex < 2)
//		{
//			double seconds = (System.DateTime.Now.Ticks - this.cutsceneStart) / 10000000.0;
//			if (seconds > 5)
//			{
//				//this.cutsceneIndex++;
//				this.cutsceneStart = System.DateTime.Now.Ticks;
////				if (IntroScreens == null || IntroScreens.Count == 0)
////				{
////					Destroy(this.finalCutsceneTransform);
////				}
////				else
////				{
//				this.DrawImage(this.finalCutsceneTransform, IntroScreens[0], 0, 0, 1024, 768, false);
//				IntroScreens.RemoveAt(0);
//				//if (IntroScreens.Count == 0)
//				//Destroy(this.finalCutsceneTransform);
//				//}
//			}
//		}
//	}
//
//	public Transform AllocateTransform()
//	{
//		Transform t;
//		int size = this.freeTransforms.Count;
//		if (size > 0)
//		{
//			t = this.freeTransforms[size - 1];
//			this.freeTransforms.RemoveAt(size - 1);
//		}
//		else
//		{
//			t = Instantiate(this.quad) as Transform;
//		}
//		t.transform.position = new Vector3(-999999, 0, 0);
//		return t;
//	}
//
//	private float ConvertX(int pixelX)
//	{
//		return pixelX * 40 / 3f / 1024 - 20 / 3f;
//	}
//	
//	private float ConvertY(int pixelY)
//	{
//		return -((pixelY + 128) * 40 / 3f / 1024 - 20 / 3f);
//	}
//}
