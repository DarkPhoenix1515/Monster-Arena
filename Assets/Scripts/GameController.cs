using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject playerPanel;
	public GameObject enemyPanel;
	public GameObject[] playerModel;
	public GameObject[] enemyModel;
	public BaseCharacter player;
	public BaseCharacter enemy;
	public GameObject playerStunned;
	public GameObject enemyStunned;
	EnemyAI ai;
	bool playerTurn;
	Button[] playerButtons;
	public Text endText;

	void Start () {
		playerTurn = true;
		string mainStat = PlayerPrefs.GetString ("mainStat");
		string name = PlayerPrefs.GetString ("name");

		if (name == "")
			name = "Player";
		
		player = createPlayer(mainStat, name);
		enemy = createOpponent ();
		ai = new EnemyAI ();
		initializeUI ();
		updateUI ();
	}

	BaseCharacter createPlayer (string mainStat, string name) {
		switch (mainStat) {
		case "Str":
			playerModel [0].SetActive (true);
			return new BaseCharacter (300, 50, 20, 15, 10, 2, 1, mainStat, name);
			break;
		case "Agi":
			playerModel [1].SetActive (true);
			return new BaseCharacter (300, 20, 25, 50, 10, 5, 0, mainStat, name);
			break;
		case "Int":
			playerModel [2].SetActive (true);
			return new BaseCharacter (300, 20, 25, 50, 10, 0, 5, mainStat, name);
			break;
		}
		return new BaseCharacter (300, 20, 25, 50, 10, 0, 5, "Int", name);
	}

	BaseCharacter createOpponent () {
		float rnd = Random.Range (0f, 99f);

		if (rnd > 66) {
			enemyModel [2].SetActive (true);
			return new BaseCharacter (300, 20, 25, 50, 10, 0, 5, "Int", "Kambit");
		} else {
			if (rnd > 33) {
				enemyModel [1].SetActive (true);
				return new BaseCharacter (300, 20, 25, 50, 10, 5, 0, "Agi", "Samurai");
			} else {
				enemyModel [0].SetActive (true);
				return new BaseCharacter (300, 50, 20, 15, 10, 2, 1, "Str", "Barbarian");
			}
		} 
	}

	void Update () {

	}

	void toggleUIButtons (bool isActive) {
		playerButtons = playerPanel.GetComponentsInChildren<Button> ();

		for (int i = 0; i < playerButtons.Length; i++) {
			playerButtons [i].interactable = isActive;
		}
	}

	public void triggerSkill (int skill) {
		if (playerTurn) {
			if (!player.onCooldown(skill)) {
				player.updateCharacterState ();
				player.castSkill (skill, enemy);
				player.applyNewEffects ();
			} else {
				return;
			}
			toggleUIButtons (false);
		} else {
			if (!enemy.onCooldown(skill)) {
				enemy.updateCharacterState ();
				enemy.castSkill (skill, player);
				enemy.applyNewEffects ();
			} else {
				aiMove();
				return;
			}
		}
		changeTurn ();
	}

	void changeTurn () {
		playerTurn = !playerTurn;

		if (playerTurn) {
			if (!player.canAct ()) {
				playerTurn = false;
			}
		} else {
			if (!enemy.canAct ()) {
				playerTurn = true;
			}
		}
		updateUI ();

		if (playerTurn) {
			toggleUIButtons (true);
		} else {
			if (enemy.canAct ()) {
				aiMove ();
			}
		}
	}

	void aiMove () {
		triggerSkill (ai.move());
	}

	void initializeUI () {
		float cHeight = Screen.height * 0.45f;
		float cWidth = Screen.width * 0.2f;
		playerPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2(cWidth, cHeight);
		enemyPanel.GetComponent<RectTransform> ().sizeDelta = new Vector2(cWidth, Screen.height * 0.21f);
	}

	public void gameOver () {
		if (player.currentHealth > 0) {
			endText.text = "You Won!";
			endText.color = new Color32 (0, 255, 0, 255);
		} else {
			endText.text = "You Lost!";
			endText.color = new Color32 (255, 0, 0, 255);
		}
		endText.gameObject.SetActive(true);
		Invoke ("goToMenu", 2);
	}

	void goToMenu () {
		SceneManager.LoadScene (0);
	}

	void updateUI () {
		playerButtons = playerPanel.GetComponentsInChildren<Button> ();

		if (player.isStunned)
			playerStunned.SetActive (true);
		else
			playerStunned.SetActive(false);

		if (enemy.isStunned)
			enemyStunned.SetActive (true);
		else
			enemyStunned.SetActive(false);

		for (int i = 0; i < playerButtons.Length; i++) {
			Text t = playerButtons [i].GetComponentInChildren<Text> ();
			t.text = player.getSkillName (i);

			if (player.skills [i].cooldown > 0) {
				t.text += " " + player.skills [i].cooldown;
				playerButtons [i].GetComponent<Image> ().color = new Color32 (255, 0, 0, 255);
			} else {
				playerButtons [i].GetComponent<Image> ().color = new Color32 (255, 255, 255, 255);
			}
		}

		GameObject sp = playerPanel.transform.Find ("StatsPanel").gameObject;

		foreach (Transform child in sp.transform) {
			if (child.name == "Text_Name") {
				child.GetComponent<Text> ().text = player.name;
			}

			if (child.name == "Text_Health") {
				child.GetComponent<Text> ().text = "Health: " + player.currentHealth + "/" + player.maxHealth;
			}

			if (child.name == "Text_Armor") {
				child.GetComponent<Text> ().text = "Armor: " + (int)player.armor["physical"] + "p/" + (int)player.armor["magical"] + "m";
			}

			if (child.name == "Text_Damage") {
				child.GetComponent<Text> ().text = "Damage: " + player.Damage;
			}

			if (child.name == "Text_Stats") {
				child.GetComponent<Text> ().text = "Str: " + player.stats["Str"] + "/Agi: " + player.stats["Agi"] + "/Int: " + player.stats["Int"];
			}
		}

		GameObject ep = enemyPanel.transform.Find ("StatsPanel").gameObject;

		foreach (Transform child in ep.transform) {
			if (child.name == "Text_Name") {
				child.GetComponent<Text> ().text = enemy.name;
			}

			if (child.name == "Text_Health") {
				child.GetComponent<Text> ().text = "Health: " + enemy.currentHealth + "/" + enemy.maxHealth;
			}

			if (child.name == "Text_Armor") {
				child.GetComponent<Text> ().text = "Armor: " + (int)enemy.armor["physical"] + "p/" + (int)enemy.armor["magical"] + "m";
			}

			if (child.name == "Text_Damage") {
				child.GetComponent<Text> ().text = "Damage: " + enemy.Damage;
			}

			if (child.name == "Text_Stats") {
				child.GetComponent<Text> ().text = "Str: " + enemy.stats["Str"] + "/Agi: " + enemy.stats["Agi"] + "/Int: " + enemy.stats["Int"];
			}
		}
	}

	public void menuButton () {
		SceneManager.LoadScene (0);
	}

	/*
	 * Skills by Main stat:
	 * Str: Crush, MightyBlow, Bludgeon, OverwhelmingBlow, Armor
	 * 
	 * Agi: Thrust, PoisonDagger, Gas, Backstab, Purge
	 * 
	 * Int: ArcaneFlash, Spark, ArcaneCurse, Prodigy, Shield
	 * 
	 * buttons: default: white, onHoover: lt green, pushed: green, on cd: red
	 * TODO: add a buff bar
	 */
}
