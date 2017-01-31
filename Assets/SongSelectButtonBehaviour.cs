using UnityEngine;
using System.Collections;

public class SongSelectButtonBehaviour : MonoBehaviour {

	public SpriteRenderer songImageRenderer;
	public Sprite songImage;

	// Use this for initialization
	void Start () {
		songImageRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If the pointer highlights the button
	public void PointerEnter() {
		songImageRenderer.enabled = true;
		songImageRenderer.sprite = songImage;

	} 

	// If the pointer leaves the button
	public void PointerLeaves() {
		songImageRenderer.enabled = false;
	}
}
