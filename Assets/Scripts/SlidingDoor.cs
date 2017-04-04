using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour, IButtonReciever {

    public bool Open = false;
    public float OpenDistance = 5f;
	public float MoveSpeed = 1f;

    private float CurDistance = 0f;
    private Vector3 Base;

	void Start()
	{
        Base = transform.position;
	}

	void Update()
	{
		if(Open)
		{
            CurDistance += MoveSpeed * Time.deltaTime;
            if (CurDistance > OpenDistance)
                CurDistance = OpenDistance;

		}
		else
		{
            CurDistance -= MoveSpeed * Time.deltaTime;
            if (CurDistance < 0)
                CurDistance = 0;

        }

        transform.position = Base + (transform.right * CurDistance);
    }

	public void OnPress()
	{
        Open = true;
	}

	public void OnUnpress()
	{
        Open = false;
	}
}
