using UnityEngine;
using System.Collections;

public class MissVolumeBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Gem")
        {
            GemBehaviour gemScript = go.GetComponent<GemBehaviour>();
            gemScript.SetAsMissed();
        }
    }
}
