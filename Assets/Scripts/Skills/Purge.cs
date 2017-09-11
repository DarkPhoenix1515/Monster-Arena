using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purge : Skill {

	public Purge () {
		this.name = "Purge";
		this.recastTime = 5;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		Debug.Log (source.name + " used " + this.name + " on himself.");
		source.purge ();
		this.cooldown = recastTime;
	}
}
