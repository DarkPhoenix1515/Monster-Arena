using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Skill {

	public Shield () {
		this.name = "Shield";
		this.recastTime = 5;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		Debug.Log (source.name + " used " + this.name + " on himself");
		source.shield = 100;
		this.cooldown = recastTime;
	}
}
