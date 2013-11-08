using UnityEngine;
using System.Collections;

public class ModulatedSound : MonoBehaviour {

	public MasterAudioGroup sound;
	
	public enum condType { Percent, Steps } ;
	public condType condTy;
	public enum condTarget { Life } ;
	public condTarget condTar;
	
	public int cond1;
	public int cond2;
	public int cond3;
	public int cond4;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PercentSound(Player playr)
	{
		float percentHP = playr.HP / playr.maxHP;
		print (percentHP);
		MasterAudio.PlaySound(sound.name);
		
		sound.groupMasterVolume = 1f;
		print ( "Sound S1 " + sound.groupMasterVolume);
		sound.groupMasterVolume = 0.5f;
		print ( "Sound S2 " + sound.groupMasterVolume);
	}
}
