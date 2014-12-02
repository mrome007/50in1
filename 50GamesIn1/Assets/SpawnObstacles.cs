using UnityEngine;
using System.Collections;

public class SpawnObstacles : MonoBehaviour 
{
	public Vector3 SpawnPos;
	public GameObject Spikes;
	public float Timer = 5.0f;
	// Use this for initialization
	void Start () 
	{
		SpawnPos = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer -= Time.deltaTime;
		SpawnPos.x = transform.position.x;
		if(Timer <= 0)
		{
			Timer = 3.0f;
			Instantiate(Spikes,SpawnPos,Quaternion.Euler(new Vector3(0.0f,0.0f,45.0f)));
		}
	}
}
