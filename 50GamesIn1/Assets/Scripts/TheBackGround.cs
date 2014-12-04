using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheBackGround : MonoBehaviour 
{
	public GameObject BackGround;
	private Queue<GameObject> Bgs;

	private Vector3 StartPos = new Vector3(0.0f,2.0f,15.0f);
	private Vector3 StartRot = new Vector3(0.0f,0.0f,0.0f);
	private float StartingPosToComp = 0.0f;
	private float CompOffset = 76.0f;
	private float GetRidPos = 76.0f;
	float Xpos;
	GameObject organizeBgs;
	// Use this for initialization
	void Start () 
	{
		organizeBgs = new GameObject ("OrganizeBackGround");
		organizeBgs.transform.position = StartPos;
		Bgs = new Queue<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Xpos = transform.parent.transform.position.x;
		if(Xpos >= StartingPosToComp)
		{
			GameObject bg = (GameObject)Instantiate(BackGround,StartPos,Quaternion.Euler(StartRot));
			Bgs.Enqueue(bg);
			bg.transform.parent = organizeBgs.transform;
			StartPos += new Vector3(CompOffset,0.0f,0.0f);
			StartRot += new Vector3(0.0f,180.0f,0.0f);
			StartingPosToComp += CompOffset;
			bg = (GameObject)Instantiate(BackGround,StartPos,Quaternion.Euler(StartRot));
			Bgs.Enqueue(bg);
			bg.transform.parent = organizeBgs.transform;
			StartPos += new Vector3(CompOffset,0.0f,0.0f);
			StartRot -= new Vector3(0.0f,180.0f,0.0f);
			if(Bgs.Count > 2)
				StartingPosToComp += CompOffset;
		}
		if(Xpos >= GetRidPos)
		{
			GetRidPos += CompOffset;
			GameObject bg = Bgs.Dequeue();
			bg.SetActive(false);
		}
	}
}
