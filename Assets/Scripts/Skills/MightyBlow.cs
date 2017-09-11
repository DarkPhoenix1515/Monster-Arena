using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MightyBlow : Skill {

	float damageMultiplier = 1.25f;
	string damageType = "physical";
	public MightyBlow () {
		this.name = "Mighty Blow";
		this.recastTime = 2;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		if (Random.Range (0f, 99f) < 25) {
			target.addEffect (new Bleed (source.stats[source.mainStat]));
		}
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
