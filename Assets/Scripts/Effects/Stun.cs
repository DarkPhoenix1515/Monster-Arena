using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : Effect {

	public Stun () {
		this.name = "Stun";
		this.duration = 1;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		Debug.Log (target.name + " is stunned.");
		target.isStunned = true;
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.isStunned = false;
		target.removeEffect (this);
	}
}
