﻿using UnityEngine;
using System;

// The code example shows how to implement a metronome that procedurally generates
// the click sounds via the OnAudioFilterRead callback. While the game is paused or the suspended,
// this time will not be updated and sounds playing will be paused. Therefore developers of music 
// scheduling routines do not have to do any rescheduling after the app is unpaused 

using UnityStandardAssets.ImageEffects;

public class BloomPulseBehaviour : MonoBehaviour
{
	public GameObject maincam;
	private BloomOptimized glow;
	private bool glowIncreasing;


    public AudioSource songToPulseTo;
    public int sampleRate;
    public int bpm;

    int lastSamplePosInTrack = 0;
    int curSamplePosInTrack = 0;
    int beatLength; 

    // Use this for initialization
    void Start () {
		glow = maincam.GetComponent<BloomOptimized>();
		glowIncreasing = true;
        beatLength = Convert.ToInt32(sampleRate / (bpm / 60.0f));
    }

	// Update is called once per frame
	void Update () {
		if (glowIncreasing) {
			glow.intensity += 0.05f;
		} 
		else {
			glow.intensity -= 0.05f;
		}

		if (glow.intensity >= 2.5f) {
			glowIncreasing = false;
		}

		if (glow.intensity <= 0.0f) {
			glowIncreasing = true;
		}

		//Debug.Log (glow.intensity);
	}
}