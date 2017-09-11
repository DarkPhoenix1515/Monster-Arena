using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : Skill {

	float damageMultiplier = 0.25f;
	string damageType = "physical";
	public Gas () {
		this.name = "Gas";
		this.recastTime = 4;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		target.addEffect (new Fatigue ());
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
