using UnityEngine;
using System.Collections;

public class GemBehaviour : MonoBehaviour
{

    private bool ready;
    private bool missed;
    private bool hit;

    private SpriteRenderer renderer;
    private bool isSlide = false;
    private float scrollSpeed = 0;
    private float gemTime = 0;
    private float gemOffset = 0;
    private AudioSource audioBase = null;

    public Sprite slideSprite;
    public Sprite missedSprite;
    public Sprite missedSlideSprite;
    public Sprite readySprite;
    public float killY;

    public GameManagerBehaviour gameMgr;

	// Use this for initialization
	IEnumerator Start ()
    {
        hit = false;
        ready = false;
        missed = false;

        renderer = GetComponent<SpriteRenderer>();

        yield return null;

        if(isSlide)
        {
            renderer.sprite = slideSprite;
            transform.Rotate(new Vector3(0, 0, 90f));
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
        renderer.sprite = readySprite;
    }

    public void SetAsMissed()
    {
        if (!hit)
        {
            ready = false;
            missed = true;
            if(isSlide)
            {
                renderer.sprite = missedSlideSprite;
            }
            else
            {
                renderer.sprite = missedSprite;
            }
            
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
            renderer.sprite = missedSprite;
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
