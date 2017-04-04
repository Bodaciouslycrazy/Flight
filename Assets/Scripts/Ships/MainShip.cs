using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShip : Ship {

	public static GameObject Singleton;
    public static int[] GunNums = { 0, -1, -1, -1 };
    public GameObject[] GunPrefabs;

    public short Energy = 100;
	public short MaxEnergy = 100;
    public float Recharge = 2f;
    private float CurRecharge = 2f;
	
	private float TimeBank = 0f;

	public Image HealthBar;
	public Image HeatBar;

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
		HealthBar.fillAmount = (float)Health / (float)MaxHealth;
		HeatBar.fillAmount = (float)Energy / (float)MaxEnergy;
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
}
