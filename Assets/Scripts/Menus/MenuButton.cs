using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
	
	public OTTextSprite label;
	public OTAnimatingSprite sprite;
	public enum ListAction
	{
		LaunchScene,
		MuteSound,
		LowerSound,
		RaiseSound,
		FunStuff, // To do funny miscellaneous stuff in menus :)
	}
	public ListAction action;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnMouseOver()
	{
		Debug.Log("Hover");	
		if(Input.GetMouseButtonDown(0))
		{
			Application.LoadLevel(Entry.lvlName1);
		}
	}
}
