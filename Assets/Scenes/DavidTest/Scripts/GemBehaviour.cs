using UnityEngine;
using System.Collections;

public class GemBehaviour : MonoBehaviour
{

    private bool ready;
    private bool missed;
    private bool hit;

    private float scrollSpeed = 0;
    private float gemTime = 0;
    private float gemOffset = 0;
    private AudioSource audioBase = null;

    public Material goodMat;
    public Material badMat;
    public Material readyMat;
    public float killY;

    public GameManagerBehaviour gameMgr;

	// Use this for initialization
	void Start ()
    {
        hit = false;
        ready = false;
        missed = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (GvrController.TouchDown)
        {
            Fire();
        }

        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            scrollSpeed * (gemTime - audioBase.time) + gemOffset,
            gameObject.transform.position.z);

        if (gameObject.transform.position.y <= 0)
        {
            Destroy(gameObject);
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

    public void setOffset(float offset)
    {
        gemOffset = offset;
    }
    public void setScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    public void setTime(float time)
    {
        gemTime = time;
    }

    public void setAudioSource(AudioSource source)
    {
        audioBase = source;
    }
}
