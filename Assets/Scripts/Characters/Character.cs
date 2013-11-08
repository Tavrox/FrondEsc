using UnityEngine;
using System.Collections;

public enum MyTeam { Team1, Team2, None }

public class Character : MonoBehaviour 
{
	public OTAnimation anim;

	/** ADD **/
	public int res_phys, res_mag;
	
	public int maxHP;
	[HideInInspector] public int HP;
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
	
	[HideInInspector] public Vector3 vectorFixed;
	protected Vector3 vectorMove;
	private Vector3 mypos;
	
	[Range (0,10)] 	public float 	moveVel = 4f;
	[Range (0,30)] 	public float 	jumpVel = 16f;
	[Range (0,30)] 	public float 	jump2Vel = 14f;
	[Range (1,2)] 	public int 		maxJumps = 2;
	[Range (15,25)] public float 	fallVel = 18f;
	
	[SerializeField] private int jumps = 0;
	[SerializeField] private float gravityY;
	[SerializeField] private float maxVelY = 0f;
		
	[SerializeField] private RaycastHit hitInfo;
	[SerializeField] private float halfMyX;
	[SerializeField] private float halfMyY;
	
	[SerializeField] private float absVel2X;
	[SerializeField] private float absVel2Y;
	
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
		HP = maxHP;
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
		mypos = new Vector3(thisTransform.position.x,thisTransform.position.y,thisTransform.position.z);
		
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
		
		
		//BLOCKED TO DOWN
		if (Physics.Raycast(mypos, Vector3.down, out hitInfo, halfMyY, platformMask))
		{
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.black);
			if (isCrounch == true)
			{
				passingPlatform = true;
				ThroughPlatform();
			}
			else 
			{
				BlockedDown();	
			}
		}
		if (Physics.Raycast(mypos, Vector3.down, out hitInfo, halfMyY, groundMask))
		{
			BlockedDown();
		}
		
		
		// BLOCKED TO UP
		if (Physics.Raycast(mypos, Vector3.up, out hitInfo, halfMyY, groundMask))
		{
			BlockedUp();
			Debug.DrawLine (thisTransform.position, hitInfo.point, Color.red);
		}
		
		// Blocked on right
		if( Physics.Raycast(mypos, Vector3.right, out hitInfo, halfMyX, groundMask) 
			|| Physics.Raycast(mypos, new Vector3(1f,0.8f,0) , out hitInfo, halfMyX, groundMask)
			|| Physics.Raycast(mypos, new Vector3(1f,-0.8f,0), out hitInfo, halfMyX, groundMask))
		{
			BlockedRight();
			Debug.DrawRay(mypos, Vector3.right, Color.cyan);
		}
		
		// Blocked on left
		if(	Physics.Raycast(mypos, Vector3.left, out hitInfo, halfMyX, groundMask)
			|| Physics.Raycast(mypos, new Vector3(-1f,0.8f,0), out hitInfo, halfMyX, groundMask)
			|| Physics.Raycast(mypos, new Vector3(-1f,0.8f,0), out hitInfo, halfMyX, groundMask))
		{
			BlockedLeft();
			Debug.DrawRay(mypos, Vector3.left, Color.yellow);
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
			thisTransform.position = new Vector3(thisTransform.position.x, hitInfo.point.y + halfMyY - 0.1f, thisTransform.position.z);
		}
	}
	void BlockedRight()
	{
		if(facingDir == facing.Right || movingDir == moving.Right)
		{
			blockedRight = true;
			vectorMove.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x-(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
			
		}
	}
	
	void BlockedLeft()
	{
		if(facingDir == facing.Left || movingDir == moving.Left)
		{
			blockedLeft = true;
			vectorMove.x = 0f;
			thisTransform.position = new Vector3(hitInfo.point.x+(halfMyX-0.01f),thisTransform.position.y, 0f); // .01 less than collision width.
		}
	}
	
	public Vector3 getVectorFixed()
	{
		return vectorFixed;	
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