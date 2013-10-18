using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {
	
	private enum CineList {
		Intro,
		Intermediate,
		Ending
	};
	private CineList cinem;
	private bool isPaused;
	private bool isPlayingCine;
	private int pausedID;
	private OTAnimation Anima;

	// Use this for initialization
	void Start () 
	{
		isPlayingCine = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPaused == false)
		{
			
		}
		if (Input.GetKey(KeyCode.Space))
		{
			resCine();
		}
	}
	
	void launchCinematic(int id, int pausedID)
	{
		if (id == 1)
		{
			Anima = new OTAnimation();
			
		}	
	}
	
	void resCine()
	{
		
	}
}
