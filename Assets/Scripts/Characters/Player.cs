using UnityEngine;
using System.Collections;

public class Player : Character {
	
	[HideInInspector] public Vector3 position;
	[HideInInspector] public Transform trans;
	
	[HideInInspector] public Dialog dialog;
	//public Skill skillLaunch;
	
	public SkillManager skillManager;
	public Skills skill_knife;
	public Skills skill_axe;
	public Skills skill_shield;
	public OTSprite menu;
	
	public GUIText hp_display;
	
	public bool shootingKnife;
	[HideInInspector] public bool paused = false;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GamePause += GamePause;
		GameEventManager.GameUnpause += GameUnpause;
		
		enabled = false;
		hasShield = false;
		spawnPos = thisTransform.position;
		//dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isJump = false;
		isGoDown = false;
		isPass = false;
		
		shootingKnife = false;
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
			shootingKnife = true;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			skill_axe.useSkill(Skills.SkillList.Axe);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			skill_shield.useSkill(Skills.SkillList.Shield);
			hasShield = true;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameEventManager.gamePaused == false)
			{
				GameEventManager.TriggerGamePause();
			}
			else if (GameEventManager.gamePaused == true)
			{
				GameEventManager.TriggerGameUnpause();
			}
		}
		
		print ("Check Shield" + hasShield);
		
		UpdateMovement();
	}
	
	private void GameStart () {
		if(FindObjectOfType(typeof(Player)) && this != null) {
			transform.localPosition = spawnPos;
			enabled = true;
		}
	}
	
	private void GameOver () 
	{
		enabled = false;
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		movingDir = moving.None;
	}
	private void GamePause()
	{
		enabled = false;
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		paused = true;
		movingDir = moving.None;
		
	}
	private void GameUnpause()
	{
		paused = false;
		enabled = true;	
	}
}
