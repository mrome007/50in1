using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour 
{
	public Vector3 SpawnPos;
	public GameObject Spikes;
	public GameObject Box;
	public GameObject Projectile;
	public float Timer = 5.0f;
	private List<GameObject> ObstacleList;
	Vector3 pos;
	GameObject obs;
	GameObject obstacleOrganizer;
	// Use this for initialization
	void Start () 
	{
		SpawnPos = Vector3.zero;
		obstacleOrganizer = new GameObject ("ObstacleOrganizer");
		obstacleOrganizer.transform.position = Vector3.zero;
		ObstacleList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer -= Time.deltaTime;
		SpawnPos.x = transform.position.x;
		if(Timer <= 0)
		{
			Timer = 3.0f;
			int x = Random.Range(0,4);
			if(ObstacleList.Count > 0)
			{
				for(int i = 0; i < ObstacleList.Count; i++)
				{
					ObstacleList[i].SetActive(false);
				}
			}
			switch(x)
			{
			case 0:
				Debug.Log("Spikes");
				pos = SpawnPos;
				for(int i = 0; i < Random.Range(1,6); i++)
				{
					obs = (GameObject)Instantiate(Spikes,pos,Quaternion.Euler(new Vector3(0.0f,0.0f,45.0f)));
					ObstacleList.Add (obs);
					obs.transform.parent = obstacleOrganizer.transform;
					pos += new Vector3(2.0f,0.0f,0.0f);
				}
				break;
			case 1: //just boxes
				Debug.Log("boxes");
				pos = new Vector3(SpawnPos.x,1.0f,SpawnPos.z);
				for(int i = 0; i < Random.Range(1,3); i++)
				{
					obs = (GameObject)Instantiate(Box,pos,Quaternion.identity);
					ObstacleList.Add(obs);
					obs.transform.parent = obstacleOrganizer.transform;
					obs = (GameObject)Instantiate(Box,pos + new Vector3(9.0f,0.0f,0.0f),Quaternion.identity);
					ObstacleList.Add(obs);
					obs.transform.parent = obstacleOrganizer.transform;
					pos += new Vector3(4.5f,0.0f,0.0f);
				}
				break;
			case 2: //scaling boxes
				Debug.Log("scaling boxes");
				pos = SpawnPos;
				for(int i = 0; i < Random.Range(1,6); i++)
				{
					pos = new Vector3(pos.x,(i+2.0f)/2.0f,pos.z);
					obs = (GameObject)Instantiate(Box,pos,Quaternion.identity);
					obs.transform.localScale = new Vector3(i+1,i+1,i+1);
					ObstacleList.Add(obs);
					obs.transform.parent = obstacleOrganizer.transform;
					pos += new Vector3(i+2,0.0f,0.0f);
				}
				break;
			case 3:
				break;
			default:
				obs = (GameObject)Instantiate(Spikes,SpawnPos,Quaternion.Euler(new Vector3(0.0f,0.0f,45.0f)));
				ObstacleList.Add(obs);
				break;
			}
		}
	}
}
