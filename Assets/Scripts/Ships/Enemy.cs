using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Ship {

	public float AggroDist = 10;

    public virtual bool IsAggro()
    {
        if(MainShip.Singleton != null && Vector2.Distance(transform.position, MainShip.Singleton.transform.position) > AggroDist)
        {
            return false;
        }
        else if(MainShip.Singleton != null)
        {
            RaycastHit2D RC = Physics2D.Raycast(transform.position, MainShip.Singleton.transform.position - transform.position, AggroDist);

            if (RC.collider != null && RC.collider.Equals(MainShip.Singleton.GetComponent<Collider2D>()))
            {
                return true;
            }
            else
                return false;
        }

		return false;

    }

	public override void Hit(int Dam)
	{
		base.Hit(Dam);
		Util.AudioShot(DeathSound, transform.position, 0.5f);
	}
}
