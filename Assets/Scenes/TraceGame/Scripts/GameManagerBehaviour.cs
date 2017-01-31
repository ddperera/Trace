using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine.SceneManagement;

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

    public float gyroSpeedThreshold;

    public Transform slideSpawnCenter;

    // Use this for initialization
    void Start ()
    {
        gemList = new List<GemBehaviour>();
        StartCoroutine(StartSpawning());

        //LoadLevel("yee");
        //audioSource.Play(0);
    }

	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(GvrController.Accel);
        //Debug.Log(GvrController.Orientation.ToEulerAngles());
        Debug.Log(GvrController.Gyro);

        if(GvrController.AppButton)
        {
            SceneManager.LoadScene("SplashScreen");
        }

        // Make sure missed gems aren't being considered as the next gem in line
        while(gemList.Count > 0 && gemList[0].missed)
        {
            // If next gem in the list is a missed slide gem, make sure the rest of the slide gets missed too
            if(gemList[0].GetState() == GemBehaviour.GemState.SLIDE_START || gemList[0].GetState() == GemBehaviour.GemState.SLIDE_MID)
            {
                int nextGemCounter = 1;
                while (gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_MID || gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_END)
                {
                    gemList[nextGemCounter].SetAsMissed();

                    // Stop if you find the end of the slide
                    if(gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_END)
                    {
                        break;
                    }
                    nextGemCounter++;
                }
            }
            gemList.Remove(gemList[0]);
        }

        if (gemList.Count > 0)
        {
            GemBehaviour nextGem = gemList[0];
            //nextGem.MakeBlue();

            switch(nextGem.GetState())
            {
                case GemBehaviour.GemState.TAP:
                case GemBehaviour.GemState.SLIDE_START:
                    if (GetControllerInputOneShot())
                    {
                        if (nextGem.gameObject.transform.position.y <= tapGemYThreshold)
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }
                    }
                    break;
                case GemBehaviour.GemState.SLIDE_MID:
                case GemBehaviour.GemState.SLIDE_END:
                    if (GetControllerInputContinuous())
                    {
                        if (nextGem.gameObject.transform.position.y <= slideGemYThreshold)
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }

                    }
                    break;
                case GemBehaviour.GemState.SWING_UP:
                    if (nextGem.ready)
                    {
                        if (GetControllerInputUpSwing())
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }
                    }
                    break;
                case GemBehaviour.GemState.SWING_DOWN:
                    if (nextGem.ready)
                    {
                        if (GetControllerInputDownSwing())
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }
                    }
                    break;
                case GemBehaviour.GemState.SWING_LEFT:
                    if (nextGem.ready)
                    {
                        if (GetControllerInputLeftSwing())
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }
                    }
                    break;
                case GemBehaviour.GemState.SWING_RIGHT:
                    if (nextGem.ready)
                    {
                        if (GetControllerInputRightSwing())
                        {
                            nextGem.Fire();
                            gemList.Remove(nextGem);
                        }
                    }
                    break;
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

    private bool GetControllerInputUpSwing()
    {
        return GvrController.Gyro.x < -gyroSpeedThreshold;
    }

    private bool GetControllerInputDownSwing()
    {
        return GvrController.Gyro.x > gyroSpeedThreshold;
    }

    private bool GetControllerInputLeftSwing()
    {
        return GvrController.Gyro.y < -gyroSpeedThreshold;
    }

    private bool GetControllerInputRightSwing()
    {
        return GvrController.Gyro.y > gyroSpeedThreshold;
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

    void SpawnGemAtTransform(Vector3 pos, Quaternion rot, GemBehaviour.GemState state)
    {
        GameObject gem = Instantiate(
                gemPrefab,
                pos,
                rot
            ) as GameObject;
        gem.SetActive(true);
        GemBehaviour gemInfo = gem.GetComponent<GemBehaviour>();
        gemInfo.SetState(state);
        gemInfo.SetOffset(gameObject.transform.position.y);
        gemInfo.SetScrollSpeed(3.0f);
        gemInfo.SetTime(0f);
        gemInfo.SetAudioSource(audioSource);

        gemList.Add(gemInfo);
    }

    private IEnumerator StartSpawning()
    {
        Vector3 spawnPos = slideSpawnCenter.position;
        Quaternion spawnRot = slideSpawnCenter.rotation;
        Vector3 leftSpawnPos, downSpawnPos, upSpawnPos, rightSpawnPos;
        leftSpawnPos = rightSpawnPos = downSpawnPos = upSpawnPos = spawnPos;

        leftSpawnPos = slideSpawnCenter.InverseTransformPoint(leftSpawnPos);
        leftSpawnPos.x -= 1.7f;
        leftSpawnPos = slideSpawnCenter.TransformPoint(leftSpawnPos);

        downSpawnPos = slideSpawnCenter.InverseTransformPoint(downSpawnPos);
        downSpawnPos.x -= 0.5f;
        downSpawnPos = slideSpawnCenter.TransformPoint(downSpawnPos);

        upSpawnPos = slideSpawnCenter.InverseTransformPoint(upSpawnPos);
        upSpawnPos.x += 0.5f;
        upSpawnPos = slideSpawnCenter.TransformPoint(upSpawnPos);

        rightSpawnPos = slideSpawnCenter.InverseTransformPoint(rightSpawnPos);
        rightSpawnPos.x += 1.7f;
        rightSpawnPos = slideSpawnCenter.TransformPoint(rightSpawnPos);

        int counter = -1;
        while(true)
        {
            counter++;
            counter %= 4;

            switch(counter)
            {
                case 0:
                    SpawnGemAtTransform(leftSpawnPos, spawnRot, GemBehaviour.GemState.SWING_LEFT);
                    break;
                case 1:
                    SpawnGemAtTransform(downSpawnPos, spawnRot, GemBehaviour.GemState.SWING_DOWN);
                    break;
                case 2:
                    SpawnGemAtTransform(upSpawnPos, spawnRot, GemBehaviour.GemState.SWING_UP);
                    break;
                case 3:
                    SpawnGemAtTransform(rightSpawnPos, spawnRot, GemBehaviour.GemState.SWING_RIGHT);
                    break;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
