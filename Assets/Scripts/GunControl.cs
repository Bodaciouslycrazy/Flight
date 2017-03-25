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

    public ButtonInfo Button;

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

            Button = new ButtonInfo();
            Button.Name = "A" + PlayerNum;
            Button.Down = false;
            Button.Pressed = false;
            Button.Released = false;
            Button.LastFrame = false;
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
	}

    public struct ButtonInfo
    {
        public string Name;
        public bool Down;
        public bool LastFrame;
        public bool Pressed;
        public bool Released;
    }

    public void UpdateButton()
    {
        //Update Input
        Button.Down = Input.GetAxisRaw(Button.Name) >= 1;
        Button.Pressed = Button.Down && !Button.LastFrame;
        Button.Released = !Button.Down && Button.LastFrame;
        Button.LastFrame = Button.Down;
    }

    public void SetFill()
    {
        float scale =   1- (CurrTime / FIRERATE);
        Vector3 NS = Fill.transform.localScale;
        NS.x = scale * 2;
        Fill.transform.localScale = NS;
    }
}
