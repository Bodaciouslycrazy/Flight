using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShip : Ship {

	public static GameObject Singleton;

	public short Heat = 0;
	public short MaxHeat = 100;
	private DCooldown UpdateHeat;
	public float Cooldown = 10f; //Heat per Second

	public bool Overheated = false;
	public float FastCooldown = 20f; //Heat per Second
	public float HeatDamage = 10;
	public short PenaltyEnd = 50; //when heat reaches this, the ship regains control.
	
	private float TimeBank = 0f;

	public Image HealthBar;
	public Image HeatBar;

	public List<GunControl> Guns = new List<GunControl>();

	void Start()
	{
		Singleton = gameObject;
		UpdateHeat = NormalCool;
	}

	void Update () {
		UpdateHeat();

		//Display heat and health
		HealthBar.fillAmount = (float)Health / (float)MaxHealth;
		HeatBar.fillAmount = (float)Heat / (float)MaxHeat;
	}

	//Cooldown Delegate
	private delegate void DCooldown();

	private void NormalCool()
	{
		//cooldown heat
		TimeBank += Time.deltaTime;
		int lost = Mathf.FloorToInt(TimeBank * Cooldown);
		Heat -= (short)lost;
		TimeBank -= lost / Cooldown;

		if(Heat >= MaxHeat)
		{
			Overheated = true;
			SetGunsEnable(false);
			UpdateHeat = OverheatCool;
		}
		else if (Heat <= 0)
			Heat = 0;
	}

	private void OverheatCool()
	{
		//cooldown heat
		TimeBank += Time.deltaTime;
		int lost = Mathf.FloorToInt(TimeBank * FastCooldown);
		Heat -= (short)lost;
		TimeBank -= lost / FastCooldown;

		if(Heat <= PenaltyEnd)
		{
			Overheated = false;
			SetGunsEnable(true);
			UpdateHeat = NormalCool;
		}
	}

	//Random Functions

	public void SetGunsEnable(bool e)
	{
		for(int i = 0; i < Guns.Count; i++)
		{
			Guns[i].enabled = e;
		}
	}
}
