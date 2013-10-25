using UnityEngine;
using System.Collections;

public class ParaOpen : MonoBehaviour {
	
	public GameObject parachute;
	public float paraEffectivness;
	public float deployHeight;
	private bool deployed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hitInfo;
		Ray landingRay = new Ray(transform.position, Vector3.down);
		
		Debug.DrawRay(parachute.transform.position, Vector3.down*deployHeight);
		
		if(!deployed) {
			if (Physics.Raycast(landingRay, out hitInfo, deployHeight)) {
				if(hitInfo.collider.tag == "environment") {
					Debug.Log("TOUCHE");
					deployed = true;
					rigidbody.drag = paraEffectivness;
				}
			}
		}
	}
}
