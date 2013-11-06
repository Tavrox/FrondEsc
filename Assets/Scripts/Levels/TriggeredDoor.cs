using UnityEngine;
using System.Collections;

public class TriggeredDoor : MonoBehaviour {
	
	public OTAnimatingSprite animSprite;
	private bool isLocked = true ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool Unlock()
	{
		if (isLocked == true)
		{
			animSprite.Play("unlock");
			isLocked = false;
			Destroy(collider);
			return true;
		}
		else
		{
			return false;			
		}
	}
}
