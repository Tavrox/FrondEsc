using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

	protected Transform trans;
	
	public Bullets bull;
	public Character Owner;
	public OTAnimationFrameset skill_fb;
	
	public int ID;
	public float cooldown;
	public Input selec;
	public int damages;
	
	public enum SkillList {Knife, Axe, Shield};
	public SkillList SkillNames;
	
	private enum specialEffectsTypes { Dash_Left, Dash_Right, Etherate, Dash_up};
	private specialEffectsTypes specialEffects;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public void useSkill(SkillList sk)
	{
		if (sk == SkillList.Knife)
		{
			launchBullets(sk);
			gfxFb(sk);
			sfxFB(sk);
		}
		else if (sk == SkillList.Axe)
		{
			launchBullets(sk);
			gfxFb(sk);
		}
		else if (sk == SkillList.Shield) 
		{
			launchShield(sk);
			gfxFb(sk);
			print (sk);
		}
	}
	
	private void launchBullets(SkillList sk)
	{
		// Launch bullets
		Instantiate(bull, new Vector3(0,0,0), Quaternion.identity);
		// do side effects
		// Add sound
	}
	void launchShield(SkillList sk)
	{
		Instantiate(bull, new Vector3(0,0,0), Quaternion.identity);
		Owner.hasShield = true;
		Owner.shieldDef = damages;
	}
	
	public void gfxFb(SkillList sk)
	{
	// Instantiate( skill_fb, new Vector3(0,0,0), Quaternion.identity);
	}
	void sfxFB(SkillList sk)
	{
		if (sk == SkillList.Knife)
		{
			MasterAudio.PlaySound("Player_knife_1");
		}
		else if (sk == SkillList.Axe)
		{
			
		}
		else if (sk == SkillList.Shield) 
		{
			
		}
	}
}