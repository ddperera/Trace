using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {

	public float delayTime = 5;

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds (delayTime);

		SceneManager.LoadScene ("StartMenu");
	}

	// Update is called once per frame
	void Update () {

	}
}