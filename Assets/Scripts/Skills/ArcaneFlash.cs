using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneFlash : Skill {

	float damageMultiplier = 1f;
	string damageType = "magical";
	public ArcaneFlash () {
		this.name = "Arcane Flash";
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		if (Random.Range (0f, 99f) < 25) {
			target.addEffect (new Blind ());
		}
		Debug.Log (source.name + " used " + this.name + " on " + target.name);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
	}
}
