using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapidfire : GunControl {

	void Update () {

		//Update Angle
		UpdateRotation();

		//Fire Gun
		CurrTime -= Time.deltaTime;

		if(CurrTime <= 0)
		{
			if(Input.GetAxisRaw("A" + PlayerNum) >= 1)
			{
				Fire();
				CurrTime += FIRERATE;
			}
			else
			{
				CurrTime = 0f;
			}
		}

	}
}
