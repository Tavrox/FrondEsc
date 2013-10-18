using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour {
	
	public enum anim
	{
		None,
		Default, Jumping, Falling, Recovering,
		WalkLeft, FacingLeft,
		WalkRight, FacingRight,
		Skill_1, Skill_2, Skill_3, Skill_4,
		Dash
		
	}
	
	public Transform spriteParent;
	public OTAnimatingSprite playerSprite;
	
	private anim currentAnim;
	private Character character;
	
	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if (GameLoop.gameOver == true) return;
		
		
		// RUN
			// Left
			if (character.onGround == true && character.facingDir == Character.facing.Left)
			{
				currentAnim = anim.WalkLeft;
				playerSprite.Play("run");
				spriteParent.localScale = new Vector3(-1,1,1);
			}
			// Right
			if (character.onGround == true && character.facingDir == Character.facing.Right)
			{
				currentAnim = anim.WalkRight;
				playerSprite.Play("run");
				spriteParent.localScale = new Vector3(1,1,1);
			}
		// STAND
			// Left
			if (character.onGround == true && character.facingDir == Character.facing.Left)
			{
				currentAnim = anim.FacingLeft;
				spriteParent.localScale = new Vector3(-1,1,1);
				//playerSprite.Play("run");
			}
			// Right
			if (character.onGround == true && character.facingDir == Character.facing.Right)
			{
				currentAnim = anim.FacingRight;
				spriteParent.localScale = new Vector3(1,1,1);
				///playerSprite.Play("run");
			}
		
		// JUMP
			// Left
			if (character.jumping == true && character.facingDir == Character.facing.Left)		
			{
				currentAnim = anim.Jumping;
				playerSprite.Play("falling");
				spriteParent.localScale = new Vector3(-1,1,1);
			}
			// Right
			if (character.jumping == true && character.facingDir == Character.facing.Right)		
			{
				currentAnim = anim.Jumping;
				playerSprite.Play("falling");
				spriteParent.localScale = new Vector3(1,1,1);
			}
			
		// GODOWN			
		if (character.passingPlatform == true)
		{
			//playerSprite.Play("passingPlatform");
		}
	
	}
	
}
