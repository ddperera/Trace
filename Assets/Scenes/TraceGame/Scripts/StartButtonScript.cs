using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {
    public AudioSource gameBegins;
    public AudioSource loop;
    public AudioSource intro;
    public GameObject audioController;

    private bool waitingForBeat;
    
	// Use this for initialization
	void Start () {
        waitingForBeat = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GoToGame()
    {
        SceneManager.LoadScene("SongSelectScreen");
    }

    IEnumerator WaitForBeat()
    {
        waitingForBeat = true;
        audioController.SendMessage("StopAudio");
        gameBegins.Play();
        yield return new WaitForSeconds(2);// WaitUntil(() => intro.isPlaying);
        GoToGame();
    }

    IEnumerator BlinkArcade()
    {
        yield return null;
    }

	// Switch to the game scene
	public void StartGame()
    {
        if(!waitingForBeat)
        {
            StartCoroutine(WaitForBeat());
        }
    }

	// Switch to the instructions screen
	public void LoadHowToPlay()
    {
        if (!waitingForBeat)
        {
            SceneManager.LoadScene("InstructionScreen");
        }
	}

	// Go back to start menu screen (for instruction screen button)
	public void BackToStartMenu() {
		SceneManager.LoadScene ("StartMenu");
	}
}
