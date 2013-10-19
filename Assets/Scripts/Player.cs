using UnityEngine;
using System.Collections;


public class Player : Character {
	
	
	[HideInInspector] public Skills sk1;
	[HideInInspector] public Skills sk2;
	[HideInInspector] public Skills sk3;
	[HideInInspector] public Skills sk4;
	
	private bool sk1Active;
	private bool sk2Active;
	private bool sk3Active;
	private bool sk4Active;
	private bool isIdle;
	
	private Ray dirBullets;
	public Bullets bull;
	
	// Use this for initialization
	public override void Start ()
	{
		base.Start();
	}
	
	// Update is called once per frame
	public void Update() 
	{
		
		isLeft = false;
		isRight = false;
		isJump = false;
		isGoDown = false;
		movingDir = Character.moving.None;
		
		
		// Basic Movements
		if (Input.GetKeyDown(KeyCode.Space))
		{
			isJump = true;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			isGoDown = true;
		}
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			isLeft = true;
			facingDir = facing.Left;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			isRight = true;
			facingDir = facing.Right;
		}
		
		if (Input.GetKey(KeyCode.Alpha1))
		{
			sk1Active = true;
			print ("yes");
		}
		
		
		UpdateMovement();
	}
	
	void useSkill(Skills sk)
	{
		if (sk == sk1)
		{
			
		}
		else if (sk == sk2)
		{
			
		}
		else if (sk == sk3)
		{
			
		}
		else if (sk == sk4)
		{
			
		}
	}
}
