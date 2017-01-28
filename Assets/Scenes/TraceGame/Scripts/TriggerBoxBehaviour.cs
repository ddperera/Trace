using UnityEngine;
using System.Collections;

public class TriggerBoxBehaviour : MonoBehaviour {

    public GameObject reticle;
    private float reticleX;
    private Plane myPlane;
    private float rayDistance;
    private Ray reticleRay;
	// Use this for initialization
	void Start ()
    {
        myPlane = new Plane(transform.position.normalized * -1, transform.position.magnitude);
        reticleRay.origin = Vector3.up;

    }
	
	// Update is called once per frame
	void Update ()
    {
        reticleRay.direction = reticle.transform.position;
        myPlane.Raycast(reticleRay, out rayDistance);//Vector3.ProjectOnPlane(reticle.transform.position, Vector3.back).x;
        reticleX = reticleRay.GetPoint(rayDistance).x;
        transform.position = new Vector3(reticleX, transform.position.y, transform.position.z);
	}

    private void OnTriggerEnter(Collider other)
    {
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


