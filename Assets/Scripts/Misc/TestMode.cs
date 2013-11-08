using UnityEngine;
using System.Collections;

public class TestMode : MonoBehaviour {
	
	public int[] wantedTweaks;
	private Player player;

	
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			Debug.Log("Player HP + 8");
			player.RegenHP(8);
		}
		if (Input.GetKeyDown(KeyCode.Keypad2))
		{
			Debug.Log("Player HP - 8");
			player.DiminishHP(8);
		}
		if (Input.GetKeyDown(KeyCode.Keypad3))
		{
			Debug.Log("Player HP" + player.HP);
		}
	
	}
	
	void OnGUI()
	{
//		moveVel = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), moveVel, 0f, 10f);
		
	}
}
