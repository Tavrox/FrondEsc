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
	
	public void PercentSound(Character charac)
	{
		float percentHP = (charac.HP * 1.0f / charac.maxHP * 1.0f);		
		sound.groupMasterVolume = percentHP;
		MasterAudio.PlaySound(sound.name);
	}
}
