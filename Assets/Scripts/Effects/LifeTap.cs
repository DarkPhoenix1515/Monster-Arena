using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTap : Effect {

	public LifeTap() {
		this.name = "Life Tap";
		this.duration = 3;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		float healthRatio = (float)target.currentHealth / (float)target.maxHealth;
		int amount = (int)(target.maxHealth * 0.25);
		target.maxHealth -= amount;
		target.currentHealth = (int)((float)target.maxHealth * healthRatio);
		Debug.Log (this.name + " lowered " + target.name + "'s maximum health by " + amount + " for " + this.duration + " round(s).");
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.removeEffect (this);
	}
}
