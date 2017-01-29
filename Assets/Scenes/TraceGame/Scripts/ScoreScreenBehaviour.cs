using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScoreScreenBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Restart game (needs to be changed later to maintain song selection)
	public void BackToGame()
	{
		SceneManager.LoadScene("TraceGame");
	}

	// Back to song selection screen
	public void BackToSongSelection()
	{
		// This needs to be changed once song selection exists
		SceneManager.LoadScene("StartMenu");
	}
}
