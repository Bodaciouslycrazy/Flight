using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour, IButtonReciever {

	public Vector2 ReleasePos;
	public Vector2 PressPos;
	public float MoveSpeed = 1f;

	private Vector2 Target;

	void Start()
	{
		Target = ReleasePos;
	}

	void Update()
	{
		if(Vector2.Distance(transform.position, Target) > MoveSpeed * Time.deltaTime)
		{
			Vector2 mov = Target - (Vector2)transform.position;
			mov.Normalize();
			mov *= MoveSpeed * Time.deltaTime;

			transform.position = (Vector2)transform.position + mov;
		}
		else
		{
			transform.position = (Vector3)Target;
		}
	}

	public void OnPress()
	{
		Target = PressPos;
	}

	public void OnUnpress()
	{
		Target = ReleasePos;
	}
}
