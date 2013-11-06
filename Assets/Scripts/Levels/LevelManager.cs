using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public OTSprite background;
	[SerializeField] private Player player;
	[SerializeField] private Camera camera;
	public Waypoints waypoint1;
	public Waypoints waypoint2;
	
	public int ID;
	public int nextLvlID;
	public int previousLvlID;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
//		camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		camera.transform.position = new Vector3 (player.transform.position.x, 0, player.transform.position.z);
		camera.nearClipPlane = -1000;
	}
}
