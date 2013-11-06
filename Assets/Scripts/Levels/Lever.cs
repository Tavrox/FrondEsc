using UnityEngine;
using System.Collections;

public class Lever : MonoBehaviour {
	
	public OTAnimatingSprite animSprite;
	public TriggeredDoor linkedDoor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
	
		if (other.gameObject.CompareTag("Bullets"))
		{
			triggerLever();
		}
		
	}
	
	void triggerLever()
	{
//		animSprite.Play("trigger");
//		ScaleMode = new Vector3(-1,0,0);
		linkedDoor.Unlock();
		
	}
}
