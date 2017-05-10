using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigglePosition : MonoBehaviour {

    public bool LocalSpace = true;
    public Vector2 Amplitude;
    public float Speed = 1f;
    private Vector2 START;


	// Use this for initialization
	void Start () {
        START = transform.position;
        if(LocalSpace)
            Amplitude = transform.TransformDirection(Amplitude);
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 WavePos = Amplitude * Mathf.Sin(Time.time * Speed * 2 * Mathf.PI);
        transform.position = (Vector3)(START + WavePos);
    }
}
