using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
	public string name = "";
	public int duration = 0;
	public bool isNegative = false;
	public bool isApplied = false;
//	bool activeEffectApplied = false;

	public void updateEffect (BaseCharacter target) {
		if (duration == 0) {
			removeEffect (target);
		} else {
			applyPassiveEffect (target);
			applyActiveEffect (target);
			duration -= 1;
		}
	}
	protected abstract void applyPassiveEffect (BaseCharacter target);
	protected abstract void applyActiveEffect (BaseCharacter target);
	protected abstract void removeEffect (BaseCharacter target);
}