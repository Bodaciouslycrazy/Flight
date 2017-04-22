using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : Enemy {

	public int Damage = 5;
	public float Impulse = 5;
	public float JUMPRATE = 2;
	public float RandomAngle = 45; //degrees
	private float CurrTime = 2;

	public AudioClip JumpSound;
	
	public override void Start()
	{
        base.Start();
        CurrTime = Random.value * JUMPRATE;
	}

	// Update is called once per frame
	void Update () {
		CurrTime -= Time.deltaTime;
		
		if (CurrTime <= 0 && IsAggro())
		{
			CurrTime += JUMPRATE;

			Vector2 Force = MainShip.Singleton.transform.position - transform.position;
			float Angle = Mathf.Atan2(Force.y, Force.x) + (Random.value * RandomAngle * Mathf.Deg2Rad) - (RandomAngle * Mathf.Deg2Rad / 2f);
			Force = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * Impulse;

			transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Angle);
			GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);

			//Sound
			Util.AudioShot(JumpSound, transform.position);

		}
		else if (CurrTime <= 0)
			CurrTime = 0;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		//Blow the fuck up
		if (col.gameObject.tag.Equals("Player"))
		{
			col.gameObject.GetComponent<Ship>().Hit(Damage);
			Destroy(gameObject);
		}

		
	}
}
