using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfiniteLevel : MonoBehaviour
{
	public GameObject TheSurfaceObject;
	private Queue<GameObject> SurfaceList;
	private float XPosition;
	private float SpawnPositionOffset = 15.0f;

	private float XPositionPlus;
	GameObject organizeSurface;
	GameObject s;
	Vector3 surfacepos;
	// Use this for initialization
	void Start () 
	{
		SurfaceList = new Queue<GameObject> ();
		organizeSurface = new GameObject ("OrganizeTheSurfaces");
		organizeSurface.transform.position = Vector3.zero;
		organizeSurface.transform.rotation = Quaternion.identity;
		XPosition = 0.0f;
		surfacepos = new Vector3 (XPosition, 0.0f, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		surfacepos += new Vector3 (SpawnPositionOffset, 0.0f, 0.0f);
		s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
		SurfaceList.Enqueue (s);
		s.transform.parent = organizeSurface.transform;
		XPositionPlus = 30.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.x >= XPosition)
		{
			XPosition += SpawnPositionOffset;
			surfacepos += new Vector3(SpawnPositionOffset,0,0);
			s = (GameObject)Instantiate(TheSurfaceObject,surfacepos,Quaternion.identity);
			SurfaceList.Enqueue(s);
			s.transform.parent = organizeSurface.transform;
		}
		if(transform.position.x >= XPositionPlus)
		{
			XPositionPlus += SpawnPositionOffset;
			GameObject deact = SurfaceList.Dequeue();
			deact.SetActive(false);
		}
	}
}
