using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public static List<Ship> Ships = new List<Ship>();

	public int Health = 100;
	public int MaxHealth = 100;

    public AudioClip DeathSound;

    public virtual void Start()
    {
        Ships.Add(this);
    }

	public virtual void Hit(int Dam, GameObject Source)
	{
		Health -= Dam;
		if (Health <= 0)
		{
            Kill();
		}
	}

    public virtual void Heal(int Value)
    {
        Health += Value;
        if (Health > MaxHealth)
            Health = MaxHealth;
    }

    public virtual void Kill()
    {
        
        Ships.Remove(this);
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(DeathSound, transform.position);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Projectile Proj = other.GetComponent<Projectile>();
		if (Proj != null && Proj.Target.Equals(tag))
		{
			Hit(Proj.Damage, Proj.gameObject);
		}
	}

    
}
