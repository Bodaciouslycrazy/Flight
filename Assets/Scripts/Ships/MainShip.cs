using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShip : Ship {

	public static GameObject Singleton;
    public static int[] GunNums = { 3, -1, -1, -1 };
    public GameObject[] GunPrefabs;

    public short Energy = 100;
	public short MaxEnergy = 100;
    public float Recharge = 2f;
    private float CurRecharge = 2f;
	
	private float TimeBank = 0f;

	public Image HealthBar;
	public Image EnergyBar;

    public AudioClip HurtSound;

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
            if(GunNums[i] >= 0 && GunNums[i] < GunPrefabs.Length)
            {
                //Create Instance of gun
                GunControl G = Instantiate(GunPrefabs[GunNums[i]], transform.position, transform.rotation, transform).GetComponent<GunControl>();
                G.SetPlayer((short)(i + 1));
            }
            else
            {
                Debug.LogWarning("Gun number " + GunNums[i] + " does not exist.");
            }
        }
	}

	void Update () {

        CurRecharge -= Time.deltaTime;
        if(CurRecharge <= 0 && Energy < MaxEnergy)
        {
            //Recharge Energy
            Energy = MaxEnergy;
        }

        //Display henergy and health
        CurHealthShow += Time.deltaTime;
        CurEnergyShow += Time.deltaTime;

		HealthBar.fillAmount = (float)Health / (float)MaxHealth;
        HealthBar.color = GetHealthColor();

		EnergyBar.fillAmount = (float)Energy / (float)MaxEnergy;

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
}
