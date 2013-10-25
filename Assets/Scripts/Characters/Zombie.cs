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
	
    
    public override void Start () 
	{
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
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
		isPass = false;
		//System.Console.WriteLine("test");
		movingDir = moving.None;
		
		Debug.Log(this.HP);
		
	    //rotate to look at the player
		//    myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
		//    Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
		//     
		//    //move towards the player
		//    myTransform.position += myTransform.forward * movevectorMove * Time.deltaTime;
		
		if(chasingPlayer) {
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
			UpdateMovement();
			//myTransform.Translate(direction * movevectorMove * Time.deltaTime);
		}
		//myTransform.position = Vector3.Lerp(myTransform.position, target.position, movevectorMove * Time.deltaTime);
    
		detectTargetLeft = new Ray(thisTransform.position, Vector3.left);
		detectTargetRight = new Ray(thisTransform.position, Vector3.right);
  		Debug.DrawRay(thisTransform.position, Vector3.left*targetDetectionArea);
		Debug.DrawRay(thisTransform.position, Vector3.right*targetDetectionArea);
		
		if (Physics.Raycast(detectTargetLeft, out hitInfo, targetDetectionArea) || Physics.Raycast(detectTargetRight, out hitInfo, targetDetectionArea)) {
			if(hitInfo.collider.tag == "Player") {
				chasingPlayer = true;
				//Debug.Log("CHASE");
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
				//Debug.Log("JUMP");
			}
		}
    }
	
	void OnTriggerEnter(Collider other) 
	{
		print ("omg");
		
		if(other.gameObject.CompareTag("Player")) {
			GameEventManager.TriggerGameOver();
			//Debug.Log("Collide");
			chasingPlayer = false;
		}
		if(other.gameObject.CompareTag("Bullets")) {
			HP-= other.GetComponent<Bullets>().Skill.damages;
			if(HP <= 0) {
				//Debug.Log("HEADSHOT");
				chasingPlayer = false;
				Destroy(gameObject);
			}
		}
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
//	void OnGUI() {
//		Rect rect = new Rect(0,0,250,50);
//    	GUI.Box(rect,"This is the text to be displayed");     
//    }
}
