using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy {

	public int Damage = 5;
	public float Impulse = 5;
	public float JUMPRATE = 2;
	public float RandomAngle = 45; //degrees
	private float CurrTime = 2;
	
	void Start()
	{
		CurrTime = Random.value * JUMPRATE;
	}

	// Update is called once per frame
	void Update () {
		CurrTime -= Time.deltaTime;

		float Dist = Vector2.Distance(transform.position, MainShip.Singleton.transform.position);

		RaycastHit2D RC = Physics2D.Raycast(transform.position, MainShip.Singleton.transform.position - transform.position, AggroDist);

		
		if (CurrTime <= 0 && RC.collider != null && RC.collider.Equals(MainShip.Singleton.GetComponent<Collider2D>()))
		{
			CurrTime += JUMPRATE;

			Vector2 Force = MainShip.Singleton.transform.position - transform.position;
			float Angle = Mathf.Atan2(Force.y, Force.x) + (Random.value * RandomAngle * Mathf.Deg2Rad) - (RandomAngle * Mathf.Deg2Rad / 2f);
			Force = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * Impulse;

			GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);

		}
		else if (CurrTime <= 0)
			CurrTime = 0;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//Blow the fuck up
		if (col.gameObject.tag.Equals("Player"))
		{
			col.gameObject.GetComponent<Ship>().Hit(Damage, gameObject);
			Destroy(gameObject);
		}

		
	}
}
