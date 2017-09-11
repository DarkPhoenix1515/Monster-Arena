using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrust : Skill {

	float damageMultiplier = 1f;
	float critMultiplier = 2f;
	string damageType = "physical";
	public Thrust () {
		this.name = "Thrust";
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		string crit = "";
		if (Random.Range (0f, 99f) < 25) {
			critMultiplier = 2f;
			crit = " and inflicted a critical hit on ";
		} else {
			critMultiplier = 1f;
			crit = " on ";
		}
		Debug.Log (source.name + " used " + this.name + crit + target.name);
		int damage = (int)(source.Damage * damageMultiplier * critMultiplier);
		target.takeDamage (damage, damageType);
	}
}
