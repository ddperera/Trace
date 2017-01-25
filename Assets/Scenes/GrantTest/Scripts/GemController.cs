using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {

    float scroll_speed = 0;
    float gemTime = 0;
    float gem_offset = 0;
    AudioSource audioBase = null;

    public void setOffset(float offset)
    {
        gem_offset = offset;
    }
    public void setScrollSpeed(float speed)
    {
        scroll_speed = speed;
    }

    public void setTime(float time)
    {
        gemTime = time;
    }

    public void setAudioSource(AudioSource source)
    {
        audioBase = source;
    }

	// Use this for initialization
	void Start () {
	
	}

    // is called once per frame
    void Update()
    {
        Debug.Log(scroll_speed.ToString());
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            scroll_speed * (gemTime - audioBase.time)+gem_offset,
            gameObject.transform.position.z);

        if (gameObject.transform.position.y <= 0)
        {
            Destroy(gameObject);
        }

    }
}
