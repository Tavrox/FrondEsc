using UnityEngine;
using System.Collections;

public class LevelDoor : MonoBehaviour {
	
	public enum doorType { BeginLevel, EndLevel }
	public LevelManager lvlManager;
	public doorType myDoorType;
	
	// Use this for initialization
	void Start () {
		GameEventManager.NextLevel += NextLevel;
		GameEventManager.PreviousLevel += PreviousLevel;
	}
//	void Update () {
//		if(null) FindObjectOfType(typeof(LevectorMoveDoor));	
//	}
	void OnTriggerEnter(Collider other)
    {
		if(other.gameObject.CompareTag("Player")) 
		{	
			if(myDoorType.ToString()=="BeginLevel") GameEventManager.TriggerPreviousLevel();
			else GameEventManager.TriggerNextLevel();
		}
    }
	
	private void NextLevel ()
	{
		Application.LoadLevel(lvlManager.nextLvlID);
	}
	
	private void PreviousLevel () {
		Application.LoadLevel(lvlManager.previousLvlID);
	}
	
}
