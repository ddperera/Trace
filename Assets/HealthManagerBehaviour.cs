using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthManagerBehaviour : MonoBehaviour
{

    public SpriteRenderer damageBorder;
    private int health;
    public int maxHealth;
    public int lowHealthThreshold;
    private bool isPulsing;

	// Use this for initialization
	void Start ()
    {
        damageBorder.color = Color.clear;
        isPulsing = false;
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(health);

        if (health<1)
        {
            SceneManager.LoadScene("LosingScreen");
        }

	    if(health < lowHealthThreshold && !isPulsing)
        {
            StartCoroutine(Pulse());
        }
	}

    public void AddHealth(int h)
    {
        health = Mathf.Min(health + h, maxHealth);
    }

    public void TakeDamage(int h)
    {
        health -= h;
    }

    private IEnumerator Pulse()
    {
        isPulsing = true;
        float pulseDuration = health;
        Color newColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        for (float t = 0; t < .5f; t += Time.deltaTime/(pulseDuration / 2.0f) )
        {
            newColor.a = t;
            damageBorder.color = newColor;
            yield return null;
        }
        for (float t = .5f; t > 0; t -= Time.deltaTime / (pulseDuration / 2.0f))
        {
            newColor.a = t;
            damageBorder.color = newColor;
            yield return null;
        }
        damageBorder.color = Color.clear;
        isPulsing = false;
    }
}
