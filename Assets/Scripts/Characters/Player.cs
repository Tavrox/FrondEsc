using UnityEngine;
using System.Collections;

public class Player : Character {
	
	[HideInInspector] public Vector3 position;
	
	[HideInInspector] public Dialog dialog;
	//public Skill skillLaunch;
	
	public Skills skill_knife;
	public Skills skill_axe;
	public Skills skill_shield;
	
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		enabled = false;
		hasShield = false;
		spawnPos = thisTransform.position;
		//dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		print ("Player shield " + hasShield);
		
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isJump = false;
		isGoDown = false;
		isPass = false;
		//System.Console.WriteLine("test");
		movingDir = moving.None;
		
		// keyboard input
		if(Input.GetKey("left")) 
		{ 
			//Debug.Log("left");
			isLeft = true;
			shootLeft = true;
			facingDir = facing.Left;
		}
		if (Input.GetKey("right") && isLeft == false) 
		{ 
			isRight = true; 
			facingDir = facing.Right;
			shootLeft = false;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			isGoDown = true;
			facingDir = facing.Down;
		}
		if (Input.GetKeyDown("up")) 
		{ 
			isJump = true; 
		}
		
		if(Input.GetKeyDown("space"))
		{
			isPass = true;
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			isShot = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			skill_knife.useSkill(Skills.SkillList.Knife);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			skill_axe.useSkill(Skills.SkillList.Axe);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			skill_shield.useSkill(Skills.SkillList.Shield);
		}
		UpdateMovement();
	}
	
	private void GameStart () {
		if(FindObjectOfType(typeof(Player)) && this != null) {
			transform.localPosition = spawnPos;
			enabled = true;
		}
	}
	
	private void GameOver () {
		enabled = false;
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		movingDir = moving.None;
	}
}
