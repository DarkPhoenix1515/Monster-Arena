using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backstab : Skill {

	public string damageType = "physical";
	public float damageMultiplier = 2.5f;
	public Backstab () {
		this.name = "Backstab";
		this.recastTime = 4;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
