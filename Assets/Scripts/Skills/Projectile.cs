using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	
	public float ProjectileSpeed;
	public int dmg;
	private Transform myTransform;
	
	[HideInInspector] public Player player;
	
	[HideInInspector] public Vector3 direction;
	[HideInInspector] public float amtToMove;
	
	// Use this for initialization
	void Start () {
		myTransform = transform;
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		if(player.shootLeft == true) direction = Vector3.left;
		else direction = Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {
		
		amtToMove = ProjectileSpeed * Time.deltaTime;
		myTransform.Translate(direction * amtToMove);
		
		if(myTransform.position.x > (player.transform.position.x + 8.5f) || myTransform.position.x < (player.transform.position.x - 8.5f)) {
			Destroy(gameObject);
		}
	}
}
