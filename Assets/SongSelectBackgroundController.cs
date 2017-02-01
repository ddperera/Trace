using UnityEngine;
using System.Collections;
using System;

public class SongSelectBackgroundController : MonoBehaviour {
    
    float backgroundFadeTime;
    bool backgroundFading = false;
    AudioSource background;

    double dspStartTime;

    float curVol;
    int fadeDir;
// Use this for initialization
    void Start () {
        background =  gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        FadeIn();
    }

    public void FadeIn()
    {
        dspStartTime = AudioSettings.dspTime;
        curVol = background.volume;
        backgroundFadeTime = 3.0f;
        fadeDir = 1;
    }
	public void FadeOut()
    {
        dspStartTime = AudioSettings.dspTime;
        curVol = background.volume;
        backgroundFadeTime = 1.0f;
        fadeDir = -1;
    }
	// Update is called once per frame
	void Update () {
        background.volume = Mathf.Clamp(Convert.ToSingle(fadeDir*(AudioSettings.dspTime - dspStartTime) / backgroundFadeTime + curVol), 0.2f, 1.0f);
    }
}
