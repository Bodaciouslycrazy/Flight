using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public float SplashRange = 2;
    public int Damage = 15;
    public int SelfDamage = 10;
    public float DetonationTime = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DetonationTime -= Time.deltaTime;

        if (DetonationTime <= 0)
            Explode();
	}

    public void Explode()
    {

        for(int i = 0; i < Ship.Ships.Count; i++)
        {
            float Dist = Vector2.Distance(transform.position, Ship.Ships[i].transform.position);

            if(Dist <= SplashRange)
            {
                if(Ship.Ships[i].tag.Equals("Player"))
                {
                    //Ship.Ships[i].Hit(SelfDamage);
                }
                else
                {
                    Ship.Ships[i].Hit(Damage);
                }
            }
        }

        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        Explode();
    }
}
