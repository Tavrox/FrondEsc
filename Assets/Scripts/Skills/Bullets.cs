﻿using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {

	protected Transform trans;
	
	public Skills Skill;
	public Character Owner;
	
	public int ID;
	public float BulletSpeed;
	public int stackSize;
	
	public enum bullTopo { Physic, Magic, Shield };
	public bullTopo bullType;
	private enum goThroughTypes { Walls, Platforms, Enemies };
	private goThroughTypes goThrough;
	private enum bullDir { Up, Down, Left, Right };
	private bullDir bullDirection;
	
	private RaycastHit rayInfo;
	private float rayDist = 0.7f;
	private Vector3 mypos;
	
	protected int groundMask = 1 << 8 ; // Ground
	protected int platformMask = 1 << 9; // Platforms
	protected int enemiesMask = 1 << 11; // Platforms
	
	// Use this for initialization
	void Start ()
	{
		trans = transform;
		Owner = GameObject.Find("Player").GetComponent<Player>();
		trans.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y, Owner.transform.position.z);
		
		switch (Owner.facingDir)
		{
			case (Character.facing.Left) :
			{
				bullDirection = bullDir.Left;
				break;
			}
			case (Character.facing.Right) :
			{
				bullDirection = bullDir.Right;
				break;
			}
			case (Character.facing.Up) :
			{
				bullDirection = bullDir.Up;
				break;
			}
		}
		
		// Rotate the bullet to match direction
		switch (bullDirection)
		{
			case (bullDir.Up) :
			{
				transform.Rotate(new Vector3(0, 0,90f));
				break;
			}
			case (bullDir.Down) :
			{
				transform.Rotate(new Vector3(0, 0,270f));
				break;
			}
			case (bullDir.Left) :
			{
				trans.localScale = new Vector3( -1, 1, 1);
				break;
			}
			case (bullDir.Right) :
			{
				break;
			}
		}
	}
	
	bool Update ()
	{
		if (bullType == bullTopo.Magic || bullType == bullTopo.Physic)
		{
			switch (bullDirection)
			{
				case (bullDir.Up) :
				{
					trans.position = new Vector3(trans.position.x, trans.position.y + (BulletSpeed * 1) ,trans.position.z);
					break;
				}
				case (bullDir.Down) :
				{
					trans.position = new Vector3(trans.position.x, trans.position.y + (BulletSpeed * -1) ,trans.position.z);
					break;
				}
				case (bullDir.Left) :
				{
					trans.position = new Vector3(trans.position.x + (BulletSpeed * -1),trans.position.y ,trans.position.z);
					break;
				}
				case (bullDir.Right) :
				{
					trans.position = new Vector3(trans.position.x + (BulletSpeed * 1), trans.position.y ,trans.position.z);
					break;
				}
				default :
				{
					return false;
				}
			}
			checkCollisionLevel();
			if (Owner as Player)
			{
				checkPlayerHitOpp();
			}
		}
		else if (bullType == bullTopo.Shield)
		{
			trans.position = new Vector3(Owner.transform.position.x, Owner.transform.position.y ,Owner.transform.position.z);
		}
		else if (Owner as Enemy)
		{
		
		}
		return true;
	}
	
	bool checkCollisionLevel()
	{
		if (Physics.Raycast(trans.position, buildVectorBull(), out rayInfo, rayDist , platformMask) || Physics.Raycast(trans.position, buildVectorBull(), out rayInfo, rayDist , groundMask))
		{
			Destroy(gameObject, 0.4f);
			MeshRenderer.Destroy(this, 0.4f);
			Debug.DrawLine (trans.position, rayInfo.point, Color.cyan);
			return true;
		}
		else return false;
	}
	
	bool checkPlayerHitOpp()
	{
		if (Physics.Raycast(trans.position, buildVectorBull(), out rayInfo, rayDist , enemiesMask))
		{
			Destroy(this.gameObject);
			return true;
		}
		else return false;
	}
	
	Vector3 buildVectorBull()
	{
		switch (bullDirection)
		{
			case (bullDir.Up) :
			{
				return (Vector3.up);
			}
			case (bullDir.Down) :
			{
				return (Vector3.down);
			}
			case (bullDir.Left) :
			{
				return (Vector3.left);
			}
			case (bullDir.Right) :
			{
				return (Vector3.right);
			}
			default :
			{
				return (new Vector3 (0f,0f,0f));
			}
		}
	}
}