using UnityEngine;
using System.Collections;


public class Player : Character {
	
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
		UpdateMovement();
	}
}
