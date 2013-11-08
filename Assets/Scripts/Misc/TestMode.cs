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
		
		if (Input.GetKey(KeyCode.Keypad1))
		{
			Debug.Log("Player HP + 10");
			player.RegenHP(10);
		}
		if (Input.GetKey(KeyCode.Keypad2))
		{
			Debug.Log("Player HP - 20");
			player.DiminishHP(20);
		}
		if (Input.GetKey(KeyCode.Keypad3))
		{
			Debug.Log("Player HP" + player.HP);
		}
	
	}
	
	void OnGUI()
	{
//		moveVel = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), moveVel, 0f, 10f);
		
	}
}
