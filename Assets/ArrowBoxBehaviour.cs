using UnityEngine;
using System.Collections;

public class ArrowBoxBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Gem")
        {
            GemBehaviour gemScript = go.GetComponent<GemBehaviour>();
            gemScript.SetAsReady();
        }
    }
}
