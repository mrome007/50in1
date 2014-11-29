using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteLevel : MonoBehaviour
{
	public GameObject TheSurfaceObject;
	public GameObject TransitionBlock;
	public Queue<GameObject> SurfaceList;
	private float XPosition;
	private float SpawnPositionOffset = 15.0f;

	private float XPositionPlus;
	GameObject organizeSurface;
	GameObject s;
	Vector3 surfacepos;

	private bool InfiniteGo;
	// Use this for initialization
	void Start () 
	{
		InfiniteGo = false;
		SurfaceList = new Queue<GameObject> ();
		organizeSurface = new GameObject ("OrganizeTheSurfaces");
		organizeSurface.transform.position = Vector3.zero;
		organizeSurface.transform.rotation = Quaternion.identity;
	
		StartCoroutine (InfiniteLevelRight (0.0f,0.0f));
	}
	


	public IEnumerator InfiniteLevelRight(float currentXPos, float currentYPos)
	{
		InfiniteGo = false;
		int counter = 2;
		XPosition = currentXPos;
		surfacepos = new Vector3 (XPosition, currentYPos, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		surfacepos += new Vector3 (SpawnPositionOffset, 0.0f, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		XPositionPlus = XPosition + 30.0f;
		while(!InfiniteGo)
		{
			if(transform.position.x >= XPosition)
			{
				XPosition += SpawnPositionOffset;
				surfacepos += new Vector3(SpawnPositionOffset,0.0f,0.0f);
				s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
				SurfaceList.Enqueue(s);
				s.transform.parent = organizeSurface.transform;
				counter++;
			}
			if(transform.position.x >= XPositionPlus)
			{
				XPositionPlus += SpawnPositionOffset;
				GameObject deact = SurfaceList.Dequeue();
				deact.SetActive(false);
			}
			InfiniteGo = counter > 20;
			yield return 0;
		}

		surfacepos += new Vector3 (SpawnPositionOffset, 0.0f, 0.0f);
		GameObject tb = (GameObject)Instantiate (TransitionBlock, surfacepos, Quaternion.identity);
		BlockProperties bp = tb.GetComponent<BlockProperties> ();
		bp.ToContinue = "CONTINUE";
		bp.NextLevelDirection = "LEFT";
	}

	public IEnumerator InfiniteLevelLeft(float currentXPos, float currentYpos)
	{
		InfiniteGo = false;
		int counter = 2;
		XPosition = currentXPos;
		surfacepos = new Vector3 (XPosition, currentYpos, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		surfacepos -= new Vector3 (SpawnPositionOffset, 0.0f, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		XPositionPlus = XPosition - 30.0f;
		while(!InfiniteGo)
		{
			if(transform.position.x <= XPosition)
			{
				XPosition -= SpawnPositionOffset;
				surfacepos -= new Vector3(SpawnPositionOffset,0,0);
				s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
				counter++;
				SurfaceList.Enqueue(s);
				s.transform.parent = organizeSurface.transform;
			}
			if(transform.position.x <= XPositionPlus)
			{
				XPositionPlus -= SpawnPositionOffset;
				GameObject deact = SurfaceList.Dequeue();
				deact.SetActive(false);
			}
			InfiniteGo = counter > 20;
			yield return 0;
		}
		surfacepos -= new Vector3 (SpawnPositionOffset, 0.0f, 0.0f);
		GameObject tb = (GameObject)Instantiate (TransitionBlock, surfacepos, Quaternion.identity);
		BlockProperties bp = tb.GetComponent<BlockProperties> ();
		bp.ToContinue = "END";
		bp.NextLevelDirection = "";
	}
}
