using UnityEngine;
using System.Collections;

public class Bullets : MonoBehaviour {
	
	protected Transform trans;
	
	public float bullMove = 3f;
	public OTSprite sprite;
	public OTSpriteAtlasCocos2D atlas;
	
	public Vector3 dir;
	
//	[HideInInspector] public Vector3 dir = new Vector3(trans.position.x;

	// Use this for initialization
	void Start () 
	{
		trans = transform;
		dir = new Vector3(trans.position.x, trans.position.y, trans.position.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		trans.position = new Vector3(trans.position.x + 0.1f, trans.position.y ,trans.position.z);
	}
	
	public OTSprite newBullet()
	{
		sprite = OT.CreateObject(OTObjectType.Sprite).GetComponent<OTSprite>();
		sprite.size = new Vector2(10,10);
		sprite.depth = -100;
		sprite.transparent = true;
		return sprite;
	}
}
