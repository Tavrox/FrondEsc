using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

public class SpeakingAsset 
{

	[MenuItem("Assets/Create/Speak")]
	public static void CreateAsset () 
	{
		CustomAssetUtility.CreateAsset<Speak>();
	}
}
