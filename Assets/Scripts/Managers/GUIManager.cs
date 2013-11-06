using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	
	public GUIText gameOverText, instructionsText;
	public GUIText HPTxt;
	
	private Player _player;
	
	// Use this for initialization
	void Start () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GamePause += GamePause;
		GameEventManager.GameUnpause += GameUnpause;
		
		gameOverText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
//		if(null) FindObjectOfType(typeof(GUIManager));
		if(Input.GetKeyDown(KeyCode.Space)){
			GameEventManager.TriggerGameStart();
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			GameEventManager.TriggerGameUnpause();
		}
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//		HPTxt.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y+2,_player.transform.position.z);
//		HPTxt.transform.localPosition = new Vector3(_player.transform.position.x, _player.transform.position.y+2,_player.transform.position.z);

	}
	
	private void GameStart () {
		if(FindObjectOfType(typeof(GUIManager)) && this != null) 
		{
			gameOverText.enabled = false;
			instructionsText.enabled = false;
		}
	}
	
	private void GameOver () 
	{
		gameOverText.enabled = true;
	}
	
	private void GamePause()
	{
		
	}
	
	private void GameUnpause()
	{
		
	}
}
