﻿using UnityEngine;
using System.Collections;

public class TriggerBoxBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hi");
        GameObject go = other.gameObject;
        if(go.tag == "Gem")
        {
            GemBehaviour gemScript = go.GetComponent<GemBehaviour>();
            gemScript.SetAsReady();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.tag == "Gem")
        {
            GemBehaviour gemScript = go.GetComponent<GemBehaviour>();
            gemScript.SetAsMissed();
        }
    }
}


