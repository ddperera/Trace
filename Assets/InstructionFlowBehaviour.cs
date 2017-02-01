using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionFlowBehaviour : MonoBehaviour {

	// Keep track of which phase of the instructions you're in
	private string instructionPhase;

	// Tutorial images
	public SpriteRenderer firstImage;
	public SpriteRenderer centerImage;
	public SpriteRenderer leftImage;
	public SpriteRenderer rightImage;
	public SpriteRenderer lastImage;

	// Buttons
	public Button nextButton;
	public Button tryItButton;
	public Button gotItButton;
	public Button finishedButton;

	// Use this for initialization
	void Start () {
		firstImage.enabled = true;
		nextButton.gameObject.SetActive (true);

		centerImage.enabled = false;
		leftImage.enabled = false;
		rightImage.enabled = false;
		lastImage.enabled = false;
		tryItButton.gameObject.SetActive (false);
		gotItButton.gameObject.SetActive (false);
		finishedButton.gameObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Button function - advance to next screen
	public void AdvanceToFirstInstruction() {
		// Make the first (center) instructions show up 
		instructionPhase = "center";
		firstImage.enabled = false;
		nextButton.gameObject.SetActive (false);
		centerImage.enabled = true;
		tryItButton.gameObject.SetActive (true);
	}

	// Button function - finish an exercise 
	public void DoneWithExercise() {
		// If you're in center phase, go to left phase image
		if (instructionPhase.Equals("center")) {
			// TO DO: END THE EXERCISE
			gotItButton.gameObject.SetActive (false);
			leftImage.enabled = true;
			tryItButton.gameObject.SetActive (true);
			instructionPhase = "left";
		}
			
		// If you're in left phase, go to right phase image
		else if (instructionPhase.Equals("left")) {
			// TO DO: END THE EXERCISE
			gotItButton.gameObject.SetActive (false);
			rightImage.enabled = true;
			tryItButton.gameObject.SetActive (true);
			instructionPhase = "right";
		}
				
		// If you're in right phase, go to last instruction screen
		else if (instructionPhase.Equals("right")) {
			// TO DO: END THE EXERCISE
			gotItButton.gameObject.SetActive (false);
			lastImage.enabled = true;
			finishedButton.gameObject.SetActive (true);
		}

		else {
			Debug.Log("Something in instructions broke.");
		}
	}

	// Button function - start exercise 
	public void StartExercise() {
		// Center phase
		if (instructionPhase.Equals("center")) {
			centerImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
			// TO DO: START THE EXERCISE
		}

		// Left phase
		else if (instructionPhase.Equals("left")) {
			leftImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
			// TO DO: START THE EXERCISE
		}

		// Right phase
		else if (instructionPhase.Equals("right")) {
			rightImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
			// TO DO: START THE EXERCISE
		}

		else {
			Debug.Log("Something in instructions broke.");
		}
	}

	// Button function - done with the tutorial (for now goes to start screen)
	public void EndTutorial() {
		SceneManager.LoadScene("StartMenu");
	}
}
