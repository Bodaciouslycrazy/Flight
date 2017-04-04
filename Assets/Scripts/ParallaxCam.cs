using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCam : MonoBehaviour {

    public float ParaxPercent = 1f;
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = Camera.main.transform.position;
        float z = pos.z;
        pos = pos * ParaxPercent;
        pos.z = z;
        transform.position = pos;
	}
}
