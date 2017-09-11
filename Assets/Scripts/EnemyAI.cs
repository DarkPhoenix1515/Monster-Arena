using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI {

	public EnemyAI () {

	}

	public int move () {
		int skill = (int)Mathf.Round (Random.Range (0.51f, 5.49f)) - 1;
		return skill;
	}
}
