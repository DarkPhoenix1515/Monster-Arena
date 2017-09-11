using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blind : Effect {

	public Blind () {
		this.name = "Blind";
		this.duration = 3;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		Debug.Log (target.name + " is blinded for " + this.duration + " more round(s).");
		target.isBlind = true;
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.isBlind = false;
		target.removeEffect (this);
	}
}
