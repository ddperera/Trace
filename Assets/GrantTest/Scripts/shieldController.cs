using UnityEngine;
using System.Collections;

public class shieldController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.transform.position = new Vector3(0 , 1, 2 );
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
