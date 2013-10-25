using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, NextLevel, PreviousLevel;
	
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
}
