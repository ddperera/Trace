using UnityEngine;
using System.Collections;

public class MenuAudio : MonoBehaviour {
    public AudioSource intro;
    public AudioSource loop;
    bool possible = true;
    
	// Use this for initialization
	void Start () {
        StartCoroutine(LoopIntro());
	}

    public IEnumerator LoopIntro()
    {
        yield return new WaitUntil(() => possible && !intro.isPlaying);
        loop.Play();
    }

    public void StopAudio()
    {
        possible = false;
        loop.Stop();
        intro.Stop();
    }
}
