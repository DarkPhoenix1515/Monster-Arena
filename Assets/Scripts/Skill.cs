using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill {
	public string name = "";
	public bool hasModifier = false;
	public int recastTime = 0;
	public int cooldown = 0;
	public abstract void cast (BaseCharacter source, BaseCharacter target);
}