using UnityEngine;
using System.Collections;

public class GemBehaviour : MonoBehaviour {


    private Rigidbody rb;
    private bool ready;
    private bool missed;
    private bool hit;

    public Material goodMat;
    public Material badMat;
    public Material readyMat;

    public GameManagerBehaviour gameMgr;

	// Use this for initialization
	void Start ()
    {
        hit = false;
        ready = false;
        missed = false;
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, -6f, 0f);
	}

    // Update is called once per frame
    void Update()
    {
        if (GvrController.TouchDown)
        {
            Fire();
        }
    }

    public void SetAsReady()
    {
        ready = true;
        GetComponent<Renderer>().material = readyMat;
    }

    public void SetAsMissed()
    {
        if (!hit)
        {
            ready = false;
            missed = true;
            GetComponent<Renderer>().material = badMat;
        }

    }

    public void Fire()
    {
        if(transform.position.y > 1)
        {
            return;
        }

        if(ready)
        {
            gameObject.SetActive(false);
            //GetComponent<Renderer>().material = goodMat;
            hit = true;
        }
        else
        {
            GetComponent<Renderer>().material = badMat;
        }
    }
}
