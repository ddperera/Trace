using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SongSelectionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Restart game (needs to be changed later to maintain song selection)
	public void GoToGame()
	{
		SceneManager.LoadScene("TraceGame");
	}

	// Back to song selection screen
	public void BackToStartMenu()
	{
		// This needs to be changed once song selection exists
		SceneManager.LoadScene("StartMenu");
	}
}
