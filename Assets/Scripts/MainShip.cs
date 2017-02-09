using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShip : Ship {

	public static GameObject Singleton;

	public short Heat = 0;
	public short MaxHeat = 100;
	public float Cooldown = 10f; //Heat per Second
	public float HeatDPS = 10f;
	private float TimeBank = 0f;

	public Image HealthBar;
	public Image HeatBar;



	void Start()
	{
		Singleton = gameObject;
	}

	void Update () {
		//Check for overheat
		if(Heat >= MaxHeat)
		{
			Destroy(gameObject);
		}
		
		//cooldown heat
		TimeBank += Time.deltaTime;
		int lost = Mathf.FloorToInt(TimeBank * Cooldown);
		Heat -= (short)lost;
		TimeBank -= lost / Cooldown;

		if (Heat <= 0)
			Heat = 0;

		//Display heat and health
		HealthBar.fillAmount = (float)Health / (float)MaxHealth;
		HeatBar.fillAmount = (float)Heat / (float)MaxHeat;
	}
}
