using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CautionArrowManager : MonoBehaviour {

    private List<KeyValuePair<float, GameManagerBehaviour.Track>> trackStartTimes;

    public SpriteRenderer leftLeftArrow, leftRightArrow, rightLeftArrow, rightRightArrow;

    private float pulseDuration = 1.0f;
    private int numPulses = 4;

    private GvrAudioSource tapAudioSource, traceAudioSource, swingAudioSource;

    public AudioSource audioSource;
	// Use this for initialization
	void Start ()
    {
        leftLeftArrow.color = Color.clear;
        leftRightArrow.color = Color.clear;
        rightLeftArrow.color = Color.clear;
        rightRightArrow.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(trackStartTimes.Count == 0)
        {
            return;
        }

	    if(trackStartTimes[0].Key - audioSource.time < pulseDuration * numPulses )
        {
            switch(trackStartTimes[0].Value)
            {
                case GameManagerBehaviour.Track.TAP:
                    StartCoroutine(WarnMiddleTrack());
                    break;
                case GameManagerBehaviour.Track.SWING:
                    StartCoroutine(WarnLeftTrack());
                    break;
                case GameManagerBehaviour.Track.TRACE:
                    StartCoroutine(WarnRightTrack());
                    break;
            }
            trackStartTimes.RemoveAt(0);
        }
	}

    IEnumerator WarnLeftTrack()
    {
        int pulseCounter = 0;
        float alpha = 0f;
        Color targetColor = new Color(1.0f, 1.0f, 1.0f, alpha);
        while (pulseCounter < numPulses)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(0, 1, t);
                targetColor.a = alpha;

                leftLeftArrow.color = targetColor;
                rightLeftArrow.color = targetColor;

                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(1, 0, t);
                targetColor.a = alpha;

                leftLeftArrow.color = targetColor;
                rightLeftArrow.color = targetColor;

                yield return null;
            }

            pulseCounter++;
        }

        leftLeftArrow.color = Color.clear;
        rightLeftArrow.color = Color.clear;

        tapAudioSource.volume = 0.8f;
        traceAudioSource.volume = 0.8f;
        swingAudioSource.volume = 1.1f;

        yield return null;
    }

    IEnumerator WarnMiddleTrack()
    {
        int pulseCounter = 0;
        float alpha = 0f;
        Color targetColor = new Color(1.0f, 1.0f, 1.0f, alpha);
        while (pulseCounter < numPulses)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(0, 1, t);
                targetColor.a = alpha;

                leftRightArrow.color = targetColor;
                rightLeftArrow.color = targetColor;

                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(1, 0, t);
                targetColor.a = alpha;

                leftRightArrow.color = targetColor;
                rightLeftArrow.color = targetColor;

                yield return null;
            }

            pulseCounter++;
        }

        leftRightArrow.color = Color.clear;
        rightLeftArrow.color = Color.clear;

        tapAudioSource.volume = 1.1f;
        traceAudioSource.volume = 0.8f;
        swingAudioSource.volume = 0.8f;
        yield return null;
    }

    IEnumerator WarnRightTrack()
    {
        int pulseCounter = 0;
        float alpha = 0f;
        Color targetColor = new Color(1.0f, 1.0f, 1.0f, alpha);
        while (pulseCounter < numPulses)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(0, 1, t);
                targetColor.a = alpha;

                leftRightArrow.color = targetColor;
                rightRightArrow.color = targetColor;

                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / (pulseDuration / 2.0f))
            {
                alpha = Mathf.Lerp(1, 0, t);
                targetColor.a = alpha;

                leftRightArrow.color = targetColor;
                rightRightArrow.color = targetColor;

                yield return null;
            }

            pulseCounter++;
        }

        leftRightArrow.color = Color.clear;
        rightRightArrow.color = Color.clear;

        tapAudioSource.volume = 0.8f;
        traceAudioSource.volume = 1.1f;
        swingAudioSource.volume = 0.8f;
        yield return null;
    }

    public void LoadTimes(List<KeyValuePair<float, GameManagerBehaviour.Track>> list, GvrAudioSource tap, GvrAudioSource trace, GvrAudioSource swing)
    {
        trackStartTimes = new List<KeyValuePair<float, GameManagerBehaviour.Track>>(list);
        tapAudioSource = tap;
        traceAudioSource = trace;
        swingAudioSource = swing;
    }
}
