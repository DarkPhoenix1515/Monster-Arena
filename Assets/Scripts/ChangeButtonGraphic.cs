using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonGraphic : MonoBehaviour {

	bool mouseOn = false;
	public Sprite graphic;
	public Sprite selectedGraphic;
	public GameObject character;
	Image buttonImg;

	void Start () {
		buttonImg = transform.GetComponent<Image>();
		buttonImg.sprite = graphic;
	}

	public void mouseIn(string stat) {
		buttonImg.sprite = selectedGraphic;
		character.SetActive(true);
	}

	public void mouseOut(string stat) {
		buttonImg.sprite = graphic;
		character.SetActive(false);
	}
}
