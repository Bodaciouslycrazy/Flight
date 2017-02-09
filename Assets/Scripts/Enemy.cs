using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship {

	public float AggroDist = 10;
	public int Damage = 5;

	const float HitKnockback = 0;

	public override void Hit(int Dam, GameObject Source)
	{
		base.Hit(Dam, Source);
		Vector2 DX = transform.position - Source.transform.position;
		DX = DX.normalized * HitKnockback;

		GetComponent<Rigidbody2D>().AddForce(DX, ForceMode2D.Impulse);
	}
}
