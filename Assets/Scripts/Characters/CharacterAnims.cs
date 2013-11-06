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
	private Character _character;
	private Player _player;
	
	private bool animPlaying = false;
	
	// Use this for initialization
	void Start () 
	{
		_character = GetComponent<Character>();
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void Update() 
	{
		animSprite.looping = true;
		// run left
		if(_character.isLeft && _character.grounded == true && currentAnim != anim.WalkLeft)
		{
			currentAnim = anim.WalkLeft;
			animSprite.Play("run");
			InvertSprite();
		}
		if(!_character.isLeft && _character.grounded == true && currentAnim != anim.StandLeft && _character.facingDir == Character.facing.Left && animPlaying == false)
		{
			currentAnim = anim.StandLeft;
			animSprite.Play("stand"); // stand left
			InvertSprite();
		}
		
		// run right
		if(_character.isRight && _character.grounded && currentAnim != anim.WalkRight)
		{
			currentAnim = anim.WalkRight;
			animSprite.Play("run");
			NormalScaleSprite();;
		}
		if(!_character.isRight && _character.grounded && currentAnim != anim.StandRight && _character.facingDir == Character.facing.Right && animPlaying == false)
		{
			currentAnim = anim.StandRight;
			animSprite.Play("stand"); // stand left
			NormalScaleSprite();
		}
		
		// falling
		if(_character.grounded == false && currentAnim != anim.FallLeft && _character.facingDir == Character.facing.Left)
		{
			currentAnim = anim.FallLeft;
			animSprite.Play("jump"); // fall left
			InvertSprite();
		}
		if(_character.grounded == false && currentAnim != anim.FallRight && _character.facingDir == Character.facing.Right)
		{
			currentAnim = anim.FallRight;
			animSprite.Play("jump"); // fall right
			NormalScaleSprite();
		}
		
		// PLAYER SPECIFIC ANIMS
		// Shooting
		if (_player.shootingKnife == true)
		{
			animPlaying = true;
			currentAnim = anim.ShootRight;
			animSprite.Play("throw_knife");
			NormalScaleSprite();
			StartCoroutine( WaitAndCallback( animation.GetDuration(animation.framesets[3]) ) );
		}
		
		//ENEMIES SPECIFIC ANIMS
		if (_character.isShot == true && _character.facingDir == Character.facing.Left)
		{
			animPlaying = true;
			animSprite.Play("hurt");
			StartCoroutine( WaitAndCallback( animation.GetDuration(animation.framesets[2]) ) );
		}
		
		if (_player.paused == true)
		{
			currentAnim = anim.None;
			animSprite.looping = false;
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
	
	private void InvertSprite()
	{
		spriteParent.localScale = new Vector3(-1,1,1);
	}
	private void NormalScaleSprite()
	{
		spriteParent.localScale = new Vector3(1,1,1);
	}
}
