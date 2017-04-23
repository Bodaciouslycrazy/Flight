using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

	private float StartTime = 0f;
	public float DecayTime = 1f;

	// Use this for initialization
	void Start () {
		StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float amt = Time.time - StartTime;

		if(amt > DecayTime)
		{
			Destroy(gameObject);
		}
		else
		{
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f - (amt / DecayTime));
		}
	}
}
