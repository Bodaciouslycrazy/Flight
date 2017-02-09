using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int Damage = 10;
	public float Speed = 8f;
	public float Timeout = 3f;

	public string Target = "";

	void Update()
	{
		Timeout -= Time.deltaTime;
		if (Timeout <= 0)
		{
			Destroy(gameObject);
			return;
		}

		
		float Dist = Speed * Time.deltaTime;
		float Angle = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 Displacement = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * Dist;


		Vector2 NewPos = (Vector2)transform.position + Displacement;
		transform.position = NewPos;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Destroy(gameObject);
	}
}
