using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour {

	public bool Pressed = false;
	public float TIMER = 10f;
	public float CurTime = 0f;

	public GameObject[] Recievers;

	private delegate void WBUpdate();
	private WBUpdate SelectedUpdate;

	// Use this for initialization
	void Start () {
		SelectedUpdate = UnpressedUpdate;
	}
	
	// Update is called once per frame
	void Update () {
		SelectedUpdate();
	}

	private void Press()
	{
		Pressed = true;
		SelectedUpdate = PressedUpdate;
		CurTime = TIMER;

		for(int i = 0; i < Recievers.Length; i++)
		{
			Recievers[i].GetComponent<WorldButtonReciever>().OnPress();
		}
	}

	private void Unpress()
	{
		Pressed = false;
		SelectedUpdate = UnpressedUpdate;

		for (int i = 0; i < Recievers.Length; i++)
		{
			Recievers[i].GetComponent<WorldButtonReciever>().OnUnpress();
		}
	}

	//Delegate functions
	void PressedUpdate()
	{
		CurTime -= Time.deltaTime;

		if(CurTime <= 0)
		{
			Unpress();
		}
	}

	void UnpressedUpdate()
	{
		//do nothing
	}


	//Checking for press
	void OnCollisionEnter2D(Collision2D col)
	{
		if (!Pressed && col.gameObject.tag.Equals("Player"))
		{
			Press();
		}
	}
}
