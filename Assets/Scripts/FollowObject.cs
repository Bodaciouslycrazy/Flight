using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	public Transform Following;

	public float SpeedPerUnit = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Need to do calculus
		Vector2 Target = Following.position;
		Vector2 Pos = transform.position;
		float vel =  SpeedPerUnit * Vector2.Distance(Target, Pos);
		Vector2 Dir = Target - Pos;
		Dir.Normalize();

		Pos += Dir * vel * Time.deltaTime;



		transform.position = (Vector3)Pos + new Vector3(0,0,transform.position.z);
	}
}
