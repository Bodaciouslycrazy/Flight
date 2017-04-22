using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : Projectile {

	public int ExplosionDamage = 50;
	public AudioClip Sound;
	public float Radius = 2f;
	public float ActivationTime = .5f;

	public int Raycasts = 32;

	void Update()
	{
		Timeout -= Time.deltaTime;
		ActivationTime -= Time.deltaTime;

		if (Timeout <= 0)
		{
			Explode();
			return;
		}

		if( ActivationTime <= 0)
		{
			Damage = 0;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(ActivationTime > 0)
		{
			Destroy(gameObject);
		}
		else
		{
			Explode();
		}
	}

	public void Explode()
	{
		List<GameObject> Targets = new List<GameObject>();
		float Angle = 0;
		float Interval = (2f * Mathf.PI) / Raycasts;
		for(int i = 0; i < Raycasts; i++)
		{
			Vector2 Dir = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle));
			RaycastHit2D Hit = Physics2D.Raycast(transform.position, Dir, Radius);

			if (Hit.collider != null && Hit.collider.gameObject.tag.Equals(Target) && !Targets.Contains(Hit.collider.gameObject))
			{
				Targets.Add(Hit.collider.gameObject);
			}

			Angle += Interval;
		}


		for(int i = 0; i < Targets.Count; i++)
		{
			Targets[i].GetComponent<Ship>().Hit(ExplosionDamage);
		}

		Util.AudioShot(Sound, transform.position);
		Destroy(gameObject);
	}

}
