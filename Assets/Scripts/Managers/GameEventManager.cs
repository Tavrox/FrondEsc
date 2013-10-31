using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GamePause, GameUnpause, GameOver, NextLevel, PreviousLevel;
	public static bool gamePaused = false;
	
	public static void TriggerGameStart(){
		if(GameStart != null){					
			GameStart();
		}
	}

	public static void TriggerGameOver(){
		if(GameOver != null){
			GameOver();
		}
	}
	
	public static void TriggerNextLevel(){
		if(NextLevel != null){
			NextLevel();
		}
	}
	public static void TriggerPreviousLevel(){
		if(PreviousLevel != null){
			PreviousLevel();
		}
	}
	public static void TriggerGamePause()
	{
		if(GamePause != null)
		{
			Debug.Log("Pause");
			gamePaused = true;
			GamePause();
		}
	}
	public static void TriggerGameUnpause()
	{
		Debug.Log("OMG");
		if(GameUnpause != null)
		{
			Debug.Log("Unpause");
			gamePaused = false;
			GameUnpause();
		}
	}
}
