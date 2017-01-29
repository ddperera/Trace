using UnityEngine;
using System.Collections;

public class TriggerBoxBehaviour : MonoBehaviour {

    public GameObject reticle;
    public float movementBound;

    private float localLeftXBound, localRightXBound;
    private float reticleX, reticleZ;
    private Plane myPlane;
    private float rayDistance;
    private Ray reticleRay;
    public Transform initialTrans;
	// Use this for initialization
	void Start ()
    {
        localLeftXBound = transform.localPosition.x - movementBound;
        localRightXBound = transform.localPosition.x + movementBound;
        myPlane = new Plane(transform.position.normalized * -1, transform.position.magnitude);
        reticleRay.origin = Vector3.zero;// Vector3.up;
    }
	
	// Update is called once per frame
	void Update ()
    {
        reticleRay.direction = reticle.transform.position;
        myPlane.Raycast(reticleRay, out rayDistance);
        reticleX = reticleRay.GetPoint(rayDistance).x;
        reticleZ = reticleRay.GetPoint(rayDistance).z;
        Vector3 XZImpactVec = new Vector3(reticleX, transform.position.y, reticleZ);
        Vector3 localTargetVec = initialTrans.InverseTransformPoint(new Vector3(reticleX, transform.position.y, reticleZ));
        localTargetVec.x = Mathf.Clamp(localTargetVec.x, -movementBound, movementBound);
        localTargetVec.y = 0;
        localTargetVec.z = 0; //myPlane.distance;
        transform.position = initialTrans.TransformPoint(localTargetVec);
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
}


