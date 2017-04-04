using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int Value = 20;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            Ship pl = coll.gameObject.GetComponent<Ship>();

            if(pl.Health < pl.MaxHealth)
            {
                pl.Heal(Value);
                Destroy(gameObject);
            }
        }
    }
}
