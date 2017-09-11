using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]

public class BaseCharacter {
	public string name;
	public string mainStat;
	public Skill trait;
	Dictionary<string, float> baseStats = new Dictionary<string, float>();
	public Dictionary<string, float> stats = new Dictionary<string, float>();
	Dictionary<string, float> baseArmor = new Dictionary<string, float>();
	public Dictionary<string, float> armor = new Dictionary<string, float>();
	public Dictionary<string, float> armorMultiplier = new Dictionary<string, float>();
	public float damageMultiplier = 1f;
	List<Effect> effects = new List<Effect> ();
	public List<Skill> skills = new List<Skill> ();

	// balance things
	float hpRate = 4f;
	float armorRate = 1/7f;

	public int baseHealth = 100;
	public int maxHealth = 100;
	public int currentHealth = 100;
	int baseDamage = 0;
	public int shield = 0;

	// attack/action modifiers
	public bool isStunned = false;
	public bool isBlind = false;

	// calculate
	public int Damage = 0;

	public BaseCharacter(int baseHealth, float baseStr, float baseAgi, float baseInt, int baseDamage, float baseArmor, float baseMagicArmor, string mainStat, string name) {
		this.baseHealth = baseHealth;
		this.maxHealth = baseHealth;
		this.currentHealth = baseHealth;
		this.baseStats.Add("Str", baseStr);
		this.baseStats.Add("Agi", baseAgi);
		this.baseStats.Add("Int", baseInt);
		this.baseArmor.Add ("physical", baseArmor);
		this.baseArmor.Add ("magical", baseMagicArmor);
		this.baseDamage = baseDamage;
		this.mainStat = mainStat;
		this.name = name;
		calculateStats ();
		initializeSkills ();
	}

	void calculateStats () {
		this.stats["Str"] = this.baseStats["Str"];
		this.stats["Agi"] = this.baseStats["Agi"];
		this.stats["Int"] = this.baseStats["Int"];
		float healthRatio = (float)currentHealth/(float)maxHealth;
		this.maxHealth = (int)(baseHealth + stats["Str"] * hpRate);
		currentHealth = (int)(healthRatio * maxHealth);
		this.Damage = (int)(((float)baseDamage + stats[mainStat]) * damageMultiplier);
		this.armor["physical"] = baseArmor["physical"] + (stats["Agi"] * armorRate);
		this.armor["magical"] = baseArmor["magical"] + (stats["Int"] * armorRate);
		this.armorMultiplier["physical"] = 1f - 0.06f * armor["physical"] / (1f + 0.06f * Mathf.Abs(armor["physical"]));
		this.armorMultiplier["magical"] = 1f - 0.06f * armor["magical"] / (1f + 0.06f * Mathf.Abs(armor["magical"]));
		foreach (Effect e in this.effects.ToList()) {
			e.isApplied = false;
		}
	}

	void initializeSkills () {
		switch (mainStat) {
			case "Str":
				skills.Add (new Crush ());
				skills.Add (new MightyBlow());
				skills.Add (new Bludgeon());
				skills.Add (new OverwhelmingBlow());
				skills.Add (new Armor());
				break;
			case "Agi":
				skills.Add (new Thrust());
				skills.Add (new PoisonDagger());
				skills.Add (new Gas());
				skills.Add (new Backstab());
				skills.Add (new Purge());
				break;
			case "Int":
				skills.Add (new ArcaneFlash());
				skills.Add (new Spark());
				skills.Add (new ArcaneCurse());
				skills.Add (new Prodigy());
				skills.Add (new Shield());
				break;
		}
	}

	public void updateCharacterState () {
		calculateStats ();
		updateEffectsAndSkills ();
	}

	public void takeDamage(int amount, string type) {
		if (shield > 0) {
			int reminder = shield - amount;

			if (reminder <= 0) {
				amount -= shield;
				Debug.Log ("Shield absorbed " + shield + " " + type + " damage.");
				shield = 0;
			} else {
				shield = reminder;
				Debug.Log ("Shield absorbed " + amount + " " + type + " damage.");
				return;
			}
		}
		int damageTaken = (int)(amount * armorMultiplier[type]);
		Debug.Log (this.name + " takes " + damageTaken + " " + type + " damage.");
		currentHealth -= damageTaken;

		if (currentHealth <= 0) {
			die ();
		}
	}

	public bool onCooldown(int skill) {
		if (this.skills [skill].cooldown == 0) {
			return false;
		} else {
			return true;
		}
	}

	public void castSkill (int skill, BaseCharacter target) {
		if (canAct ()) {
			if (this.isBlind) {
				if (Random.Range (0f, 99f) < 50) {
					this.skills [skill].cast (this, target);
				} else {
					Debug.Log (this.name + "'s blindness caused them to fail " + skills [skill].name + ".");
				}
			} else {
				skills [skill].cast (this, target);
			}
		} else {
			Debug.Log (this.name + " is stunned and cannot act.");
		}
	}

	public void addEffect (Effect effect) {
		foreach (Effect e in this.effects.ToList()) {
			if (e.name == effect.name) {
				e.duration = effect.duration;
				return;
			}
		}
		this.effects.Add (effect);
	}

	public void removeEffect (Effect effect) {
		this.effects.Remove (effect);
	}

	public void purge () {
		foreach (Effect e in this.effects.ToList()) {
			if (e.isNegative) {
				removeEffect (e);
			}
		}
		Debug.Log ("All " + this.name + "'s negative effects are removed.");
	}

	public void updateEffectsAndSkills () {
		foreach (Effect e in this.effects.ToList()) {
			if (!e.isApplied) {
				e.updateEffect (this);
				e.isApplied = true;
			}
		}

		foreach (Skill s in this.skills.ToList()) {
			if (s.cooldown > 0) {
				s.cooldown -= 1;
			}
		}
	}

	public void applyNewEffects () {
		foreach (Effect e in this.effects.ToList()) {
			if (!e.isApplied) {
				e.updateEffect (this);
				e.isApplied = true;
			}
		}
	}

	public string getSkillName (int skill) {
		return skills [skill].name;
	}

	public bool canAct () {
		return !this.isStunned;
	}

	void die () {
		GameObject.Find("GameController").GetComponent<GameController>().gameOver();
		Debug.Log (name + " is dead");
	}
}
