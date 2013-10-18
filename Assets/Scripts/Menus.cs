using UnityEngine;
using System.Collections;

public class Menus : MonoBehaviour {
	
	public enum MenuType
	{
		EnterGame,
		Pause,
	}
	public MenuType Menu;
	private GameLoop gl;
	
	// Use this for initialization
	void Start () 
	{
		gl = GetComponent<GameLoop>();
		if (Menu == MenuType.EnterGame)
		{
			
		}
		else if (Menu == MenuType.Pause)
		{
			
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
