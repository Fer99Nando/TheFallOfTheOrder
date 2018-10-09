using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour 
{

	public Transform lookAt;

	private CameraCollision camColl;

	Vector3 dir;

	public float distance;
	private float fixedDist;

	// Use this for initialization
	void Start () 
	{
		this.dir = new Vector3 (0, 0, this.distance);
		transform.position = this.lookAt.position - this.lookAt.rotation * this.dir;
		transform.LookAt (this.lookAt);
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.distance += Input.GetAxis("Mouse ScrollWheel");
		this.distance = Mathf.Clamp(distance, 1.6f, 6f);

		this.fixedDist = camColl.GetCurrentDist ();
	}

	void LateUpdate ()
	{
		this.dir.Set(0, 0, this.distance);

		transform.position = this.lookAt.position - this.lookAt.rotation * this.dir;
		transform.LookAt (this.lookAt);
	}



}
