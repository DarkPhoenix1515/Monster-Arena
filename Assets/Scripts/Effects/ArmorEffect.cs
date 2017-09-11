using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorEffect : Effect {

	int bonusArmor = 0;
	public ArmorEffect (float mainStat) {
		this.name = "Armor";
		this.bonusArmor = (int)(mainStat/5);
		this.duration = 3;
	}

	protected override void applyPassiveEffect (BaseCharacter target) {
		target.armor["physical"] += bonusArmor;
		target.armor["magical"] += bonusArmor;
		Debug.Log (target.name + "'s armor is increased by " + bonusArmor + " for " + this.duration + " round(s).");
	}

	protected override void applyActiveEffect (BaseCharacter target) {}

	protected override void removeEffect (BaseCharacter target) {
		target.removeEffect (this);
	}
}
