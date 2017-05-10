using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowInterp : MonoBehaviour {

	public Transform Following;
	private Vector2 Target = new Vector2();

	public float SpeedPerUnit = 3f;
	
	// Update is called once per frame
	void Update () {

		//Need to do calculus
		if (Following != null)
			Target = Following.position;

		Vector2 Pos = transform.position;
		float vel =  SpeedPerUnit * Vector2.Distance(Target, Pos);
		Vector2 Dir = Target - Pos;
		Dir.Normalize();

		Pos += Dir * vel * Time.deltaTime;
		transform.position = (Vector3)Pos + new Vector3(0,0,transform.position.z);
	}
}
