using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fatigue : Effect {

	public Fatigue() {
		this.name = "Fatigue";
		this.duration = 3;
		this.isNegative = true;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		int amount = (int)(target.stats [target.mainStat] * 0.25f);
		Debug.Log (this.name + " lowers " + target.name + "'s " + target.mainStat + " by " + amount + " for " + this.duration + " more round(s).");
		target.stats [target.mainStat] -= (float)amount;
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.removeEffect (this);
	}
}
