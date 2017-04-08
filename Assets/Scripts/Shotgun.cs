using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : GunControl {

	public float Spread = 90;
	public int Pellets = 8;

    public bool Firing = false;

	void Update()
	{
		UpdateRotation();
        UpdateButton();

        if (Button.Pressed)
            Firing = true;
        if (Button.Released)
            Firing = false;

		//Fire Gun
		CurrTime -= Time.deltaTime;

		if (CurrTime <= 0)
		{
			if (Firing)
			{
                if (transform.parent.GetComponent<MainShip>().GetEnergy(EnergyPerShot))
                {
                    Fire();
                    Util.AudioShot(FireSound, transform.position);
                    CurrTime += FIRERATE;
                }
                else
                {
                    Firing = false;
                }
			}
			else
			{
				CurrTime = 0f;
			}
		}
	}

	public override void Fire()
	{
		//Fire projectile
		Transform gun = transform.GetChild(0);
        float interval = Spread / (Pellets - 1);
        float CurAngle = -(Spread / 2f);
		for(int i = 0; i < Pellets; i++)
		{
			Quaternion dir = transform.rotation;
			dir.eulerAngles = dir.eulerAngles + new Vector3(0, 0, CurAngle);
			GameObject Fired = (GameObject)Instantiate(Projectile, gun.position, dir);
            CurAngle += interval;
		}
		
		//Projectile Proj = Fired.GetComponent<Projectile>();


		//Create Knockback
		float Angle = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 Force = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * -Knockback;
		transform.parent.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
	}
}
