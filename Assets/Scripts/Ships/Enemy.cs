using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship {

	public float AggroDist = 10;

    public virtual bool IsAggro()
    {
        if(Vector2.Distance(transform.position, MainShip.Singleton.transform.position) > AggroDist)
        {
            return false;
        }
        else
        {
            RaycastHit2D RC = Physics2D.Raycast(transform.position, MainShip.Singleton.transform.position - transform.position, AggroDist);

            if (RC.collider != null && RC.collider.Equals(MainShip.Singleton.GetComponent<Collider2D>()))
            {
                return true;
            }
            else
                return false;
        }

    }
}
