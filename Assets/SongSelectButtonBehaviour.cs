using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SongSelectButtonBehaviour : MonoBehaviour {

	public SpriteRenderer songImageRenderer;
	public Sprite songImage;
	public string songDifficultySetting;
	public Text songDifficultyDisplay;

    public AudioSource audioSource;
    public AudioSource background;

    public string songName;
    public float startTime;
    public float endTime;

    float fadeInTime = 1.0f;

    double dspStartTime;
    float fadePlace = 0.0f;
    double curTime = 0.0;

    bool playing = false;

    // Use this for initialization
    void Start () {
		songImageRenderer.enabled = false;
		songDifficultyDisplay.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (playing)
        {
            audioSource.volume = Mathf.Clamp(Convert.ToSingle((AudioSettings.dspTime - dspStartTime)/fadeInTime), 0.0f, 1.0f);
        }
    }

	// If the pointer highlights the button
	public void PointerEnter() {
		songImageRenderer.enabled = true;
		songImageRenderer.sprite = songImage;
		songDifficultyDisplay.enabled = true;
		songDifficultyDisplay.text = songDifficultySetting;

        audioSource.clip = (AudioClip)Resources.Load("AudioFiles/" + songName, typeof(AudioClip));
        audioSource.time = startTime;
        audioSource.volume = 0.0f;
        audioSource.Play();
        playing = true;
        background.SendMessage("FadeOut");
        dspStartTime = AudioSettings.dspTime;

    } 

	// If the pointer leaves the button
	public void PointerLeaves() {
		songImageRenderer.enabled = false;
		songDifficultyDisplay.enabled = false;
        audioSource.Stop();
        background.SendMessage("FadeIn");
        
        playing = false;
	}
}
