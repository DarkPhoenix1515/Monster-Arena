using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneCurse : Skill {

	float damageMultiplier = 0.25f;
	string damageType = "magical";
	public ArcaneCurse () {
		this.name = "Arcane Curse";
		this.recastTime = 4;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.addEffect (new LifeTap ());
		target.takeDamage (damage, damageType);
		this.cooldown = recastTime;
	}
}
