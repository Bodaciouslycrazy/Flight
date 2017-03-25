using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

	public static float Deadzone = .8f;
	public static Color Selected = new Color(0, 255, 0, .5f);
	public static Color Normal = new Color(255, 255, 255, .5f);

	public GameObject PressA;
	public GameObject WeaponSelect;
	public GameObject Ready;

	public Image[] WeaponIcons;
	public static bool[] WeaponTaken = { false, false, false, false };

	public int PNum = 1;
	public State CurState = State.Join;
	public int CurWeapon = 0;
	PrevInput LastIP = new PrevInput();
	

	void Update () {
		
		bool A = Input.GetAxis("A" + PNum) == 1f ? true : false;
		float X = Input.GetAxis("X" + PNum);
		float Y = Input.GetAxis("Y" + PNum);

		if(CurState == State.Join)
		{
			if(A && !LastIP.A)
			{

				CurState = State.Weapon;
				PressA.SetActive(false);
				WeaponSelect.SetActive(true);
				SetSelection(0);
			}
		}
		else if(CurState == State.Weapon)
		{
			if (Y > Deadzone && LastIP.Y < Deadzone)
			{
				//Move Up
				MoveSelection(-1);
			}
			else if (Y < -Deadzone && LastIP.Y > -Deadzone)
			{
				//Move Down
				MoveSelection(1);
			}
			else if(A && !LastIP.A)
			{
				//SelectWeapon
				if (!WeaponTaken[CurWeapon])
				{
					
					WeaponTaken[CurWeapon] = true;
					WeaponSelect.SetActive(false);
					Ready.SetActive(true);
					CurState = State.Ready;
				}
				else
				{
					Debug.Log("That weapon is taken.");
				}
			}
		}
		else if(CurState == State.Ready)
		{

		}

		//Update previous inputs
		LastIP.A = A;
		LastIP.X = X;
		LastIP.Y = Y;

	}

	public enum State
	{
		Join,
		Weapon,
		Ready
	}

	struct PrevInput
	{
		public bool A;
		public float X;
		public float Y;
	}

	private void MoveSelection(int num)
	{
		SetSelection(num + CurWeapon);
	}

	private void SetSelection(int num)
	{
		WeaponIcons[CurWeapon].color = Normal;

		CurWeapon = num;

		if (CurWeapon < 0)
			CurWeapon = 0;
		else if (CurWeapon > WeaponIcons.Length - 1)
			CurWeapon = WeaponIcons.Length - 1;

		WeaponIcons[CurWeapon].color = Selected;
	}

}
