using UnityEngine;
using System.Collections;

public class PersistentSongManager : MonoBehaviour {

    private string songName;
    private int songBpm;

	// Use this for initialization
	void Start ()
    {
        GameObject otherSongMgr = GameObject.FindWithTag("SongSelect");
        if(otherSongMgr != null && otherSongMgr != gameObject)
        {
            GameObject.Destroy(otherSongMgr);
        }
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Assign the song name
    public void SetSongTitle(string song)
    {
        songName = song;
    }

    // Assign the song's bpm
    public void SetSongBpm(int bpm)
    {
        songBpm = bpm;
    }


    public string GetSongName()
    {
        return songName;
    }

    public int GetSongBpm()
    {
        return songBpm;
    }
}
