using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class SpeakingAsset : MonoBehaviour {

	[MenuItem("Assets/Create/Dialogue")]
	public static void CreateAsset () 
	{
		//ScriptableObjectUtility.CreateAsset<Speaks> ();
		ScriptableObject.CreateInstance<Speaks>();
	}
}
