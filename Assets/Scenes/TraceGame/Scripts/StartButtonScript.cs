using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {
    public AudioSource gameBegins;
    public AudioSource loop;
    public AudioSource intro;
    public GameObject audioController;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("TraceGame");
    }

    IEnumerator WaitForBeat()
    {
        audioController.SendMessage("StopAudio");
        gameBegins.Play(); // ;
        yield return new WaitForSeconds(2);// WaitUntil(() => intro.isPlaying);
        GoToGame();
    }

	// Switch to the game scene
	public void StartGame()
    {
        StartCoroutine(WaitForBeat());
	}

	// Switch to the instructions screen
	public void LoadHowToPlay() {
		SceneManager.LoadScene ("InstructionScreen");
	}

	// Go back to start menu screen (for instruction screen button)
	public void BackToStartMenu() {
		Debug.Log ("WHOOPA");
		SceneManager.LoadScene ("StartMenu");
	}
}
