using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armored : Enemy {

	public override void Hit(int Dam)
	{
		Dam = Mathf.Max(Dam - 5, 0);
		base.Hit(Dam);
	}
}
