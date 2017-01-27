﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Switch to the game scene
	public void StartGame() {
		SceneManager.LoadScene ("TraceGame");
	}

	// Switch to the instructions screen
	public void LoadHowToPlay() {
		SceneManager.LoadScene ("InstructionScreen");
	}
}
