using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

	private bool grounded =true;
	[HideInInspector] public Transform thisTransform;
	[SerializeField] private float gravityY;
	[SerializeField] private Vector3 vectorMove;
	//RaycastHit hitInfo;
	//Ray landingRay;	
	//public float deployHeight;
	
	public virtual void Awake()
	{
		thisTransform = transform;	
	}
	
	public virtual void Start () 
	{
		StartCoroutine(StartGravity());
	}
	
	IEnumerator StartGravity()
	{
		// wait for things to settle before applying gravity
		yield return new WaitForSeconds(0.1f);
		gravityY = 52f;
	}
	
	// Update is called once per frame
	void Update () 
	{		
		if(grounded == false)
		{
			vectorMove.y -= gravityY * Time.deltaTime;
		}
		//landingRay = new Ray(thisTransform.position, Vector3.down);
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.gameObject.CompareTag("Ground")) 
		{
			grounded = true;
		}
		if(other.gameObject.CompareTag("Platforms")) 
		{
			grounded = true;
		}
	}
}
