using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock : Effect {

	public int damage = 0;
	public string damageType = "magical";
	public Shock (float mainStat) {
		this.name = "Shock";
		this.damage = (int)(mainStat / 4);
		this.duration = 3;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {}

	protected override void applyActiveEffect (BaseCharacter target) {
		Debug.Log (this.name + " hurts " + target.name + " for " + this.duration + " more round(s).");
		target.takeDamage (this.damage, this.damageType);
	}

	protected override void removeEffect (BaseCharacter target) {
		target.removeEffect (this);
	}
}
