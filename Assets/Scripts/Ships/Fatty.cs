using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatty : Enemy {

	public GameObject Projectile;
	public float FIRERATE = 2f;
	protected float CurrTime = 2f;

	public float Impulse = .5f;
	public float TargetDistance = 5f;
	public float TargetError = .5f;
	
	// Update is called once per frame
	void Update () {
		

		if(IsAggro())
		{
            float Dist = Vector2.Distance(transform.position, MainShip.Singleton.transform.position);

			//move toward target distance
			Vector2 Dir = new Vector2();
			if(Dist > TargetDistance + TargetError)
			{
				Dir = MainShip.Singleton.transform.position - transform.position;
				Dir.Normalize();
			}
			else if(Dist < TargetDistance - TargetError)
			{
				Dir = transform.position - MainShip.Singleton.transform.position;
				Dir.Normalize();
			}
			GetComponent<Rigidbody2D>().AddForce(Dir * Impulse * Time.deltaTime, ForceMode2D.Force);

			//If close enough, fire
			if(Dist < TargetDistance + TargetError)
			{
				CurrTime -= Time.deltaTime;

				if(CurrTime <= 0)
				{
					//Fire
					CurrTime += FIRERATE;
					float angle = Mathf.Atan2(MainShip.Singleton.transform.position.y - transform.position.y, MainShip.Singleton.transform.position.x - transform.position.x);

					Vector2 Displacement = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 2;
					GameObject Proj = Instantiate(Projectile, transform.position + (Vector3)Displacement, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg)) as GameObject;
				}
			}



		}
	}
}
