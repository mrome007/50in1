using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private float Speed = 10.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector3.right * Speed * Time.deltaTime);
	}
}
