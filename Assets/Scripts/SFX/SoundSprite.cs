using UnityEngine;
using System.Collections;

public class SoundSprite : MonoBehaviour {
	
	private Player Owner;
	
	public MasterAudioGroup FooleySound;
	public MasterAudioGroup StepSound;
	public MasterAudioGroup onJumpingSound;
	public MasterAudioGroup onWalkingSound;
	public MasterAudioGroup onGettingHitSound;
	public MasterAudioGroup onHealingSound;
	public MasterAudioGroup onShootingSound;
	
	public int[] frameWantedRun;
	
	public enum ActionList{Jumping, Walking, GettingHit, Healing, Shooting};
	private ActionList actions;
	
	[SerializeField] private MasterAudioGroup soundMatchAction;

	// Use this for initialization
	void Start () 
	{
		Owner = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	public void OnShooting( MasterAudioGroup onShooting)
	{
		
	}
	
	public void OnWalking(int currFrameIndex ,float _delay = 0f, bool _randPitch = false, float _everyX = 2f )
	{
		if (onWalkingSound != null)
		{
			for (int i = 0; i == 2; i++)
	        {
				if (currFrameIndex == frameWantedRun[0] || currFrameIndex == frameWantedRun[1] )
				{
					string nameSd = onWalkingSound.name;
					onWalkingSound.limitPolyphony = true;
					MasterAudio.PlaySound("Player_run_1_v2");
					print ("OMG");
				}
				else
				{
					Debug.Log("CurrFrame doesn't match wanted frame run");
				}
	        }
		}
		else 
		{
			Debug.LogError("Missing OnWalkingSound");
		}
	}
	
	public void onAction(ActionList _actions ,float _delay = 0f, bool _randPitch = false, float _everyX = 2f  )
	{
		switch (_actions)
		{
			case (ActionList.GettingHit) :
			{
				break;
			}
			case (ActionList.Healing) :
			{
				break;
			}
			case (ActionList.Jumping) :
			{
				break;
			}
			case (ActionList.Shooting) :
			{
				break;
			}
			case (ActionList.Walking) :
			{
				break;
			}
			default :
			{
				Debug.LogError("Default switch on action triggered");
				break;
			}
		}
	}
	
	IEnumerator cycleSounds(float _everyX, string _soundname)
	{
		yield return new WaitForSeconds(_everyX);
	    MasterAudio.PlaySound(_soundname);
	}
}