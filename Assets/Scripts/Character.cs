using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	[HideInInspector] public enum facing
	{ Right, Left};
	[HideInInspector] public facing facingDir;
	
	[HideInInspector] public enum moving
	{ Right, Left, None };
	[HideInInspector] public moving movingDir;
	
 	[HideInInspector] public bool isLeft;
	[HideInInspector] public bool isRight;
	[HideInInspector] public bool isAir;
	[HideInInspector] public bool isCrounch;
	[HideInInspector] public bool isAlive;
	[HideInInspector] public bool isDying;
	[HideInInspector] public bool isGoDown;
	[HideInInspector] public bool isShooting;
	[HideInInspector] public bool isJump;
	
	[HideInInspector] public bool jumping;
	[HideInInspector] public bool passingPlatform;
	[HideInInspector] public bool onGround;
	[HideInInspector] public bool onPlatform;
	
	[HideInInspector] public bool blockedLeft;
	[HideInInspector] public bool blockedRight;
	[HideInInspector] public bool blockedUp;
	[HideInInspector] public bool blockedDown;
	
	private int maxJumps = 2;
	private int currJump;
	private float jump1y = 6.25f;
	private float jump2y = 10f;
	
	private Vector3 vectorMove;
	private Vector3 vectorFixed;
	
	private float maxVelocityY;
	private float maxVelocityX;
	private float gravityY = 20f;
	
	private float moveSpeed = 8f;
	private float rayDistanceUp = 0.25f;
	private float rayDistanceDown = 0.7f;
	
	private float absoluteVectorFixedX;
	private float absoluteVectorFixedY;
	
	private RaycastHit RayHit;
	protected Transform thisTransform;
	
	private float charHalfY = 0.6f;
	private float charHalfX = 0.5f ;
	
	protected int groundMask = 1 << 8 ; // Ground, Block
	protected int platformMask = 1 << 9; //Block
	private float pfPassSpeed = 2.8f;
	
	public virtual void Awake()
	{
		thisTransform = transform;
		onGround = true;
	}
	
	// Use this for initialization
	public virtual void Start ()
	{
		currJump = 0;
	}
	
	// Update is called once per frame
	public virtual void UpdateMovement()
	{
		vectorMove.x = 0;		
		if (isLeft == true)
		{
			vectorMove.x = -moveSpeed;
		}
		if (isRight == true)
		{
			vectorMove.x = moveSpeed;
		}
		if (isJump == true)
		{
			if (currJump < maxJumps)
			{
				jumping = true;	
				currJump += 1;
				if (currJump == 1)
				{
					vectorMove.y = jump1y;
				}
				if (currJump == 2)
				{
					vectorMove.y = jump2y;
				}
			}
		}
		
		if (onGround == true && vectorMove.y == 0)
		{
			jumping = false;
			currJump = 0;
		}
		
		updateRaycasts();
	
		if (onGround == false)
		{
			vectorMove.y -= gravityY * Time.deltaTime;
		}
		vectorFixed = vectorMove * Time.deltaTime;
		thisTransform.position += new Vector3(vectorFixed.x,vectorFixed.y,0f);
	}
	
	// =========RAYCASTS============== //
	
	void updateRaycasts()
	{
		blockedRight = false;
		blockedLeft = false;
		blockedUp = false;
		blockedDown = false;
		onGround = false;
		onPlatform = false;
		print (isGoDown);
		
		absoluteVectorFixedX = Mathf.Abs(vectorFixed.x);
		absoluteVectorFixedY = Mathf.Abs(vectorFixed.y);
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out RayHit, rayDistanceDown +absoluteVectorFixedY, groundMask) 
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out RayHit, rayDistanceDown + absoluteVectorFixedY, groundMask))
		{
			BlockedDown();
			Debug.DrawLine (thisTransform.position, RayHit.point, Color.cyan);
		}
		
		if (Physics.Raycast(new Vector3(thisTransform.position.x-0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out RayHit, rayDistanceDown +absoluteVectorFixedY, platformMask) 
			|| Physics.Raycast(new Vector3(thisTransform.position.x+0.25f,thisTransform.position.y,thisTransform.position.z), -Vector3.up, out RayHit, rayDistanceDown + absoluteVectorFixedY, platformMask))
		{
			if (isGoDown == true)
			{
				ThroughPlatform();
			}
			else
			{
				BlockedDown();
			}
			Debug.DrawLine (thisTransform.position, RayHit.point, Color.cyan);
		}
		
		if (Physics.Raycast(new Vector3 (thisTransform.position.x - 0.2f, thisTransform.position.y, thisTransform.position.z), Vector3.up, out RayHit, rayDistanceUp + absoluteVectorFixedY , groundMask)
			||
			Physics.Raycast(new Vector3 (thisTransform.position.x + 0.2f, thisTransform.position.y, thisTransform.position.z),
			Vector3.up, out RayHit, rayDistanceUp + absoluteVectorFixedY, groundMask))
		{
			BlockedUp();
			Debug.DrawLine (thisTransform.position, RayHit.point, Color.cyan);
		}
		
		if (Physics.Raycast(new Vector3 (thisTransform.position.x, thisTransform.position.y, thisTransform.position.z), Vector3.left, out RayHit, charHalfX + absoluteVectorFixedX , groundMask))
		{
			BlockedLeft();
			Debug.DrawLine (thisTransform.position, RayHit.point, Color.cyan);
		}
		if (Physics.Raycast(new Vector3 (thisTransform.position.x, thisTransform.position.y, thisTransform.position.z), Vector3.right, out RayHit, charHalfX + absoluteVectorFixedX , groundMask))
		{
			BlockedRight();
			Debug.DrawLine (thisTransform.position, RayHit.point, Color.cyan);
		}
		
	}
	
	void BlockedUp()
	{
		if (vectorMove.y > 0)
		{
				vectorMove.y = 0f;
				blockedUp = true;
		}
	}
	void BlockedDown()
	{
		if (vectorMove.y <= 0)
		{
			onGround = true;
			isJump = false;
			vectorMove.y = 0f;
			thisTransform.position = new Vector3(thisTransform.position.x, RayHit.point.y + charHalfY, 0f);
		}
	}
	
	void BlockedRight()
	{
		if (facingDir == facing.Right || movingDir == moving.Right)
		{
			vectorMove.x = 0f;
			blockedRight = true;
			thisTransform.position = new Vector3(RayHit.point.x - (charHalfX - 0.01f), thisTransform.position.y,0f);
		}
	}
	
	void BlockedLeft()
	{
		if (facingDir == facing.Left || movingDir == moving.Left)
		{
			vectorMove.x = 0f;
			blockedLeft = true;
			thisTransform.position = new Vector3(RayHit.point.x + (charHalfX - 0.01f), thisTransform.position.y, 0f);
		}
	}
	
	void ThroughPlatform()
	{
		vectorMove.y -= pfPassSpeed;
	}
	
	public void testPublic()
	{
		
	}
}
