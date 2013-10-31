using UnityEngine;
using System.Collections;

public class CharacterAnims : MonoBehaviour 
{
	public enum anim { None, WalkLeft, WalkRight, RopeLeft, RopeRight, Climb, ClimbStop, StandLeft, StandRight, HangLeft, HangRight, FallLeft, FallRight , ShootLeft, ShootRight }
	
	public Transform spriteParent;
	public OTAnimatingSprite playerSprite;
	public OTAnimation animation;
	//public tk2dAnimatedSprite playerSprite;
	
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
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(!character.isLeft && character.grounded == true && currentAnim != anim.StandLeft && character.facingDir == Character.facing.Left && animPlaying == false)
		{
			currentAnim = anim.StandLeft;
			playerSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		
		// run right
		if(character.isRight && character.grounded && currentAnim != anim.WalkRight)
		{
			currentAnim = anim.WalkRight;
			playerSprite.Play("run");
			spriteParent.localScale = new Vector3(1,1,1);
		}
		if(!character.isRight && character.grounded && currentAnim != anim.StandRight && character.facingDir == Character.facing.Right && animPlaying == false)
		{
			currentAnim = anim.StandRight;
			playerSprite.Play("stand"); // stand left
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// falling
		if(character.grounded == false && currentAnim != anim.FallLeft && character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.FallLeft;
			playerSprite.Play("jump"); // fall left
			spriteParent.localScale = new Vector3(-1,1,1);
		}
		if(character.grounded == false && currentAnim != anim.FallRight && character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.FallRight;
			playerSprite.Play("jump"); // fall right
			spriteParent.localScale = new Vector3(1,1,1);
		}
		
		// PLAYER SPECIFIC ANIMS
		// Shooting
		if (aplayer.shootingKnife == true)
		{
			animPlaying = true;
			currentAnim = anim.ShootRight;
			playerSprite.Play("throw_knife");
			spriteParent.localScale = new Vector3(1,1,1);
			StartCoroutine( WaitAndCallback( animation.GetDuration(animation.framesets[3]) ) );
			print ("Duration" + animation.framesets[3].singleDuration);
			print ("Frameset" + animation.framesets[3]);
		}
	}
	
	IEnumerator WaitAndCallback(float waitTime)
	{
	    yield return new WaitForSeconds(waitTime);
	    AnimationFinished();
	}
		 
	void AnimationFinished()
	{
		print ("AnimFinished");
	    animPlaying = false;
	}
}
