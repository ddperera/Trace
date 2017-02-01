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
    public float gemOffset;
    private AudioSource audioBase = null;

    public Sprite startSlideSprite, midSlideSprite, endSlideSprite;
    public Sprite missedSprite, missedMidSlideSprite, missedEndSlideSprite;

    public Sprite pivotTraceSprite, midTraceSprite;
    public Sprite missedPivotTraceSprite, missedMidTraceSprite;

    public Sprite swingSprite;
    public Sprite missedSwingSprite;

    public Sprite readySprite;
    public float killY;
    public enum GemState
    {
        TAP,
        SLIDE_START,
        SLIDE_MID,
        SLIDE_END,
        TRACE_PIVOT,
        TRACE_MID,
        SWING_UP,
        SWING_DOWN,
        SWING_LEFT,
        SWING_RIGHT
    }
    private GemState myState;

    public ScoringManagerBehaviour scoreMgr;
    public ParticleSystem tapReticleEffect;
    public ParticleSystem traceReticleEffect;
    public ParticleSystem swingLeftReticleEffect, swingDownReticleEffect, swingUpReticleEffect, swingRightReticleEffect;

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
                transform.localScale = new Vector3(.4f, .7f, 1f);
                break;
            case GemState.SLIDE_END:
                renderer.sprite = endSlideSprite;
                break;
            case GemState.SWING_LEFT:
                renderer.sprite = swingSprite;
                transform.Rotate(new Vector3(0, 0, 90f));
                break;
            case GemState.SWING_DOWN:
                renderer.sprite = swingSprite;
                transform.Rotate(new Vector3(0, 0, 180f));
                break;
            case GemState.SWING_UP:
                renderer.sprite = swingSprite;
                transform.Rotate(new Vector3(0, 0, 0f));
                break;
            case GemState.SWING_RIGHT:
                renderer.sprite = swingSprite;
                transform.Rotate(new Vector3(0, 0, -90f));
                break;
            case GemState.TRACE_PIVOT:
                renderer.sprite = pivotTraceSprite;
                break;
            case GemState.TRACE_MID:
                renderer.sprite = midTraceSprite;
                transform.localScale = new Vector3(.4f, 1.07f, 1f);
                break;

            default:
                break;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (gemTime != 0f)
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                scrollSpeed * (gemTime - audioBase.time) + gemOffset,
                gameObject.transform.position.z);

            if (gameObject.transform.position.y <= killY)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y - (scrollSpeed * Time.deltaTime),
                transform.position.z);
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
            case GemState.SWING_LEFT:
            case GemState.SWING_DOWN:
            case GemState.SWING_UP:
            case GemState.SWING_RIGHT:
                renderer.sprite = missedSwingSprite;
                break;
            case GemState.TRACE_MID:
                renderer.sprite = missedMidTraceSprite;
                traceReticleEffect.Stop();
                traceReticleEffect.Clear();
                break;
            case GemState.TRACE_PIVOT:
                renderer.sprite = missedPivotTraceSprite;
                traceReticleEffect.Stop();
                traceReticleEffect.Clear();
                break;
            default:
                break;
        }
    }

    public void Fire()
    {
        if(missed)
        {
            return;
        }

        if(ready)
        {
            gameObject.SetActive(false);
            hit = true;
            
            switch(myState)
            {
                case GemState.TAP:
                case GemState.SLIDE_START:
                case GemState.SLIDE_MID:
                case GemState.SLIDE_END:
                    tapReticleEffect.Play();
                    break;
                case GemState.SWING_LEFT:
                    swingLeftReticleEffect.Play();
                    break;
                case GemState.SWING_DOWN:
                    swingDownReticleEffect.Play();
                    break;
                case GemState.SWING_UP:
                    swingUpReticleEffect.Play();
                    break;
                case GemState.SWING_RIGHT:
                    swingRightReticleEffect.Play();
                    break;
                case GemState.TRACE_PIVOT:
                    traceReticleEffect.Play();
                    break;
            }

            AddToScore();
            Destroy(gameObject);
        }
        else
        {
            SetAsMissed();
        }
    }

    private void AddToScore()
    {
        switch(myState)
        {
            case GemState.TAP:    
            case GemState.SLIDE_START:
            case GemState.SLIDE_END:
            case GemState.TRACE_PIVOT:
                scoreMgr.AddToScore(100);
                break;
            case GemState.TRACE_MID:
            case GemState.SLIDE_MID:
                scoreMgr.AddToScore(25);
                break;
            case GemState.SWING_LEFT:
            case GemState.SWING_DOWN:
            case GemState.SWING_UP:
            case GemState.SWING_RIGHT:
                scoreMgr.AddToScore(125);
                break;
            default:
                break;
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

    public GemState GetState()
    {
        return myState;
    }
}
