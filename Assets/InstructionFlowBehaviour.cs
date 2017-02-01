using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class InstructionFlowBehaviour : MonoBehaviour {

	// Keep track of which phase of the instructions you're in
	private string instructionPhase;

    private bool keepSpawningTap, keepSpawningTrace, keepSpawningSwing;

	// Tutorial images
	public SpriteRenderer firstImage;
	public SpriteRenderer centerImage;
	public SpriteRenderer leftImage;
	public SpriteRenderer rightImage;
	public SpriteRenderer lastImage;

	// Buttons
	public Button nextButton;
	public Button tryItButton;
	public Button gotItButton;
	public Button finishedButton;

    // Tracks
    public GameObject tapTrack;
    public GameObject swingTrack;
    public GameObject traceTrack;

    public GameObject gemPrefab;

    private List<GemBehaviour> gemList;
    private float gyroSpeedThreshold = 10.0f;

	// Use this for initialization
	void Start () {
        gemList = new List<GemBehaviour>();
		firstImage.enabled = true;
		nextButton.gameObject.SetActive (true);

		centerImage.enabled = false;
		leftImage.enabled = false;
		rightImage.enabled = false;
		lastImage.enabled = false;
		tryItButton.gameObject.SetActive (false);
		gotItButton.gameObject.SetActive (false);
		finishedButton.gameObject.SetActive (false);

        keepSpawningSwing = keepSpawningTap = keepSpawningTrace = true;

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Make sure missed gems aren't being considered as the next gem in line
        while (gemList.Count > 0 && gemList[0].missed)
        {
            // If next gem in the list is a missed slide gem, make sure the rest of the slide gets missed too
            if (gemList[0].GetState() == GemBehaviour.GemState.SLIDE_START || gemList[0].GetState() == GemBehaviour.GemState.SLIDE_MID)
            {
                int nextGemCounter = 1;
                while (gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_MID || gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_END)
                {
                    gemList[nextGemCounter].SetAsMissed();

                    // Stop if you find the end of the slide
                    if (gemList[nextGemCounter].GetState() == GemBehaviour.GemState.SLIDE_END)
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

            switch (nextGem.GetState())
            {
                case GemBehaviour.GemState.TAP:
                case GemBehaviour.GemState.SLIDE_START:
                    if (GetControllerInputOneShot())
                    {
                        if (nextGem.gameObject.transform.position.y <= 2.0f)
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
                        if (nextGem.gameObject.transform.position.y <= 0f)
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
                case GemBehaviour.GemState.TRACE_MID:
                case GemBehaviour.GemState.TRACE_PIVOT:
                    if (nextGem.ready)
                    {
                        nextGem.Fire();
                        gemList.Remove(nextGem);
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

    // Button function - advance to next screen
    public void AdvanceToFirstInstruction() {
		// Make the first (center) instructions show up 
		instructionPhase = "center";
		firstImage.enabled = false;
		nextButton.gameObject.SetActive (false);
		centerImage.enabled = true;
		tryItButton.gameObject.SetActive (true);
	}

	// Button function - finish an exercise 
	public void DoneWithExercise() {
		// If you're in center phase, go to left phase image
		if (instructionPhase.Equals("center")) {
            keepSpawningTap = false;
            tapTrack.SetActive(false);

            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i++)
            {
                GameObject.Destroy(gems[i]);
            }
            gemList.Clear();
            gotItButton.gameObject.SetActive (false);
			leftImage.enabled = true;
			tryItButton.gameObject.SetActive (true);
			instructionPhase = "left";
		}
			
		// If you're in left phase, go to right phase image
		else if (instructionPhase.Equals("left")) {
            keepSpawningSwing = false;
            swingTrack.SetActive(false);

            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i++)
            {
                GameObject.Destroy(gems[i]);
            }
            gemList.Clear();
            gotItButton.gameObject.SetActive (false);
			rightImage.enabled = true;
			tryItButton.gameObject.SetActive (true);
			instructionPhase = "right";
		}
				
		// If you're in right phase, go to last instruction screen
		else if (instructionPhase.Equals("right")) {
            keepSpawningTrace = false;
            traceTrack.SetActive(false);

            GameObject[] gems = GameObject.FindGameObjectsWithTag("Gem");
            for (int i = 0; i < gems.Length; i++)
            {
                GameObject.Destroy(gems[i]);
            }
            gemList.Clear();
            gotItButton.gameObject.SetActive (false);
			lastImage.enabled = true;
			finishedButton.gameObject.SetActive (true);
		}

		else {
			Debug.Log("Something in instructions broke.");
		}
	}

	// Button function - start exercise 
	public void StartExercise() {
		// Center phase
		if (instructionPhase.Equals("center")) {
			centerImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
            StartCoroutine(SpawnNotesTap());
		}

		// Left phase
		else if (instructionPhase.Equals("left")) {
			leftImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
            StartCoroutine(SpawnNotesSwing());
		}

		// Right phase
		else if (instructionPhase.Equals("right")) {
			rightImage.enabled = false;
			tryItButton.gameObject.SetActive (false);
			gotItButton.gameObject.SetActive (true);
            StartCoroutine(SpawnNotesTrace());
		}

		else {
			Debug.Log("Something in instructions broke.");
		}
	}

	// Button function - done with the tutorial (for now goes to start screen)
	public void EndTutorial() {
		SceneManager.LoadScene("StartMenu");
	}

    private IEnumerator SpawnNotesTap()
    {
        tapTrack.SetActive(true);

        Vector3 spawnPos = tapTrack.transform.position;
        spawnPos.y += 10;

        Quaternion spawnRot = tapTrack.transform.rotation;

        int counter = -1;
        while(keepSpawningTap)
        {
            counter++;
            counter %= 3;
            switch(counter)
            {
                case 0:
                case 1:
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.TAP, 0f).GetComponent<GemBehaviour>());
                    break;
                case 2:
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.SLIDE_START, 0f).GetComponent<GemBehaviour>());
                    spawnPos.y += .5f;
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.SLIDE_MID, 0f).GetComponent<GemBehaviour>());
                    spawnPos.y += .5f;
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.SLIDE_END, 0f).GetComponent<GemBehaviour>());
                    break;
            }

            yield return new WaitForSeconds(1.0f);
        }
        tapTrack.SetActive(false);

        for (int i = 0; i < gemList.Count; i++)
        {
            GameObject.Destroy(gemList[i].gameObject);
        }
        gemList.Clear();

        yield return null;
    }

    private IEnumerator SpawnNotesSwing()
    {
        swingTrack.SetActive(true);

        Vector3 spawnPos = swingTrack.transform.position;
        spawnPos.y += 10.0f;
        Quaternion spawnRot = swingTrack.transform.rotation;
        Vector3 leftSpawnPos, downSpawnPos, upSpawnPos, rightSpawnPos;
        leftSpawnPos = rightSpawnPos = downSpawnPos = upSpawnPos = spawnPos;

        leftSpawnPos = swingTrack.transform.InverseTransformPoint(leftSpawnPos);
        leftSpawnPos.x -= 1.7f;
        leftSpawnPos = swingTrack.transform.TransformPoint(leftSpawnPos);

        downSpawnPos = swingTrack.transform.InverseTransformPoint(downSpawnPos);
        downSpawnPos.x -= 0.5f;
        downSpawnPos = swingTrack.transform.TransformPoint(downSpawnPos);

        upSpawnPos = swingTrack.transform.InverseTransformPoint(upSpawnPos);
        upSpawnPos.x += 0.5f;
        upSpawnPos = swingTrack.transform.TransformPoint(upSpawnPos);

        rightSpawnPos = swingTrack.transform.InverseTransformPoint(rightSpawnPos);
        rightSpawnPos.x += 1.7f;
        rightSpawnPos = swingTrack.transform.TransformPoint(rightSpawnPos);

        int counter = -1;
        while (keepSpawningSwing)
        {
            counter++;
            counter %= 4;

            switch (counter)
            {
                case 0:
                    gemList.Add(SpawnGemAtTransform(leftSpawnPos, spawnRot, GemBehaviour.GemState.SWING_LEFT, 0f).GetComponent<GemBehaviour>());
                    break;
                case 1:
                    gemList.Add(SpawnGemAtTransform(downSpawnPos, spawnRot, GemBehaviour.GemState.SWING_DOWN, 0f).GetComponent<GemBehaviour>());
                    break;
                case 2:
                    gemList.Add(SpawnGemAtTransform(upSpawnPos, spawnRot, GemBehaviour.GemState.SWING_UP, 0f).GetComponent<GemBehaviour>());
                    break;
                case 3:
                    gemList.Add(SpawnGemAtTransform(rightSpawnPos, spawnRot, GemBehaviour.GemState.SWING_RIGHT, 0f).GetComponent<GemBehaviour>());
                    break;
            }

            yield return new WaitForSeconds(1.0f);
        }

        swingTrack.SetActive(false);

        for (int i = 0; i < gemList.Count; i++)
        {
            GameObject.Destroy(gemList[i].gameObject);
        }
        gemList.Clear();
    }

    private IEnumerator SpawnNotesTrace()
    {
        traceTrack.SetActive(true);
        float traceTrackOffset = 0.525f;
        Vector3 spawnPos = traceTrack.transform.position;
        spawnPos.y += 10;

        Quaternion spawnRot = traceTrack.transform.rotation;

        while (keepSpawningTrace)
        {
            for (int i = 2; i < 7; i++)
            {
                spawnPos = traceTrack.transform.InverseTransformPoint(spawnPos);
                spawnPos.x = (i - 4) * traceTrackOffset;
                spawnPos = traceTrack.transform.TransformPoint(spawnPos);

                if(i==2 || i==6)
                {
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.TRACE_PIVOT, 0f).GetComponent<GemBehaviour>());
                }
                else
                {
                    Quaternion gemRot = traceTrack.transform.rotation;
                    Vector3 pointToPrev = gemList[gemList.Count - 1].transform.position - spawnPos;
                    gemRot.SetLookRotation(traceTrack.transform.TransformDirection(Vector3.forward), pointToPrev);
                    gemList.Add(SpawnGemAtTransform(spawnPos, gemRot, GemBehaviour.GemState.TRACE_MID, 0f).GetComponent<GemBehaviour>());
                }

                spawnPos.y += 1.0f;
            }

            for (int i = 5; i > 1; i--)
            {
                spawnPos = traceTrack.transform.InverseTransformPoint(spawnPos);
                spawnPos.x = (i - 4) * traceTrackOffset;
                spawnPos = traceTrack.transform.TransformPoint(spawnPos);

                if (i == 2)
                {
                    gemList.Add(SpawnGemAtTransform(spawnPos, spawnRot, GemBehaviour.GemState.TRACE_PIVOT, 0f).GetComponent<GemBehaviour>());
                }
                else
                {
                    Quaternion gemRot = traceTrack.transform.rotation;
                    Vector3 pointToPrev = gemList[gemList.Count - 1].transform.position - spawnPos;
                    gemRot.SetLookRotation(traceTrack.transform.TransformDirection(Vector3.forward), pointToPrev);
                    gemList.Add(SpawnGemAtTransform(spawnPos, gemRot, GemBehaviour.GemState.TRACE_MID, 0f).GetComponent<GemBehaviour>());
                }

                spawnPos.y += 1.0f;
            }

            spawnPos.y = traceTrack.transform.position.y + 10f;

            yield return new WaitForSeconds(3.0f);
        }
        yield return null;
    }

    GameObject SpawnGemAtTransform(Vector3 pos, Quaternion rot, GemBehaviour.GemState state, float time)
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
        gemInfo.SetTime(time);
        gemInfo.SetAudioSource(null);

        return gem;
    }
}
