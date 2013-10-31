using UnityEngine;
using System.Collections;

public class Zombie : Enemy {
	private Transform target; //the enemy's target
   // public float movevectorMove = 1f; //move speed
  //  public int rotationSpeed = 3; //speed of turning
	private bool chasingPlayer;
	private Vector3 direction;
	
	public float targetDetectionArea = 3;
	public float blockDetectionArea = 1;
    
	private RaycastHit hitInfo; //infos de collision
	private Ray detectTargetLeft, detectTargetRight, detectBlockLeft, detectBlockRight; //point de départ, direction
	
	private bool go = true;
	private int waypointId = 0;
	public Transform[] waypoints;
    
    public override void Start () 
	{
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.GamePause += GamePause;
		GameEventManager.GameUnpause += GameUnpause;
		
		enabled = false;
		
		HP = 150;
		res_mag = 50;
		res_phys = 10;
		runSpeed = 0.5f;
		
		spawnPos = thisTransform.position;
    	target = GameObject.FindWithTag("Player").transform; //target the player
    }
     
    void Update () {
//		if(null) FindObjectOfType(typeof(Zombie));
		
		
		isLeft = false;
		isRight = false;
		isJump = false;
		isShot = false;
		isPass = false;
		movingDir = moving.None;
		facingDir = facing.Left;
		
	    //rotate to look at the player
		//    myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
		//    Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		//     
		//    //move towards the player
		//    myTransform.position += myTransform.forward * movevectorMove * Time.deltaTime;
		
		if(chasingPlayer) {ChasePlayer();UpdateMovement();}
		else {Patrol();UpdateMovement();}
		//myTransform.position = Vector3.Lerp(myTransform.position, target.position, movevectorMove * Time.deltaTime);
    
		detectTargetLeft = new Ray(thisTransform.position, Vector3.left);
		detectTargetRight = new Ray(thisTransform.position, Vector3.right);
  		Debug.DrawRay(thisTransform.position, Vector3.left*targetDetectionArea);
		Debug.DrawRay(thisTransform.position, Vector3.right*targetDetectionArea);
		
		if (Physics.Raycast(detectTargetLeft, out hitInfo, targetDetectionArea) || Physics.Raycast(detectTargetRight, out hitInfo, targetDetectionArea)) {
			if(hitInfo.collider.tag == "Player") {
				chasingPlayer = true;
			}
		}
		
		detectBlockLeft = new Ray(thisTransform.position, Vector3.left);
		detectBlockRight = new Ray(thisTransform.position, Vector3.right);
  		Debug.DrawRay(thisTransform.position, Vector3.left*blockDetectionArea);
		Debug.DrawRay(thisTransform.position, Vector3.right*blockDetectionArea);
		if (Physics.Raycast(detectBlockLeft, out hitInfo, blockDetectionArea) || Physics.Raycast(detectBlockRight, out hitInfo, blockDetectionArea)) {
			if(hitInfo.collider.tag == "Boxes") {
				isJump = true;
				UpdateMovement();
			}
		}
    }
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.CompareTag("Player")) 
		{
			Character ch = other.GetComponent<Character>();
			if (ch.hasShield == false)
			{
				GameEventManager.TriggerGameOver();
				chasingPlayer = false;
			}
		}
		if(other.gameObject.CompareTag("Bullets")) 
		{
			Bullets bull = other.GetComponent<Bullets>();
			if (bull.bullType != Bullets.bullTopo.Shield)
			{
				isShot = true;
				HP-= bull.Skill.damages;
			}
			if(HP <= 0) 
			{
				chasingPlayer = false;
				Destroy(gameObject);
			}
		}
	}
	
	private void Patrol () {
		
		if(waypoints.Length<=0) print("No Waypoints linked");
		
//		print(Vector3.Distance(waypoints[waypointId].position, transform.position));
//		print(go);print(waypointId);
//		print(waypoints[waypointId].position+" "+transform.position);
		
		if(Vector3.Distance(transform.position, waypoints[waypointId].position) < 1) {
			go = !go;
			if(go) waypointId=0;
			else if (!go) waypointId=1;
		}
		
//		if(Vector3.Distance(transform.position, waypoints[waypointId].position) < 1) {
//			if(go) waypointId++;
//			else waypointId--;
//			//print(go);
//			if(waypointId >= waypoints.Length) {go=false;waypointId--;waypointId--;}
//			else if (waypointId <= 0) {go=true;waypointId++;waypointId++;}
//		}
		
		if(go) {
				isLeft = true;
				facingDir = facing.Left;
		}
		else {
				isRight = true;
				facingDir = facing.Right;
		}
	}
	
	private void ChasePlayer () {
		//Debug.Log("Px ="+target.position.x+" / Zx ="+myTransform.position.x);
		if (target.position.x < thisTransform.position.x) {
			//direction = Vector3.left;
			isLeft = true;
			facingDir = facing.Left;
		}
		else if (target.position.x >= thisTransform.position.x && isLeft == false) {
			//direction = Vector3.right;
			isRight = true; 
			facingDir = facing.Right;
		}
		//myTransform.Translate(direction * movevectorMove * Time.deltaTime);
	}
	private void GameStart () {
		if(FindObjectOfType(typeof(Zombie)) && this != null) {
			transform.localPosition = spawnPos;
			enabled = true;
		}
	}
	
	private void GameOver () {
		enabled = false;
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		movingDir = moving.None;
	}
	private void GamePause()
	{
		enabled = false;	
	}
	private void GameUnpause()
	{
		enabled = true;	
	}
//	void OnGUI() {
//		Rect rect = new Rect(0,0,250,50);
//    	GUI.Box(rect,"This is the text to be displayed");     
//    }
}
