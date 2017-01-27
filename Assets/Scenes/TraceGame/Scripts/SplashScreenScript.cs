using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour {

    public float delayTime = 3.5f;

	public float fadeSpeed = 0.3f;

	public Texture fadeOutTexture;
	private int drawDepth = -1000;
	public float alpha = .99f;
	public float fadeDir = -1.0f;
	private Color alphaColor;

	// Use this for initialization
	IEnumerator Start () {
		FadeIn();
		yield return new WaitForSeconds (3.5f);
		FadeOut();
	}

	// Use this for initialization
	void OnGUI() 
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);
		alphaColor = GUI.color;
		alphaColor.a = alpha;
		GUI.color = alphaColor;
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);

		if (alpha == 1)
		{
			//gameObject.tag = "Camera";
			SceneManager.LoadScene ("StartMenu");
		}
	}

	public void FadeIn()
	{
		fadeDir = -1;
	}

	public void FadeOut()
	{
		fadeDir = 1;
	}
		
}