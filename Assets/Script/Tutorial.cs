using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    [Header("Tutorial Components")]
    // public List<GameObject> tutorialObjects = new List<GameObject>();
    public List<GameObject> fixedObjects = new List<GameObject>();
    public List<GameObject> tutorialPanels = new List<GameObject>();
    public List<GameObject> fixedObjectLight = new List<GameObject>();


    [Space(20)]

    public Button uiButtons;
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public TouchManager touchManager;
    public int stepCount;

    // public FocusSwitcher focus;
    #region privateVariables
    private List<bool> stepIsDone;
    private bool showGuide = true;
    private bool nextIsClicked = false;
    private int i = 0;
    private GameObject[] correctObjects;
    private GameObject[] correctObjectsLights;
    #endregion

    string levelIndex;



    // Start is called before the first frame update
    void Start()
    {
        // //*These two lines are only for debuggind. Delete when you're gonna publish it.
        // if (DataManager.Instance.GetTutorial() == false)
        //     DataManager.Instance.SetTutorial(true);
        // //*Up to here
        uiButtons.interactable = false;

        levelIndex = SceneManager.GetActiveScene().name;
        levelIndex = levelIndex.Substring(levelIndex.Length - 1);
        // print("LevelIndex = " + levelIndex);

        stepIsDone = new List<bool>(stepCount);
        for (int j = 0; j < stepIsDone.Capacity; j++)
        {
            stepIsDone.Add(false);
        }

        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
        correctObjectsLights = GameObject.FindGameObjectsWithTag("MainComponentLight");
        for (int j = 0; j < correctObjectsLights.Length; j++)
        {
            correctObjectsLights[j].SetActive(false);
        }

        foreach (GameObject go in fixedObjects)
        {
            go.GetComponent<Animator>().enabled = false;
        }

        if (DataManager.Instance.GetTutorial() == false)
            showGuide = false;




    }

    // Update is called once per frame

    // List<GameObject> setFocused = new List<GameObject>();
    void Update()
    {
        if (i == stepCount)
        {
            uiButtons.interactable = true;
            globalLight.intensity = 1f;
            showGuide = false;
            if (levelIndex == 3.ToString())
            {
                DataManager.Instance.SetTutorial(false);

                DataManager.Instance.Save();
            }
            // focus.SetFocused(setFocused);
        }
        if (showGuide)
        {

            if (!stepIsDone[i])
            {
                // if (!tutorialObjects[i].active)
                //     tutorialObjects[i].SetActive(true);
                print("i = " + i.ToString());
                if (!tutorialPanels[i].active && tutorialPanels.Count > 0 && tutorialPanels[i].name == i.ToString())
                    tutorialPanels[i].SetActive(true);


                correctObjectsLights[i].SetActive(true);
                if (fixedObjectLight.Count > 0 && fixedObjectLight[0].name == i.ToString())
                {
                    // print("Fixed object light name= " + fixedObjectLight[i].name);
                    fixedObjectLight[0].SetActive(true);
                    fixedObjectLight[0].GetComponent<Animator>().enabled = true;
                    // tutorialObjects[i].GetComponent<Animator>().Play("Hand1");
                    fixedObjectLight[0].GetComponent<Animator>().Play("Light");

                }
                // setFocused.Add(tutorialPanels[i]);
                // // setFocused.Add(fixedObjects[i]);
                // setFocused.Add(tutorialObjects[i]);
                // setFocused.Add(correctObjects[i]);
                // for (int i = 0; i < setFocused.Count; i++)
                // {
                //     print(setFocused[i].name);
                // }
                // focus.SetFocused(setFocused);

                if (Object.ReferenceEquals(touchManager.activeGameObject, correctObjects[i]))//correctObjects[i - 2].GetComponent<TouchRotate>().touched)
                {
                    // for (int i = 0; i < setFocused.Count; i++)
                    // {
                    //     setFocused[i] = null;
                    // }
                    // focus.SetFocused(setFocused);

                    // tutorialObjects[i].SetActive(false);

                    if (correctObjectsLights[i].active)
                        correctObjectsLights[i].GetComponent<Animator>().enabled = false;

                    //when movement is over:
                    if (Input.touchCount == 0)
                    {
                        stepIsDone[i] = true;
                        tutorialPanels[i].SetActive(false);

                        correctObjectsLights[i].SetActive(false);
                        if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                            fixedObjectLight[0].SetActive(false);
                        i++;
                        // setFocused.RemoveRange(0, setFocused.Count - 1);
                    }
                }
            }
        }

    }


}
