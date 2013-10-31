using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Alpha4))
		{
			MasterAudio.TriggerPlaylistClip("Music_Univers1");
		}
	}
}
