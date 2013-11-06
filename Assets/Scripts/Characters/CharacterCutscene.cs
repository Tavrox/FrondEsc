using UnityEngine;
using System.Collections;

[System.Serializable]

public class CharacterCutscene : MonoBehaviour {
	
	public OTAnimation anim;
	public OTAnimationFrameset[] framesets;

	
	
	// Use this for initialization
	void Start () {
		framesets[0] = anim.framesets[0];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
