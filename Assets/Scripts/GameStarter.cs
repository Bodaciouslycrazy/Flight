using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public float COUNTDOWN = 3f;
    private float CurTime = 3f;

    public MenuScript[] Players;
    public static bool[] Playing = new bool[4];
    public static int[] PWeapons = new int[4];

	// Update is called once per frame
	void Update () {

        bool OneReady = false;
        bool StillSelecting = false;

        for(int i = 0; i < Players.Length; i++)
        {
            if (Players[i].CurState == MenuScript.State.Ready)
                OneReady = true;
            else if (Players[i].CurState == MenuScript.State.Weapon)
                StillSelecting = true;
        }

        if(OneReady && !StillSelecting)
        {
            CurTime -= Time.deltaTime;
            if(CurTime <= 0)
            {
                for(int i = 0; i < Players.Length; i++)
                {
                    if(Players[i].CurState == MenuScript.State.Ready)
                    {
                        Playing[i] = true;
                        PWeapons[i] = Players[i].CurWeapon;
                    }
                    else
                    {
                        Playing[i] = false;
                        PWeapons[i] = -1;
                    }
                }


                Debug.Log("LOAD LEVEL");
            }
        }
        else
        {
            CurTime = COUNTDOWN;
        }

	}
}
