using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private float Gravity = 20.0f;
	private float Acceleration = 12.0f;
	private float Speed = 14.0f;

	private Vector3 AmountToMove;
	private float CurrentSpeed;
	private float TargetSpeed;

	private PlayerPhysics pp;
	// Use this for initialization
	void Start () 
	{
		CurrentSpeed = 0.0f;
		AmountToMove = new Vector3 ();
		pp = gameObject.GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		TargetSpeed = Speed;
		CurrentSpeed = IncrementToward (CurrentSpeed, TargetSpeed, Acceleration);

		AmountToMove.x = CurrentSpeed;
		AmountToMove.y -= Gravity * Time.deltaTime;
		AmountToMove.z = 0.0f;
		if(pp.Grounded)
		{
			AmountToMove.y = 0.0f;
			if(Input.GetButtonDown("Jump"))
			{
				pp.Grounded = false;
				AmountToMove.y = 10.0f;
			}
		}
		pp.Move (AmountToMove * Time.deltaTime);
		//transform.Translate (AmountToMove * Time.deltaTime);
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
