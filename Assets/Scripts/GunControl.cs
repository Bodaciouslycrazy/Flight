using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour {

	[Tooltip("Prefab of the projectile this gun shoots.")]
	public GameObject Projectile;

	public SpriteRenderer Pointer;


	public float FIRERATE = .33f;
	protected float CurrTime = .33f;

	public float Knockback;
	public short HeatPerShot = 3;
	public short PlayerNum = 1;
	public float Offset = 0;

	readonly Color[] PlayerColors = {	new Color(0,255,0),
										new Color(0,0,255),
										new Color(255,0,0),
										new Color(255,255,0)};

	void Start()
	{
		SetPlayer(PlayerNum);
		transform.parent.GetComponent<MainShip>().Guns.Add(this);
	}

	public void SetPlayer(short pl)
	{
		if(pl >= 1 && pl <= 4)
		{
			PlayerNum = pl;
			Pointer.color = PlayerColors[pl - 1];
		}
		else
		{
			Debug.Log("That player number does not exist!");
		}
	}

	// Update is called once per frame
	void Update () {

		UpdateRotation();

		if(Input.GetAxisRaw("A" + PlayerNum) >= 1f)
		{
			Fire();
		}
	}

	public void UpdateRotation()
	{
		float y = Input.GetAxis("Y" + PlayerNum);
		float x = Input.GetAxis("X" + PlayerNum);


		if (Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) > 0.5) //deadzone
		{
			float TargetAngle = Mathf.Atan2(Input.GetAxis("Y" + PlayerNum), Input.GetAxis("X" + PlayerNum));
			transform.eulerAngles = new Vector3(0, 0, TargetAngle * Mathf.Rad2Deg);
		}
	}

	public virtual void Fire()
	{
		//Fire projectile
		Transform gun = transform.GetChild(0);
		GameObject Fired = (GameObject)Instantiate(Projectile, gun.position + (transform.right * Offset), transform.rotation);
		//Projectile Proj = Fired.GetComponent<Projectile>();


		//Create Knockback
		float Angle = transform.eulerAngles.z * Mathf.Deg2Rad;
		Vector2 Force = new Vector2(Mathf.Cos(Angle), Mathf.Sin(Angle)) * -Knockback;
		transform.parent.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);

		//Add Heat
		transform.parent.GetComponent<MainShip>().Heat += HeatPerShot;
		
	}
}
