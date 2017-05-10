using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour {

	public Transform Obj;

	// Update is called once per frame
	void Update () {
		if(Obj != null)
			transform.position = Obj.position;
	}
}
