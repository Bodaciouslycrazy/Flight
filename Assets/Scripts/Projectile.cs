using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public int Damage = 10;
	public float Speed = 8f;
	public float Timeout = 3f;

	public bool Peircing = false;
	public string Target = "";

	void Start()
	{
		float Angle = transform.eulerAngles.z * Mathf.Deg2Rad;
		GetComponent<Rigidbody2D>().velocity = Speed * new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));
	}

	void Update()
	{
		Timeout -= Time.deltaTime;
		if (Timeout <= 0)
		{
			Destroy(gameObject);
			return;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!(Peircing && other.tag.Equals("Enemy")) )
		{
			Destroy(gameObject);
		}
	}
}
