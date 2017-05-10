using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public Text VictoryText;
	public float Delay = 2f;

	void Update()
	{
		if (VictoryText.gameObject.activeSelf)
		{
			Delay -= Time.deltaTime;
			if(Delay <= 0)
			{
				SceneManager.LoadScene("Test");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//End the Level!
		VictoryText.gameObject.SetActive(true);
	}
}
