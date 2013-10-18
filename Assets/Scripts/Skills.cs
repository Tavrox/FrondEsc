using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {
	
	private int id;
	private enum skillEnum { Physic, Magic, Shield };
	private skillEnum skillType;
	private float cooldown;
	private int bulletSizeX;
	private int bulletSizeY;
	private enum goThroughTypes { Walls, Platforms, Enemies };
	private goThroughTypes goThrough;
	private enum specialEffectsTypes { Dash_Left, Dash_Right, Etherate, Dash_up};
	private specialEffectsTypes specialEffects;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
