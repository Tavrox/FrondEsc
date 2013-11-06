using UnityEngine;
using System.Collections;

[System.Serializable]

public class SpeakingElement : MonoBehaviour 
{
	public enum CharacList {Oregos, Machin, Bidule};
	public enum AvatarPos {Left, Right};
	public CharacList Charac;
	public AvatarPos CharacPos;
	public Texture2D CharacPic;
	public string Speaking;
	public GUIStyle SpeakingTextStyle;
	public float TextPlayBackSpeed;
	public AudioClip PlayBackSoundFile;
	
	
	
}
