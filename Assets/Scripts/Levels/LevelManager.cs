using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public OTSprite background;
	public Player player;
	public Camera camera;
	
	public int ID;
	public int nextLvlID;
	public int previousLvlID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		camera.transform.position = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
		camera.nearClipPlane = -1000;
	}
}
