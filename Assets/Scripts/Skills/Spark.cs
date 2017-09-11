using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : Skill {

	public string damageType = "magical";
	public float damageMultiplier = 1.25f;
	public Spark () {
		this.name = "Spark";
		this.recastTime = 2;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		target.addEffect (new Shock (source.stats[source.mainStat]));
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
