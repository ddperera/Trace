using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class GameManagerBehaviour : MonoBehaviour {

    public AudioSource audioSource;

    public GameObject gemPrefab;
    public List<GemBehaviour> gemList;

    public float scrollSpeed;
    public float spawnTime;
    float lastSpawnTime = 0;
    float angle = 0;
    float slideDiff = .05f;
    float levelOffset = 10;

    public float tapGemYThreshold = 2.0f;
    public float slideGemYThreshold = 0f;

    // Use this for initialization
    void Start ()
    {
        gemList = new List<GemBehaviour>();
        //StartCoroutine(StartSpawning());

        LoadLevel("yee");
        audioSource.Play(0);
    }

	// Update is called once per frame
	void Update ()
    {
        while(gemList.Count > 0 && gemList[0].missed)
        {
            gemList.Remove(gemList[0]);
        }

        if (gemList.Count > 0)
        {
            GemBehaviour nextGem = gemList[0];
            //nextGem.MakeBlue();

            if (nextGem.isSlide)
            {
                if (GetControllerInputContinuous())
                {
                    if(nextGem.gameObject.transform.position.y <= slideGemYThreshold)
                    {
                        nextGem.Fire();
                        gemList.Remove(nextGem);
                    }
                    
                }
            }
            else
            {
                if (GetControllerInputOneShot())
                {
                    if (nextGem.gameObject.transform.position.y <= tapGemYThreshold)
                    {
                        nextGem.Fire();
                        gemList.Remove(nextGem);
                    }
                }
            }
        }
    }

    private bool GetControllerInputOneShot()
    {
        bool result;

        result = GvrController.ClickButtonDown;

#if UNITY_EDITOR
        result = GvrController.TouchDown;
#endif 

        return result;
    }

    private bool GetControllerInputContinuous()
    {
        bool result;

        result = GvrController.ClickButton;

#if UNITY_EDITOR
        result = GvrController.IsTouching;
#endif 

        return result;
    }

    private bool LoadLevel(string levelName)
    {
        //string fileName = "Assets/Scenes/TraceGame/Audio/" + levelName + ".txt";
        TextAsset txt = (TextAsset)Resources.Load(levelName, typeof(TextAsset));
        string level = txt.text;
        //string hardCode = "2.093877551\tslide start 0\n2.326530612\tslide end 30\n2.526530612\tclick 15\n2.746734693\tclick 45\n3.218299319\tclick 45\n3.697823129\tclick 30\n4.035102040\tclick 45\n4.469387755\tclick 0\n5.116303854\tclick 60\n5.428503401\tclick 60\n5.908616780\tslide start 0\n6.099795918\tslide end 30\n6.426258503\tclick 15\n6.609478458\tclick 45\n7.074671201\tclick 45\n7.554149659\tclick 60\n7.850997732\tclick 60\n8.376553287\tslide start 70\n8.718390022\tslide node 90\n9.066916099\tslide end 60";
        try
        {
            string line;
            StringReader theReader = new StringReader(level);

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
                            SpawnGem(time, float.Parse(entries[1]), GemBehaviour.GemState.TAP);
                        }
                        if (entries[0] == "slide")
                        {
                            if (entries[1] == "start")
                            {
                                slideTime = time;
                                slideStartAngle = float.Parse(entries[2]);

                                SpawnGem(time, float.Parse(entries[2]), GemBehaviour.GemState.SLIDE_START);
                            }
                            else
                            {
                                float dt = (time - slideTime);
                                int num_to_spawn = (int)Math.Floor(dt / slideDiff);
                                dt = dt / num_to_spawn;
                                float dAngle = (float.Parse(entries[2]) - slideStartAngle) / num_to_spawn;
                                GemBehaviour.GemState stateToSpawn = GemBehaviour.GemState.SLIDE_MID;
                                for (int i = 0; i < num_to_spawn; i++)
                                {
                                    if(i == num_to_spawn - 1 && entries[1] == "end")
                                    {
                                        stateToSpawn = GemBehaviour.GemState.SLIDE_END;
                                    }
                                    SpawnGem(slideTime + dt * (i + 1), slideStartAngle + dAngle * (i + 1), stateToSpawn);
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

    void SpawnGem(float height, float angle, GemBehaviour.GemState state)
    {
        GameObject gem = Instantiate(
                gemPrefab,
                new Vector3(2 * Mathf.Sin(angle * Mathf.PI / 180), (height + levelOffset) * scrollSpeed, 5 + 2 * Mathf.Cos(angle * Mathf.PI / 180)),
                Quaternion.Euler(new Vector3(0, angle, 90))
            ) as GameObject;
        gem.SetActive(true);
        GemBehaviour gemInfo = gem.GetComponent<GemBehaviour>();
        gemInfo.SetState(state);
        gemInfo.SetOffset(gameObject.transform.position.y);
        gemInfo.SetScrollSpeed(scrollSpeed);
        gemInfo.SetTime(height);
        gemInfo.SetAudioSource(audioSource);

        gemList.Add(gemInfo);
    }

    private IEnumerator StartSpawning()
    {
        /*
        GameObject spawnedGem;
        while(true)
        {
            GameObject gem = (GameObject)GameObject.Instantiate(
                gemPrefab,
                gemPrefab.transform.position,
                gemPrefab.transform.rotation);
            gemList.Add(gem);
            gem.SetActive(true);
            gem.SendMessage("SetOffset", gameObject.transform.position.y);
            gem.SendMessage("SetScrollSpeed", 5f);
            gem.SendMessage("SetAudioSource", audioSource);
            yield return new WaitForSeconds(.75f);
        }
        */

        yield return null;
    }
}
