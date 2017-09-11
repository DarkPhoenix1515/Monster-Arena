using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

	public GameObject playerCanvas;
	public GameObject enemyCanvas;

	void Start () {
		float cHeight = Screen.height * 0.6f;
		float cWidth = Screen.width * 0.2f;
		playerCanvas.GetComponent<RectTransform> ().sizeDelta = new Vector2(cWidth, cHeight);
		enemyCanvas.GetComponent<RectTransform> ().sizeDelta = new Vector2(cWidth, cHeight);
	}
	
	void Update () {
		
	}
}
