using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLauncher : GunControl {

    private bool Firing = false;
    public float LaunchForce = 3f;

    void Update()
    {

        //Update Angle
        UpdateRotation();
        Util.UpdateButton(ref Button);

        if (Button.Pressed)
            Firing = true;
        if (Button.Released)
            Firing = false;


        CurrTime -= Time.deltaTime;

        //Fire gun
        if (CurrTime <= 0)
        {

            if (Firing)
            {
                if (transform.parent.GetComponent<MainShip>().GetEnergy(EnergyPerShot))
                {
                    Fire();
                    AudioSource.PlayClipAtPoint(FireSound, transform.position);
                    CurrTime += FIRERATE;
                }
                else
                {
                    Firing = false; //force release
                }

            }
            else
            {
                CurrTime = 0f;
            }
        }

        SetFill();

    }

    public override void Fire()
    {
        //Fire projectile
        Transform gun = transform.GetChild(0);
        GameObject Fired = (GameObject)Instantiate(Projectile, gun.position + (transform.right * Offset), transform.rotation);
        Fired.GetComponent<Rigidbody2D>().AddForce(transform.right * LaunchForce, ForceMode2D.Impulse);
        //Projectile Proj = Fired.GetComponent<Projectile>();


        //Create Knockback
        float Angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 Force = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * -Knockback;
        transform.parent.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
    }
}
