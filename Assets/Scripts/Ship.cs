using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public int Health = 100;
	public int MaxHealth = 100;


	public virtual void Hit(int Dam, GameObject Source)
	{
		Health -= Dam;
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Projectile Proj = other.GetComponent<Projectile>();
		if (Proj.Target.Equals(tag))
		{
			Hit(Proj.Damage, Proj.gameObject);
		}
	}
}
