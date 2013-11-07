using UnityEngine;
using System.Collections;

public class Player : Character {
	
	[HideInInspector] public Vector3 position;
	[HideInInspector] public Transform trans;
	
	[HideInInspector] public Dialog dialog;
	//public Skill skillLaunch;
	
	public Skills skill_knife;
	public Skills skill_axe;
	public Skills skill_shield;
	public OTSprite menu;
	
	[SerializeField] private Rect hp_display;
	[SerializeField] private SoundSprite soundMan;
	public OTAnimatingSprite currSprite;
	
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
		soundMan = GetComponent<SoundSprite>();
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
		isCrounch = false;
		
		shootingKnife = false;
		movingDir = moving.None;
		
		// keyboard input
		if(Input.GetKey("left")) 
		{ 
			soundMan.OnWalking(getCurrentFrameIndex());
			isLeft = true;
			shootLeft = true;
			facingDir = facing.Left;
		}
		if (Input.GetKey("right") && isLeft == false) 
		{ 
			soundMan.OnWalking(getCurrentFrameIndex());
			isRight = true; 
			facingDir = facing.Right;
			shootLeft = false;
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			isCrounch = true;
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
		Debug.Log ("Player_Shield" + hasShield);
		Debug.Log ("Player_HP" + HP);
		
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
	public void RegenHP(int _val)
	{
		this.HP += _val;
		
	}
	private void DisplayHP()
	{
		
	}
	public OTAnimatingSprite getSprite()
	{
		return currSprite;
	}
	public int getCurrentFrameIndex()
	{
		return currSprite.CurrentFrame().index;
	}
}
