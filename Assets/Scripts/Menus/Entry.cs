using UnityEngine;
using System.Collections;

public class Entry : MonoBehaviour {
	
	
	public GUI launchCampaign;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnGUI()
	{
		if (GUI.Button( new Rect( 100, 100, 200, 100), "Hola!"))
		{
			print ("Button Clicked");
		}	
	}
}
