using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private float Gravity = 45.0f;
	public float Acceleration = 12.0f;
	public float Speed = 12.0f;
	public float OrigSpeed;

	private Vector3 AmountToMove;
	public float CurrentSpeed;
	private float TargetSpeed;

	private PlayerPhysics pp;
	private int DoubleJump;
	// Use this for initialization
	void Start () 
	{
		DoubleJump = 0;
		CurrentSpeed = 0.0f;
		AmountToMove = new Vector3 ();
		OrigSpeed = Speed;
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
		if(pp.Grounded || DoubleJump < 2)
		{
			//AmountToMove.y = 0.0f;
			if(Input.GetButtonDown("Jump"))
			{
				DoubleJump++;
				pp.Grounded = false;
				AmountToMove.y = 15.0f;
			}
			if(pp.Grounded)
				DoubleJump = 0;
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
