using UnityEngine;
using System.Collections;

public class GemBehaviour : MonoBehaviour
{

    public bool ready;
    public bool missed;
    public bool hit;

    private SpriteRenderer renderer;
    public bool isSlide = false;
    private float scrollSpeed = 0;
    private float gemTime = 0;
    private float gemOffset = 0;
    private AudioSource audioBase = null;

    public Sprite startSlideSprite;
    public Sprite midSlideSprite;
    public Sprite endSlideSprite;
    public Sprite missedSprite;
    public Sprite missedMidSlideSprite;
    public Sprite missedEndSlideSprite;
    public Sprite readySprite;
    public float killY;
    public enum GemState
    {
        TAP,
        SLIDE_START,
        SLIDE_MID,
        SLIDE_END
    }
    private GemState myState;

    public GameManagerBehaviour gameMgr;
    public ParticleSystem reticle;

	// Use this for initialization
	IEnumerator Start ()
    {
        hit = false;
        ready = false;
        missed = false;

        renderer = GetComponent<SpriteRenderer>();

        yield return null;

        switch(myState)
        {
            case GemState.SLIDE_START:
                renderer.sprite = startSlideSprite;
                break;
            case GemState.SLIDE_MID:
                renderer.sprite = midSlideSprite;
                transform.Rotate(new Vector3(0, 0, 90f));
                break;
            case GemState.SLIDE_END:
                renderer.sprite = endSlideSprite;
                transform.Rotate(new Vector3(0, 0, -90f));
                break;
            default:
                break;
        }
	}

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            scrollSpeed * (gemTime - audioBase.time) + gemOffset,
            gameObject.transform.position.z);

        if (gameObject.transform.position.y <= killY)
        {
            //Destroy(gameObject);
        }
    }

    public void SetAsReady()
    {
        ready = true;
        //renderer.sprite = readySprite;
    }

    public void SetAsMissed()
    {
        if (!hit)
        {
            ready = false;
            missed = true;
            SetSpriteAsMissed();
        }

    }

    public void MakeBlue()
    {
        renderer.sprite = readySprite;
    }

    private void SetSpriteAsMissed()
    {
        switch (myState)
        {
            case GemState.TAP:
            case GemState.SLIDE_START:
                renderer.sprite = missedSprite;
                break;
            case GemState.SLIDE_MID:
                renderer.sprite = missedMidSlideSprite;
                break;
            case GemState.SLIDE_END:
                renderer.sprite = missedEndSlideSprite;
                break;
            default:
                break;
        }
    }

    public void Fire()
    {
        if(transform.position.y > 2)
        {
            return;
        }

        if(missed)
        {
            return;
        }

        if(ready)
        {
            gameObject.SetActive(false);
            hit = true;
            reticle.Play();
        }
        else
        {
            SetAsMissed();
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

    public void SetState(GemState state)
    {
        myState = state;
        switch(myState)
        {
            case GemState.SLIDE_START:
            case GemState.SLIDE_MID:
            case GemState.SLIDE_END:
                isSlide = true;
                break;
            case GemState.TAP:
                isSlide = false;
                break;
        }
    }
}
