using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Skill {

	public Armor () {
		this.name = "Armor";
		this.recastTime = 5;
	}

	public override void cast (BaseCharacter source, BaseCharacter target) {
		Debug.Log (source.name + " used " + this.name + " on himself.");
		source.addEffect (new ArmorEffect(source.stats[source.mainStat]));
		this.cooldown = recastTime;
	}
}
