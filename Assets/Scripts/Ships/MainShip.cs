using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShip : Ship {

	public static GameObject Singleton;
    protected static int[] GunNums = { -1,-1,-1,-1};

	[Header("Prefab References")]
    public GameObject[] GunPrefabs;
	public GameObject DeathEffect;

	[Header("Variables")]
    public short Energy = 100;
	public short MaxEnergy = 100;
    public float Recharge = 2f;
    private float CurRecharge = 2f;
	
	private float TimeBank = 0f;

	[Header("Images")]
	public Image HealthBar;
	public Image EnergyBar;

	[Header("Audio")]
    public AudioClip HurtSound;
	public AudioClip ChargeSound;
	public float VolumePerSpeed = .5f;
	public float MaxChargeVolume = .5f;

    [Header("Hud Display Options")]
    public float Delay = 3f;
    public float Decay = 1f;
    public float MaxAlpha = .25f;
    protected float CurHealthShow = 0f;
    protected float CurEnergyShow = 0f;
    

	public List<GunControl> Guns = new List<GunControl>();

	public override void Start()
	{
        base.Start();
        Singleton = gameObject;

        for(int i = 0; i < GunNums.Length; i++)
        {
			AddGun(i, GunNums[i]);
        }
	}

	void Update () {

        CurRecharge -= Time.deltaTime;
        if(CurRecharge <= 0 && Energy < MaxEnergy)
        {
            //Recharge Energy
            Energy = MaxEnergy;
			Util.AudioShot(ChargeSound, transform);
        }

        //Display henergy and health
        CurHealthShow += Time.deltaTime;
        CurEnergyShow += Time.deltaTime;

		HealthBar.fillAmount = (float)Health / (float)MaxHealth;
        HealthBar.color = GetHealthColor();

		EnergyBar.fillAmount = (float)Energy / (float)MaxEnergy;

		//Change Hum Volume
		float TargetVol = GetComponent<Rigidbody2D>().velocity.magnitude * VolumePerSpeed;
		if (TargetVol > MaxChargeVolume)
			TargetVol = MaxChargeVolume;
		float Diff = (TargetVol - GetComponent<AudioSource>().volume) * .25f;
		GetComponent<AudioSource>().volume += Diff;
	}

	//Random Functions

	public void SetGunsEnable(bool e)
	{
		for(int i = 0; i < Guns.Count; i++)
		{
			Guns[i].enabled = e;
		}
	}

    public bool GetEnergy(short amt)
    {
        
        if (Energy >= amt)
        {
            Energy -= amt;
            CurRecharge = Recharge;
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetHealthAlpha()
    {
        float Show = 0f;
        if (CurHealthShow < Delay)
        {
            Show = 1f;
        }
        else if(CurHealthShow - Delay < Decay)
        {
            Show = (1 - CurHealthShow + Delay) / Decay;
        }

        return Show * MaxAlpha;
    }

    public Color GetHealthColor()
    {
        float G = 0f;
        float R = 0f;

        if(Health > MaxHealth / 2f)
        {
            G = 1f;
            R = (-2f * Health / MaxHealth) + 2f;
        }
        else
        {
            R = 1f;
            G = 2f * Health / MaxHealth;
        }

        return new Color(R, G, 0, GetHealthAlpha());
    }


    //overrides
    public override void Hit(int Dam)
    {
        base.Hit(Dam);
        CurHealthShow = 0f;
        Util.AudioShot(HurtSound, transform.position);
    }

    public override void Heal(int Value)
    {
        base.Heal(Value);
        CurHealthShow = 0f;
        //play heal sound
    }

	public override void Kill()
	{
		Instantiate(DeathEffect, transform.position, Quaternion.identity);
		base.Kill();
	}


	//Change Guns
	public static int GetGunNum(int PlayerNum)
	{
		return GunNums[PlayerNum];
	}

	public static void ChangeGun(int PlayerNum, int GunNum)
	{
		if(Singleton != null && GunNum >= 0 && GunNums[PlayerNum] < 0)
		{
			Singleton.GetComponent<MainShip>().AddGun(PlayerNum, GunNum);
			GunNums[PlayerNum] = GunNum;
		}
		else if(Singleton != null && GunNum < 0 && GunNums[PlayerNum] >= 0)
		{
			Singleton.GetComponent<MainShip>().DeleteGun(PlayerNum);
			GunNums[PlayerNum] = GunNum;
		}
		else
		{
			Debug.LogError("I can't change a player's gun, pnly add or remove.");
		}
	}

	public void AddGun(int PlayerNumber, int GunNumber)
	{
		if (GunNumber >= 0 && GunNumber < GunPrefabs.Length)
		{
			//Create Instance of gun
			GunControl G = Instantiate(GunPrefabs[GunNumber], transform.position, transform.rotation, transform).GetComponent<GunControl>();
			G.SetPlayer((short)(PlayerNumber + 1));
		}
		else
		{
			Debug.LogWarning("Gun number " + GunNumber + " does not exist.");
		}
	}

	public void DeleteGun(int PlayerNumber)
	{
		for(int i = 0; i < Guns.Count; i++)
		{
			if (Guns[i].PlayerNum == PlayerNumber)
			{
				Destroy(Guns[i].gameObject);
				Guns.RemoveAt(i);
				i--;
			}
		}
	}




}