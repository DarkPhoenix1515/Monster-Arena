using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crush : Skill {

	float damageMultiplier = 1f;
	string damageType = "physical";
	public Crush () {
		this.name = "Crush";
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		string stun = "";
		if (Random.Range (0f, 99f) < 25) {
			target.addEffect (new Stun ());
			stun = " and stunned them.";
		} else {
			stun = ".";
		}
		Debug.Log (source.name + " used " + this.name + " on " + target.name + stun);
		int damage = (int)(source.Damage * damageMultiplier);
		target.takeDamage (damage, damageType);
	}
}
