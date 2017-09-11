using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDagger : Skill {

	string damageType = "physical";
	float damageMultiplier = 1.25f;
	public PoisonDagger () {
		this.name = "Poison Dagger";
		this.recastTime = 2;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		target.addEffect (new Poison (source.stats[source.mainStat]));
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
