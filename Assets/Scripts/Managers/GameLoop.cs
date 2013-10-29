using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {
	
	public bool gameOver = false;
	static bool isPaused = false;

	// Use this for initialization
	void Start () 
	{
		if (isPaused == false)
		{
			print ("unpaused");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
