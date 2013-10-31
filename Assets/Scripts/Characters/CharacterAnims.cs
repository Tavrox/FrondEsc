using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour 
{
	public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	
	public Transform spriteParent;
	public OTAnimatingSprite animSprite;
	public OTAnimation animation;
	//public tk2dAnimatedSprite animSprite;
	
	private anim currentAnim;
	private Character character;
	private Player aplayer;
	
	private bool animPlaying = false;
	
	// Use this for initialization
	void Start () 
	{
		character = GetComponent<Character>();
		aplayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();;
	}
	
	void Update() 
	{
		// run left
		if(character.isLeft && character.grounded == true && currentAnim != anim.WalkLeft)
		{
			currentAnim = anim.WalkLeft;
			animSprite.Play("run");
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(!character.isLeft && character.grounded == true && currentAnim != anim.StandLeft && character.facingDir == Character.facing.Left && animPlaying == false)
		{
			currentAnim = anim.StandLeft;
			animSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		
		// run right
		if(character.isRight && character.grounded && currentAnim != anim.WalkRight)
		{
			currentAnim = anim.WalkRight;
			animSprite.Play("run");
			spriteParent.localScale = new Vector3(1,1,1);
		}
		if(!character.isRight && character.grounded && currentAnim != anim.StandRight && character.facingDir == Character.facing.Right && animPlaying == false)
		{
			currentAnim = anim.StandRight;
			animSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// falling
		if(character.grounded == false && currentAnim != anim.FallLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.FallLeft;
			animSprite.Play("jump"); // fall left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(character.grounded == false && currentAnim != anim.FallRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.FallRight;
			animSprite.Play("jump"); // fall right
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// PLAYER SPECIFIC ANIMS
		// Shooting
		if (aplayer.shootingKnife == true)
		{
			animPlaying = true;
			currentAnim = anim.ShootRight;
			animSprite.Play("throw_knife");
			spriteParent.localScale = new Vector3(1,1,1);
			StartCoroutine( WaitAndCallback( animation.GetDuration(animation.framesets[3]) ) );
		}
		
		//ENEMIES SPECIFIC ANIMS
		if (character.isShot == true)
		{
			animPlaying = true;
			animSprite.Play("hurt");
			StartCoroutine( WaitAndCallback( animation.GetDuration(animation.framesets[2]) ) );
		}
	}
	
	IEnumerator WaitAndCallback(float waitTime)
	{
	    yield return new WaitForSeconds(waitTime);
	    AnimationFinished();
	}
		 
	void AnimationFinished()
	{
	    animPlaying = false;
	}
}
