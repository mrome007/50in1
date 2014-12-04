using UnityEngine;
using System.Collections;

public class BoostOver : MonoBehaviour {
	public LayerMask Obstacles;
	private PlayerPhysics PP;
	private PlayerController PC;
	private bool InAirOnce;
	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		InAirOnce = false;
		PP = gameObject.GetComponent<PlayerPhysics> ();
		PC = gameObject.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!PP.Grounded)
		{
			//Debug.Log("in the air" + Time.frameCount);
			if(InAirOnce)
				return;
			ray = new Ray(transform.position,Vector3.down);
			if(Physics.Raycast(ray,out hit, 3.0f,Obstacles))
			{
				//Debug.Log("Hit Object while jumping");
				//Debug.Log(hit.collider.gameObject.name);
				InAirOnce = true;
				//boosts 15 frames;
				StartCoroutine(BoostPlayer(20));
			}
			else if(Physics.Raycast(ray,out hit, 3.0f))
			{
				if(hit.collider.tag == "Obstacle")
				{
					Debug.Log("HELLO");
					StartCoroutine(BoostPlayer(10));
				}
			}
		}
	}

	IEnumerator BoostPlayer(float frames)
	{
		float originalSpeed = PC.OrigSpeed;
		float originalAcc = PC.Acceleration;
		PC.Speed = originalSpeed + 3.0f;
		PC.Acceleration = originalAcc + 5.0f;
		int i = 0;
		while(i < frames)
		{
			i++;
			yield return 0;
		}
		PC.Speed = originalSpeed;
		PC.Acceleration = originalAcc;
		InAirOnce = false;
	}
}
