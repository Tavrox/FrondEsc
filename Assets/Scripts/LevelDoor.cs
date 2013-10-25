using UnityEngine;
using System.Collections;

public class LevelDoor : MonoBehaviour {
	
	public enum doorType { BeginLevel, EndLevel }
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
		if(other.gameObject.CompareTag("Player")) {
			if(myDoorType.ToString()=="BeginLevectorMove") GameEventManager.TriggerPreviousLevel();
			else GameEventManager.TriggerNextLevel();
		}
    }
	
	private void NextLevel () {print(Application.loadedLevel);
		Application.LoadLevel(Application.loadedLevel+1);
	}
	
	private void PreviousLevel () {
		Application.LoadLevel(Application.loadedLevel-1);
	}
}
