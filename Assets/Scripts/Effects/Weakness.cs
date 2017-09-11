using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : Effect {

	public Weakness() {
		this.name = "Weakness";
		this.duration = 3;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		int amount = (int)((float)target.Damage * 0.25f);
		Debug.Log (this.name + " lowers " + target.name + "'s damage by " + amount + " for " + this.duration + " more round(s).");
		target.Damage -= amount;
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.removeEffect (this);
	}
}