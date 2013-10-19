using UnityEngine;
using System.Collections;

public class Zombies : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void onTriggerEnter(Collider player)
	{
		if (player.gameObject.CompareTag("Player"))
		{
			print ("DIES BItch");		
		}
	}
}
