using UnityEngine;
using System.Collections;

public class HealthManagerBehaviour : MonoBehaviour
{

    public SpriteRenderer damageBorder;
    private int health;
    public int maxHealth;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void AddHealth(int h)
    {
        health = Mathf.Min(health + h, maxHealth);
    }

    public void TakeDamage(int h)
    {
        health -= h;
    }
}
