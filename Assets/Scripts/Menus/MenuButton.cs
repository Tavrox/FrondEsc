using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public GUI ui;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		Input.GetMouseButtonDown(1);
	}
	
	void onGUI()
	{
		
	}

}
