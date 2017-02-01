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
    float fadeMin;

// Use this for initialization
    void Start () {
        background =  gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
        FadeInForFirstTime();
    }


    public void FadeInForFirstTime()
    {
        dspStartTime = AudioSettings.dspTime;
        curVol = 0;
        backgroundFadeTime = 6.0f;
        fadeDir = 1;
        fadeMin = 0f;
    }
    public void FadeIn()
    {
        dspStartTime = AudioSettings.dspTime;
        curVol = background.volume;
        backgroundFadeTime = 3.0f;
        fadeDir = 1;
        fadeMin = .2f;
    }

	public void FadeOut()
    {
        dspStartTime = AudioSettings.dspTime;
        curVol = background.volume;
        backgroundFadeTime = 1.0f;
        fadeDir = -1;
        fadeMin = .2f;
    }

	// Update is called once per frame
	void Update () {
        background.volume = Mathf.Clamp(Convert.ToSingle(fadeDir*(AudioSettings.dspTime - dspStartTime) / backgroundFadeTime + curVol), fadeMin, 1.0f);
    }
}
