using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticGun : Enemy {

    public GameObject Projectile;

    public float FireDelay = 1f;
    private float Current = 1f;

    void Update()
    {
        Current -= Time.deltaTime;

        if(Current <= 0)
        {
            Current += FireDelay;

            Vector2 spawn = transform.position + (transform.right * 1);

            //Fire shot
            Instantiate(Projectile, spawn, transform.rotation);

        }
    }
}
