using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	public Transform Following;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 Target = Following.position;
		Vector2 Pos = transform.position;
		//float lerppos = Mathf.Min(PercentPerSecond * Time.deltaTime, 1f);

		transform.position = (Vector3)Vector2.Lerp(Pos, Target, 1f) + new Vector3(0,0,transform.position.z);
	}
}
