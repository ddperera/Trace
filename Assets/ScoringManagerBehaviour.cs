using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoringManagerBehaviour : MonoBehaviour {

    public Text scoreTextBox;
    private int score;

	// Use this for initialization
	void Start ()
    {
        score = 0;
        GameObject otherScoringMgr = GameObject.FindWithTag("Scoring");
        if (otherScoringMgr != null && otherScoringMgr != gameObject)
        {
            GameObject.Destroy(otherScoringMgr);
        }
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (scoreTextBox != null)
        {
            scoreTextBox.text = score.ToString("D7");
        }
    }

    public void AddToScore(int toAdd)
    {
        score += toAdd;
    }

    public string GetScoreString()
    {
        return score.ToString("D7");
    }
}
