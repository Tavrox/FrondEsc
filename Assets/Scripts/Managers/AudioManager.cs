using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Debug.Log(MasterAudio.CurrentPlaylistSettings);
		print ( MasterAudio.CurrentPlaylistSettings[0]);
		print ( MasterAudio.CurrentPlaylistSettings[1]);
		print ( MasterAudio.CurrentPlaylistSettings[2]);
		print ( MasterAudio.CurrentPlaylistSettings[3]);
	
	}
}
