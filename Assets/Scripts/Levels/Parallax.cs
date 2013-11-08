using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
	
	[SerializeField] private Player player;	
	[Range (0,10)] 	public int scrollSpeed = 4;
	//private int scrollSpeed;
	private Vector3 scrollVector;
	
	[HideInInspector] public Transform thisTransform;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		scrollVector.x = player.getVectorFixed().x; //Need vectorFixed to be public
		scrollVector.x = scrollVector.x/scrollSpeed;
		thisTransform.position += new Vector3(scrollVector.x,scrollVector.y,0f);
	}
}
