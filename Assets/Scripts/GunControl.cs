using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunControl : MonoBehaviour {

	[Tooltip("Prefab of the projectile this gun shoots.")]
	public GameObject Projectile;

	public SpriteRenderer Outline;
    public SpriteRenderer Fill;

    public AudioClip FireSound;

	public float FIRERATE = .33f;
	protected float CurrTime = .33f;

	public float Knockback;
	public short EnergyPerShot = 3;
	public short PlayerNum = 1;
	public float Offset = 0;
	public float RotationSpeed = 120;

    public Util.ButtonInfo Button;

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
			Outline.color = PlayerColors[pl - 1];
            Fill.color = PlayerColors[pl - 1];

			Button = Util.NewButton("A" + PlayerNum);
		}
		else
		{
			Debug.Log("That player number does not exist!");
		}
	}

	// Update is called once per frame
	void Update () {

		UpdateRotation();
	}

	public void UpdateRotation()
	{
		float x = Input.GetAxis("X" + PlayerNum);
		float R = RotationSpeed * x * Time.deltaTime;
		R += transform.eulerAngles.z;
		transform.eulerAngles = new Vector3(0, 0, R);
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
	}


    public void SetFill()
    {
        float scale =   1- (CurrTime / FIRERATE);
        Vector3 NS = Fill.transform.localScale;
        NS.x = scale * 2;
        Fill.transform.localScale = NS;
    }
}
