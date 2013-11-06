using UnityEngine;
using System.Collections;

public enum MyTeam { Team1, Team2, None }

public class Character : MonoBehaviour 
{
	public OTAnimatingSprite sprite;

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
	
	[HideInInspector] public bool alive = true;
	[HideInInspector] public Vector3 spawnPos;
	
	[HideInInspector] public bool hasShield = false; 
	[HideInInspector] public int shieldDef; 
	
	[HideInInspector] public Transform thisTransform;
	
	private Vector3 vectorFixed;
	protected Vector3 vectorMove;
	
	[Range (0,10)] 	public float 	moveVel = 4f;
	[Range (0,30)] 	public float 	jumpVel = 16f;
	[Range (0,30)] 	public float 	jump2Vel = 14f;
	[Range (1,2)] 	public int 		maxJumps = 2;
	[Range (15,25)] public float 	fallVel = 18f;
	
	private int jumps = 0;
	private float gravityY;
	private float maxVelY = 0f;
		
	private RaycastHit hitInfo;
	private float halfMyX;
	private float halfMyY;
	private double doubleTo;
	[HideInInspector] public float rayDistUp = 0.375f;
	
	private float absVel2X;
	private float absVel2Y;
	
//	private float rayDistanceUp = 0.25f;
	private float rayDistanceDown = 1.2f;
//	private float absoluteVectorFixedX;
	private float absoluteVectorFixedY = 0f;
	
	// layer masks
	protected int groundMask = 1 << 8; // Ground, Block
	protected int platformMask = 1 << 9; //Block
	private float pfPassSpeed = 2.8f;
	
	public virtual void Awake()
	{
		thisTransform = transform;
	
	}
	
	// Use this for initialization
	public virtual void Start () 
	{
		maxVelY = fallVel;
		vectorMove.y = 0;
		halfMyX = GetComponentInChildren<Transform>().GetComponentInChildren<OTAnimatingSprite>().size.x * 0.5f;
		halfMyY = GetComponentInChildren<Transform>().GetComponentInChildren<OTAnimatingSprite>().size.y * 0.5f;
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
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out hitInfo, rayDistanceDown + absoluteVectorFixedY, groundMask))
		{
			BlockedDown();
			if (isGoDown == true)
			{
				isCrounch = true;
			}
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.green);
		}
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistanceDown +absoluteVectorFixedY, platformMask)
		|| Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistanceDown + absoluteVectorFixedY, platformMask))
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
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.red);
		}
		
		// blocked up
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.2f,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask)
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.2f,thisTransform.position.y,thisTransform.position.z), Vector3.up, out hitInfo, rayDistUp+absVel2Y, groundMask))
		{
			BlockedUp();
		}
		
		// blocked on right
		if (Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.right, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedRight();
			Debug.DrawRay(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.right, Color.cyan);
		}
		
		// blocked on left
		if(Physics.Raycast(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.left, out hitInfo, halfMyX+absVel2X, groundMask))
		{
			BlockedLeft();
			Debug.DrawRay(new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z), Vector3.left, Color.cyan);
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
			thisTransform.position = new Vector3(thisTransform.position.x, hitInfo.point.y + halfMyY, 0f);
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
	
	void OnGUI()
	{
		moveVel = GUI.HorizontalSlider (new Rect (25, 25, 100, 30), moveVel, 0f, 10f);
	}
}