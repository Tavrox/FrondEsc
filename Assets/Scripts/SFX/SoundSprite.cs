using UnityEngine;
using System.Collections;

public class SoundSprite : MonoBehaviour {
	
	private Player Owner;
	
	public MasterAudioGroup FooleySound;
	public MasterAudioGroup StepSound;
	public MasterAudioGroup onWalkingSound;
	public int[] frameWantedRun;
	public MasterAudioGroup onShootingSound;

	// Use this for initialization
	void Start () 
	{
		Owner = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		print (frameWantedRun[0]);
		print (frameWantedRun[1]);
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
//					return true;
				}
				else
				{
					Debug.Log("CurrFrame doesn't match wanted frame run");
//					return false;
				}
	        }
//			return true;
		}
		else 
		{
			Debug.LogError("Missing OnWalkingSound");
//			return false;
		}
	}
	
	IEnumerator cycleSounds(float _everyX, string _soundname)
	{
		yield return new WaitForSeconds(_everyX);
	    MasterAudio.PlaySound(_soundname);
	}
}