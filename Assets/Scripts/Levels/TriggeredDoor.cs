using UnityEngine;
using System.Collections;

public class TriggeredDoor : MonoBehaviour {
	
	public OTAnimatingSprite animSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Unlock()
	{
		animSprite.Play("unlock");
		Destroy(collider);
//		collider.transform.localScale = new Vector3(0.2f,0.2f,1f);
		
	}
}
