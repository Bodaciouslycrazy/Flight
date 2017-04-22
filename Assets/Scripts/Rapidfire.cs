using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapidfire : GunControl {

    private bool Firing = false;

	void Update () {

		//Update Angle
		UpdateRotation();
        Util.UpdateButton(ref Button);

        if (Button.Pressed)
            Firing = true;
        if (Button.Released)
            Firing = false;
		

		CurrTime -= Time.deltaTime;

        //Fire gun
		if(CurrTime <= 0)
		{

            if (Firing)
			{
                if (transform.parent.GetComponent<MainShip>().GetEnergy(EnergyPerShot))
                {
                    Fire();
                    AudioSource.PlayClipAtPoint( FireSound , transform.position);
				    CurrTime += FIRERATE;
                }
                else
                {
                    Firing = false; //force release
                }
                
			}
			else
			{
				CurrTime = 0f;
			}
		}

        SetFill();

	}
}
