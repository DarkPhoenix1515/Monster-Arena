using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Text name;

	public void mouseClick(string stat) {
		PlayerPrefs.SetString ("mainStat", stat);
		PlayerPrefs.SetString ("name", name.text);
		Invoke ("startGame", 2.0f);
	}

	public void quitButton () {
		Application.Quit ();
	}

	void startGame() {
		SceneManager.LoadScene (1);
	}
}
