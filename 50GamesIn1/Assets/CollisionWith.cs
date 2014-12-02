using UnityEngine;
using System.Collections;

public class CollisionWith : MonoBehaviour 
{
	public GameObject Block;
	private float OffsetPos = 0.05f;
	private float Speed = 0.5f;
	int countHits = 0;
	string From = "";
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Transition")
		{
			countHits++;
			Block = other.gameObject;
			Debug.Log("I hit the Transition " + countHits);
			PlayerController pc = gameObject.GetComponent<PlayerController>();
			pc.enabled = false;
			PlayerPhysics pp = gameObject.GetComponent<PlayerPhysics>();
			pp.enabled = false;
			StartCoroutine("StartNewSurfaces");
		}
		if(other.gameObject.tag == "Obstacle")
		{
			Debug.Log("Hit an obstacle");
			StartCoroutine(SlowPlayer(10));
		}
	}

	IEnumerator SlowPlayer(int frames)
	{
		PlayerController pc = gameObject.GetComponent<PlayerController> ();
		float origSpeed = pc.Speed;
		pc.Speed = origSpeed - 5.0f;
		int i = 0;
		while(i < frames)
		{
			i++;
			yield return 0;
		}
		pc.Speed = origSpeed;
	}

	IEnumerator MovePlayerToCenterFromRight()
	{
		float playerPosX = transform.position.x;
		while(playerPosX < Block.transform.position.x - OffsetPos)
		{
			transform.position = Vector3.Lerp(transform.position,Block.transform.position,Speed * Time.deltaTime);
			playerPosX = transform.position.x;
			yield return 0;
		}
		transform.position = Block.transform.position;
	}

	IEnumerator MovePlayerToCenterFromLeft()
	{
		float playerPosX = transform.position.x;
		while(playerPosX > Block.transform.position.x + OffsetPos)
		{
			transform.position = Vector3.Lerp(transform.position,Block.transform.position,Speed * Time.deltaTime);
			playerPosX = transform.position.x;
			yield return 0;
		}
		transform.position = Block.transform.position;
	}

	IEnumerator StartNewSurfaces()
	{
		if(From == "" || From == "RIGHT")
			yield return StartCoroutine("MovePlayerToCenterFromRight");
		else
			yield return StartCoroutine("MovePlayerToCenterFromLeft");
		yield return new WaitForSeconds(5.0f);
		BlockProperties bp = Block.GetComponent<BlockProperties> ();
		InfiniteLevel ifl = gameObject.GetComponent<InfiniteLevel> ();
		foreach(GameObject value in ifl.SurfaceList)
			value.SetActive(false);
		ifl.SurfaceList.Clear ();
		Debug.Log (ifl.SurfaceList.Count);
		PlayerController pc = gameObject.GetComponent<PlayerController>();
		PlayerPhysics pp = gameObject.GetComponent<PlayerPhysics>();
		if(bp.ToContinue == "CONTINUE")
		{
			if(bp.NextLevelDirection == "LEFT")
			{
				StartCoroutine(ifl.InfiniteLevelLeft(Block.transform.position.x, Block.transform.position.y - 10.0f));
				if(Mathf.Sign(pc.Speed) > 0)
					pc.Speed *= -1.0f;
			}
			else
			{
				StartCoroutine(ifl.InfiniteLevelRight(Block.transform.position.x, Block.transform.position.y - 10.0f));
				if(Mathf.Sign(pc.Speed) < 0)
					pc.Speed *= -1.0f;
			}
			From = bp.NextLevelDirection;
			pp.enabled = true;
			pc.CurrentSpeed = 0.0f;
			pc.enabled = true;
		}

	}

}
