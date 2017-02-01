using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FinalScoreTextBoxBehaviour : MonoBehaviour {

    Text textBox;
    ScoringManagerBehaviour scoreMgr;

	// Use this for initialization
	void Start ()
    {
        scoreMgr = GameObject.FindWithTag("Scoring").GetComponent<ScoringManagerBehaviour>();
	    textBox = GetComponent<Text>();
        textBox.text = "Final Score: " + scoreMgr.GetScoreString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
