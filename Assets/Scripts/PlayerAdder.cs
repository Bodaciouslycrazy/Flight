using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdder : MonoBehaviour {

	protected Util.ButtonInfo[] AAxis = new Util.ButtonInfo[4];
	protected Util.AxisInfo[] XAxis = new Util.AxisInfo[4];

	// Use this for initialization
	void Start () {
		for(int i = 0; i < 4; i++)
		{
			AAxis[i] = Util.NewButton("A" + (i + 1));
			XAxis[i] = Util.NewAxis("X" + (i + 1));
		}
	}
	
	// Update is called once per frame
	void Update () {


		for(int i = 0; i < 4; i++)
		{
			Util.UpdateButton(ref AAxis[i]);

			if (MainShip.GetGunNum(i) <0 && AAxis[i].Pressed)
			{
				GetComponent<AudioSource>().Play();
				MainShip.ChangeGun(i, i);
			}
		}
	}
}
