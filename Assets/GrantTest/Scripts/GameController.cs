using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public GameObject gemPrefab;
    public GameObject map;
    public float scroll_speed = .00001f;
    public float spawnTime = 3f;
    Queue<GameObject> gems = new Queue<GameObject>();
    float last_spawn_time = 0;

    float angle = 0;

    // Use this for initialization
    void Start () {
    }
	
    void SpawnGem(float height, float angle)
    {
        GameObject gem = Instantiate(
                gemPrefab,
                new Vector3(2*Mathf.Sin(angle), height, 2*Mathf.Cos(angle)),
                Quaternion.Euler(new Vector3(0, angle*180/Mathf.PI, 90))
            ) as GameObject;
        gem.SendMessage("setScrollSpeed", scroll_speed);
    }

	// Update is called once per frame
	void Update () {
        if (Time.fixedTime - last_spawn_time > 1)
        {
            SpawnGem(10, angle);
            last_spawn_time = Time.fixedTime;
            angle += 10*Mathf.PI/180;
        }
    }

    void FixedUpdate()
    {
        
    }

    
}
