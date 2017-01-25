using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;
using System;

public class GameController : MonoBehaviour {
    public GameObject gemPrefab;
    public GameObject map;
    public GameObject gemSlidePrefab;
    public AudioSource audioSource;

    //Spawn parameters
    public float scroll_speed;
    public float spawnTime;
    float last_spawn_time = 0;
    float angle = 0;
    float slideDiff = .05f;
    float leveloffset = 3;

    // Use this for initialization
    void Start () {
        LoadLevel("yee");
        audioSource.Play(0);
        Debug.Log(audioSource.GetType());
    }

    private bool LoadLevel(string levelName)
    {
        string fileName = "Assets/Scenes/GrantTest/Audio/" + levelName + ".txt";
        try
        {
            string line;
            StreamReader theReader = new StreamReader(fileName, Encoding.Default);

            using (theReader)
            {
                line = theReader.ReadLine();
                if (line != null)
                {
                    // While there's lines left in the text file, do this:
                    float slideTime = 0;
                    float slideStartAngle = 0;
                    do
                    {
                        // Do whatever you need to do with the text line, it's a string now
                        // In this example, I split it into arguments based on comma
                        // deliniators, then send that array to DoStuff()
                        string[] gems = line.Split('\t');
                        float time = float.Parse(gems[0]);
                        string[] entries = gems[1].Split(' ');

                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        // FOR TESTING ONLY!!!!!
                        if (entries[0] == "click")
                        {
                            entries[1] = "0";
                        }
                        else
                        {
                            entries[2] = "0";
                        }
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////
                        ///////////////////////////////////////////////////////////////////////


                        if (entries[0] == "click")
                        {
                            SpawnGem(time, float.Parse(entries[1]));
                        }
                        if (entries[0] == "slide")
                        {
                            if (entries[1] == "start")
                            {
                                slideTime = time;
                                slideStartAngle = float.Parse(entries[2]);

                                SpawnGem(time, float.Parse(entries[2]));
                            }
                            else
                            {
                                float dt = (time - slideTime);
                                int num_to_spawn = (int)Math.Floor(dt / slideDiff);
                                dt = dt / num_to_spawn;
                                float dAngle = (float.Parse(entries[2]) - slideStartAngle)/num_to_spawn;
                                for (int i = 0; i < num_to_spawn; i++)
                                {
                                    SpawnSlideGem(slideTime + dt * (i + 1), slideStartAngle + dAngle * (i + 1));
                                }

                                slideTime = time;
                                slideStartAngle = float.Parse(entries[2]);
                            }
                        }
                        //if (entries[0] == "Click")
                        //{
                        //    Debug.Log(entries[1]);
                        //}
                        //if (entries[0] == "Slide")
                        //{
                        //    Debug.Log(entries[1]);
                        //}

                        line = theReader.ReadLine();
                    }
                    while (line != null);
                }
                // Done reading, close the reader and return true to broadcast success    
                theReader.Close();
                return true;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Not Reading File");
            return false;
        }
    }
    

    void SpawnGem(float height, float angle)
    {
        GameObject gem = Instantiate(
                gemPrefab,
                new Vector3(2 * Mathf.Sin(angle * Mathf.PI / 180), (height + leveloffset )* scroll_speed , 2 * Mathf.Cos(angle * Mathf.PI / 180)),
                Quaternion.Euler(new Vector3(0, angle, 90))
            ) as GameObject;
        gem.SendMessage("setOffset", gameObject.transform.position.y);
        gem.SendMessage("setScrollSpeed", scroll_speed);
        gem.SendMessage("setTime", height);
        gem.SendMessage("setAudioSource", audioSource);
    }
    void SpawnSlideGem(float height, float angle)
    {
        GameObject gem = Instantiate(
                gemSlidePrefab,
                new Vector3(2 * Mathf.Sin(angle * Mathf.PI / 180), (height + leveloffset) * scroll_speed, 2 * Mathf.Cos(angle * Mathf.PI / 180)),
                Quaternion.Euler(new Vector3(0, angle , 90))
            ) as GameObject;
        gem.SendMessage("setOffset",gameObject.transform.position.y);
        Debug.Log("offset : " + gameObject.transform.position.y.ToString());
        gem.SendMessage("setScrollSpeed", scroll_speed);
        gem.SendMessage("setTime", height);
        gem.SendMessage("setAudioSource", audioSource);
    }


    // Update is called once per frame
    void Update () {
        //if (Time.fixedTime - last_spawn_time > 1)
        //{
        //    SpawnGem(10, angle);
        //    last_spawn_time = Time.fixedTime;
        //    angle += 10*Mathf.PI/180;
        //}
    }

    void FixedUpdate()
    {
        
    }

    
}
