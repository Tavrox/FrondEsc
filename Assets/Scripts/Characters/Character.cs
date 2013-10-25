using UnityEngine;
using System.Collections;

public enum MyTeam { Team1, Team2, None }

public class Character : MonoBehaviour 
{
	public MyTeam myTeam = MyTeam.Team1;
	
	/** ADD **/
	public int HP, res_phys, res_mag;
	[HideInInspector] public bool isShot;
	[HideInInspector] public bool talking;
	[HideInInspector] public bool isGoDown;
	[HideInInspector] public bool isCrounch;
	[HideInInspector] public bool isLookup;
	[HideInInspector] public bool shootLeft = false;
	protected float runSpeed = 1f;
	/** END **/

	[HideInInspector] public enum facing { Right, Left, Down, Up }
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public enum moving { Right, Left, None }
	[HideInInspector] public moving movingDir;
	
	[HideInInspector] public bool isLeft; 
	[HideInInspector] public bool isRight;
	[HideInInspector] public bool isJump;
	[HideInInspector] public bool isPass;
	
	[HideInInspector] public bool jumping = false;
	[HideInInspector] public bool grounded = false;
	[HideInInspector] public bool passingPlatform;
	[HideInInspector] public bool onPlatform;
	
	[HideInInspector] public bool blockedRight;
	[HideInInspector] public bool blockedLeft;
	[HideInInspector] public bool blockedUp;
	[HideInInspector] public bool blockedDown;
	
	[HideInInspector] public bool isScoring;
	[HideInInspector] public bool alive = true;
	[HideInInspector] public Vector3 spawnPos;
	
	[HideInInspector] public Transform thisTransform;
	
	private float moveVel = 4f;
//	private float walkVel = 3f; // walk while carrying ball
	private Vector3 vectorFixed;
	protected Vector3 vectorMove;
	
	private float jumpVel = 16f;
	private float jump2Vel = 14f;
	private float fallVel = 18f;
	
	private int jumps = 0;
    private int maxJumps = 2; // set to 2 for double jump
	
	private float gravityY;// = 52f;
	private float maxVelY = 0f;
		
	private RaycastHit hitInfo;
	private float halfMyX = 0.325f; //0.25f;
	private float halfMyY = 0.5f;//0.375f;
	[HideInInspector] public float rayDistUp = 0.375f;
	
	private float absVel2X;
	private float absVel2Y;
//	private float rayDistanceUp = 0.25f;
	private float rayDistanceDown = 0.7f;
	
	
	private float charHalfY = 0.6f;
//	private float charHalfX = 0.5f ;
	
//	private float absoluteVectorFixedX;
	private float absoluteVectorFixedY;
	
	// layer masks
	protected int groundMask = 1 << 8; // Ground, Block
	protected int platformMask = 1 << 9; //Block
	private float pfPassSpeed = 2.8f;
		
	protected bool hasBall = false;
	protected string team = "";
	
	
	public virtual void Awake()
	{
		thisTransform = transform;
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		maxVelY = fallVel;
		vectorMove.y = 0;
		StartCoroutine(StartGravity());
	}
	
	IEnumerator StartGravity()
	{
		// wait for things to settle before applying gravity
		yield return new WaitForSeconds(0.1f);
		gravityY = 52f;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement() 
	{
		if(alive == false) return;
		
		vectorMove.x = 0;
		
		// pressed right button
		if(isRight == true)
		{
			vectorMove.x = moveVel;
		}
		
		// pressed left button
		if(isLeft == true)
		{			
			vectorMove.x = -moveVel;
		}
		
		// pressed jump button
		if (isJump == true)
		{
			if (jumps < maxJumps)
		    {
				jumps += 1;
				jumping = true;
				if(jumps == 1)
				{
					vectorMove.y = jumpVel;
				}
				if(jumps == 2)
				{
					vectorMove.y = jump2Vel;
				}
		    }
		}
		
		// landed from fall/jump
		if(grounded == true && vectorMove.y == 0)
		{
			jumping = false;
			jumps = 0;
		}
		
		UpdateRaycasts();
		
		// apply gravity while airborne
		if(grounded == false)
		{
			vectorMove.y -= gravityY * Time.deltaTime;
		}
		
		// velocity limiter
		if(vectorMove.y < -maxVelY)
		{
			vectorMove.y = -maxVelY;
		}
		
		// apply movement
		vectorMove.x = vectorMove.x * runSpeed; //ADD
		vectorFixed = vectorMove * Time.deltaTime;
		thisTransform.position += new Vector3(vectorFixed.x,vectorFixed.y,0f);
		
	}
	
	// ============================== RAYCASTS ============================== 
	
	void UpdateRaycasts()
	{
		blockedRight = false;
		blockedLeft = false;
		blockedUp = false;
		blockedDown = false;
		grounded = false;		
		
		absVel2X = Mathf.Abs(vectorFixed.x);
		absVel2Y = Mathf.Abs(vectorFixed.y);
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out hitInfo, rayDistanceDown + absoluteVectorFixedY, groundMask)
		|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out hitInfo, rayDistanceDown + absoluteVectorFixedY, groundMask))
		{
			BlockedDown();
			if (isGoDown == true)
			{
				isCrounch = true;
			}
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.cyan);
		}
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out hitInfo, rayDistanceDown +absoluteVectorFixedY, platformMask)
		|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out hitInfo, rayDistanceDown + absoluteVectorFixedY, platformMask))
		{
			if (isGoDown == true)
			{
				passingPlatform = true;
				ThroughPlatform();
			}
			else
			{
				BlockedDown();
			}
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.cyan);
		}
		//par le bas
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.down, out hitInfo, 0.6f+absVel2Y, groundMask))
		{			
			// not while jumping so he can pass up thru platforms
			if(vectorMove.y <= 0)
			{
				grounded = true;
				vectorMove.y = 0f; // stop falling			
				thisTransform.position = new Vector3(thisTransform.position.x,hitInfo.point.y+halfMyY,0f);
			}
		}
		
		// blocked up
		/*if (Physics.Raycast(new Vector3(thisTransform.position.x-0.2f,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.2f,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask))
		{
			BlockedUp();
		}*/
		
		// blocked on right
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedRight();
		}
		
		// blocked on left
		if(Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.left, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedLeft();
		}
	}
	
	void BlockedUp()
	{
		if(vectorMove.y > 0)
		{
			vectorMove.y = 0f;
			blockedUp = true;
		}
	}
	void BlockedDown()
	{
		if (vectorMove.y <= 0)
		{
			grounded = true;
			isJump = false;
			vectorMove.y = 0f;
			thisTransform.position = new Vector3(thisTransform.position.x, hitInfo.point.y + charHalfY, 0f);
		}
	}
	void BlockedRight()
	{
		if(grounded && facingDir == facing.Right || movingDir == moving.Right)
		{
			blockedRight = true;
			vectorMove.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
	
	void BlockedLeft()
	{
		if(grounded && facingDir == facing.Left || movingDir == moving.Left)
		{
			blockedLeft = true;
			vectorMove.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
	
	void ThroughPlatform()
	{
		vectorMove.y -= pfPassSpeed;
	}
}
