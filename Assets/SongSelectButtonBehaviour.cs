using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SongSelectButtonBehaviour : MonoBehaviour {

	public SpriteRenderer songImageRenderer;
	public Sprite songImage;
	public string songDifficultySetting;
	public Text songDifficultyDisplay;

	// Use this for initialization
	void Start () {
		songImageRenderer.enabled = false;
		songDifficultyDisplay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the pointer highlights the button
	public void PointerEnter() {
		songImageRenderer.enabled = true;
		songImageRenderer.sprite = songImage;
		songDifficultyDisplay.enabled = true;
		songDifficultyDisplay.text = songDifficultySetting;
	} 

	// If the pointer leaves the button
	public void PointerLeaves() {
		songImageRenderer.enabled = false;
		songDifficultyDisplay.enabled = false;
	}
}
