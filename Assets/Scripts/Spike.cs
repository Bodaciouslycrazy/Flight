using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Enemy {

	public int Damage = 10;
	public float Impulse = 3f;

	public override void Hit(int Dam)
	{
		//Do nothing. This enemy takes no damage.
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals("Player"))
		{
			col.gameObject.GetComponent<Ship>().Hit(Damage);
			Vector2 Dir = col.gameObject.transform.position - transform.position;
			Dir.Normalize();

			col.gameObject.GetComponent<Rigidbody2D>().AddForce(Dir * Impulse, ForceMode2D.Impulse);
		}
	}
}
