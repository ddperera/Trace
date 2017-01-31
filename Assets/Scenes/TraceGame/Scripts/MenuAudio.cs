using UnityEngine;
using System.Collections;

public class MenuAudio : MonoBehaviour {
    public AudioSource intro;
    public AudioSource loop;
    bool possible = true;

    
	// Use this for initialization
	void Start () {
		double startTime = AudioSettings.dspTime;
		intro.PlayScheduled (startTime);
		loop.PlayScheduled (startTime + 1.5);
//        StartCoroutine(LoopIntro());
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
