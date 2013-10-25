using UnityEngine;
using System.Collections;

public class Player : Character {
	
	public GameObject ProjectilePrefab;
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
		
		HP = 200;
		
		spawnPos = thisTransform.position;
		//dialog = GameObject.Find("Dialog").GetComponent<Dialog>();
	}
	
	// Update is called once per frame
	public void Update () 
	{
//		if(null) FindObjectOfType(typeof(Player));
		
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
		if(Input.GetKeyDown(KeyCode.AltGr))
		{
			//Debug.Log(isLeft);
			//Fire projectile
//			if(isLeft) {position = new Vector3(transform.position.x - (transform.localScale.x / 2), transform.position.y);}
//			else if(isRight) {position = new Vector3(transform.position.x + (transform.localScale.x / 2), transform.position.y); }
			position = new Vector3(transform.position.x,transform.position.y);
			Instantiate(ProjectilePrefab, position, Quaternion.identity);
			//skillLaunch = new Skill(1, "phys", 50, 3);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			skill_knife.useSkill(Skills.SkillList.Knife);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			skill_axe.useSkill(Skills.SkillList.Axe);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			skill_axe.useSkill(Skills.SkillList.Shield);
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
