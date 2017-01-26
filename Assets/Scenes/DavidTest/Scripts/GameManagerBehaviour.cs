using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManagerBehaviour : MonoBehaviour {

    public GameObject gemPrefab;
    public List<GameObject> gemList;
	// Use this for initialization
	void Start ()
    {
        gemList = new List<GameObject>();
        StartCoroutine(StartSpawning());
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private IEnumerator StartSpawning()
    {
        GameObject spawnedGem;
        while(true)
        {
            spawnedGem = (GameObject)GameObject.Instantiate(
                gemPrefab,
                gemPrefab.transform.position,
                gemPrefab.transform.rotation);
            gemList.Add(spawnedGem);
            spawnedGem.SetActive(true);
            yield return new WaitForSeconds(.75f);
        }
    }
}
