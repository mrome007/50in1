using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour 
{
	public LayerMask Surface;
	public bool Grounded;
	private BoxCollider Bollider;
	private Vector3 BSize;
	private Vector3 BCenter;

	private float Skin = 0.005f;
	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{
		Bollider = gameObject.GetComponent<BoxCollider> ();
		BSize = Bollider.size;
		BCenter = Bollider.center;
		Grounded = false;
	}
	
	public void Move(Vector3 moveAmount)
	{
		float deltaX = moveAmount.x;
		float deltaY = moveAmount.y;
		float deltaZ = moveAmount.z;

		Vector3 p = transform.position;

		for(int i = 0; i < 3; i++)
		{
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + BCenter.x - BSize.x/2) + BSize.x/2 * i; // Left, centre and then rightmost point of collider
			float y = p.y + BCenter.y + BSize.y/2 * dir;

			ray = new Ray(new Vector3(x,y,0.0f), new Vector3(0,dir,0));
			Debug.DrawRay(ray.origin,ray.direction);
			bool moveVert = Mathf.Approximately(deltaY,0.0f);
			if(!moveVert && Physics.Raycast(ray, out hit, Mathf.Abs(deltaY), Surface))
			{
				float dst = Vector3.Distance(ray.origin, hit.point);
				if (dst > Skin) 
					deltaY = dst * dir + Skin;
				else 
					deltaY = 0;
				Grounded = true;
				break;
			}
		}

		Vector3 finalTransform = new Vector3 (deltaX, deltaY, deltaZ);
		transform.Translate (finalTransform);
	}


}
