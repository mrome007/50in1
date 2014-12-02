using UnityEngine;
using System.Collections;

public class MoveEnemy : MonoBehaviour
{
	public float Speed = 14.0f;
	public float Acceleration = 12.0f;
	public float CurrentSpeed;
	private Vector3 AmountToMove;
	private float TargetSpeed;
	// Use this for initialization
	void Start () 
	{
		CurrentSpeed = 14.0f;
		AmountToMove = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TargetSpeed = Speed;
		CurrentSpeed = IncrementToward (CurrentSpeed, TargetSpeed, Acceleration);
		AmountToMove.x = CurrentSpeed;
		transform.Translate (AmountToMove * Time.deltaTime);
	}

	private float IncrementToward(float n, float target, float acc)
	{
		if(n == target)
			return n;
		else
		{
			float dir = Mathf.Sign(target - n);
			n += acc * Time.deltaTime * dir;
			if(dir == Mathf.Sign(target - n))
				return n;
			else
				return target;
		}
	}
}
