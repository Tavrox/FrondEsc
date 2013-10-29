using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
	
	public GUIText talkTextGUI;	//Text object
	public string[] talkLines;	//Array containing all the sentences of the dialog
	public int textScrollSpeed;
	public bool lockPlayer = true; //Stops the player while displaying the dialog if true
	
	private bool talking;	//The dialog is displayed
	private bool textIsScrolling;	//The text is currently scrolling
	private int currentLine;	//Current line read
	private int startLine;
	private string displayText; //The text displayed
	
	[HideInInspector] public Player player; //The player to lock
	
	// Update is called once per frame
	void Update () {
//		if(null) FindObjectOfType(typeof(Dialog));
		if(talking){
			player = GameObject.FindWithTag("Player").GetComponent<Player>();
			//player = GameObject.Find("Pop1").GetComponent<Player>();	//Get the player
			if(lockPlayer) player.enabled = false;	//Lock the player if lockPlayer is true
			
			if(Input.GetKeyUp(KeyCode.C)) { //Dialog interaction button detection
				if(textIsScrolling){	//If the text is scrolling
					StopCoroutine("startScrolling"); //Stop the scroll effect					
	              	talkTextGUI.text = talkLines[currentLine];	//Display the whole line
	              	textIsScrolling = false;	//The text is not scrolling anymore
	            }
				else {
					if(currentLine < talkLines.Length - 1){ //If the text is not scrolling and still lines to read
						currentLine++;	//Go to next line
						//talkTextGUI.text = talkLines[currentLine]; //STATIC
						StartCoroutine("startScrolling");	//Start scroll effect
					}
					else{	//If there is no more lines to read
						currentLine = 0;	//reset the current line number (for next reading time)
						talkTextGUI.text = "";	//Empty the text object
						talking = false; 	//Not talking anymore
						if(lockPlayer) player.enabled = true;	//Unlock the playe if needed
					}
	          	}
			}
		}
	}
	
	//Call to the dialog from another class
	public void startDialog(bool locking) {
		talking=true;	//Activtate talking state
		currentLine = 0;
		StartCoroutine("startScrolling");	//Start displaying text
		lockPlayer = locking;	//Set if player has to be locked or not
	}
	
	//Activate the dialog when the player is in the collision box
	void OnTriggerEnter(Collider col) {
		if(col.gameObject.CompareTag("Player")) {
			talking=true;
			currentLine = 0;
			StartCoroutine("startScrolling");
			//col.animation.CrossFade("idle");
		}
	}
	
	//Scrolling Coroutine
	IEnumerator startScrolling() {
		textIsScrolling = true;
		startLine = currentLine;
		displayText = "";
		
		//Display each letter one by one
		for(int i = 0; i < talkLines[currentLine].Length; i++){
			if(textIsScrolling && currentLine == startLine){
				
				yield return new WaitForSeconds((float) (1f/ textScrollSpeed)); //Waiting textScrollSpeed between each update
				displayText += talkLines[currentLine][i];
				talkTextGUI.text = displayText;
			}
		}
		
		textIsScrolling = false; //Text is not scrolling anymore
	}
}