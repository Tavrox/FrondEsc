using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	
	[SerializeField] private Player player;
	public enum Specs {HealthPotion, PowerUpKnife, PowerUpAxe,PowerUpShield};
	[SerializeField] private Specs Specif;
	public int regenValue = 20;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter(Collider coll)
	{
		player = GameObject.FindWithTag("Player").GetComponent<Player>();
		if(coll.gameObject.CompareTag("Player"))
		{
			switch (Specif)
			{
				case (Specs.HealthPotion) :
				{
					player.RegenHP(regenValue);
					break;
				}
			}
		}
		Destroy(this.gameObject);
	}
	
	private void fade()
	{
		
	}
}
