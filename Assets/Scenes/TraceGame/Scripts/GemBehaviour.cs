using UnityEngine;
using System.Collections;

public class GemBehaviour : MonoBehaviour
{

    private bool ready;
    private bool missed;
    private bool hit;

    private Renderer renderer;
    private bool isSlide = false;
    private float scrollSpeed = 0;
    private float gemTime = 0;
    private float gemOffset = 0;
    private AudioSource audioBase = null;

    public Material slideMat;
    public Material missedMat;
    public Material readyMat;
    public float killY;

    public GameManagerBehaviour gameMgr;

	// Use this for initialization
	IEnumerator Start ()
    {
        hit = false;
        ready = false;
        missed = false;

        renderer = GetComponent<Renderer>();

        yield return null;

        if(isSlide)
        {
            renderer.material = slideMat;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (isSlide)
        {
            if (GvrController.IsTouching)
            {
                Fire();
            }
        }
        else
        {
            if (GvrController.TouchDown)
            {
                Fire();
            }
        }

        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            scrollSpeed * (gemTime - audioBase.time) + gemOffset,
            gameObject.transform.position.z);

        if (gameObject.transform.position.y <= killY)
        {
            Destroy(gameObject);
        }
    }

    public void SetAsReady()
    {
        ready = true;
        renderer.material = readyMat;
    }

    public void SetAsMissed()
    {
        if (!hit)
        {
            ready = false;
            missed = true;
            renderer.material = missedMat;
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
            hit = true;
        }
        else
        {
            renderer.material = missedMat;
        }
    }

    public void SetOffset(float offset)
    {
        gemOffset = offset;
    }
    public void SetScrollSpeed(float speed)
    {
        scrollSpeed = speed;
    }

    public void SetTime(float time)
    {
        gemTime = time;
    }

    public void SetAudioSource(AudioSource source)
    {
        audioBase = source;
    }

    public void SetSlide(bool slide)
    {
        isSlide = slide;
    }
}
