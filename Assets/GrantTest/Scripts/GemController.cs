using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {

    float scroll_speed = 0;

    public void setScrollSpeed(float speed)
    {
        scroll_speed = speed;
    }
	// Use this for initialization
	void Start () {
	
	}

    // is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - scroll_speed * Time.deltaTime,
            gameObject.transform.position.z);

        if (gameObject.transform.position.y <= 0)
        {
            Destroy(gameObject);
        }

    }
}
