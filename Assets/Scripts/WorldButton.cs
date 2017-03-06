using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour {

	public bool Pressed = false;
	public float TIMER = 10f;
	private float CurTime = 0f;

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

		Transform Child = transform.GetChild(0);
		Vector2 pos = Child.position;
		pos += (Vector2)Child.up * -0.09f * transform.localScale.y;
		Child.position = pos;

		for(int i = 0; i < Recievers.Length; i++)
		{
			Recievers[i].GetComponent<IButtonReciever>().OnPress();
		}
	}

	private void Unpress()
	{
		Pressed = false;
		SelectedUpdate = UnpressedUpdate;

		Transform Child = transform.GetChild(0);
		Vector2 pos = Child.position;
		pos += (Vector2)Child.up * 0.09f * transform.localScale.y;
		Child.position = pos;

		for (int i = 0; i < Recievers.Length; i++)
		{
			Recievers[i].GetComponent<IButtonReciever>().OnUnpress();
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
