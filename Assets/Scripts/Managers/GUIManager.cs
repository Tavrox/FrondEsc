using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText gameOverText, instructionsText;
	public GUIText HPTxt;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
//		if(null) FindObjectOfType(typeof(GUIManager));
		if(Input.GetKeyDown("space")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		if(FindObjectOfType(typeof(GUIManager)) && this != null) {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		enabled = false;
		}
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		enabled = true;
	}
}
