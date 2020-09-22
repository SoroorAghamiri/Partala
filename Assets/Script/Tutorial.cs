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
    public GameObject[] correctObjects;

    public GameObject[] correctObjectsLights;
    [Space(20)]

    public Button uiButtons;
    public GameObject rotateLight;
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public TouchManager touchManager;
    public int stepCount;

    // public FocusSwitcher focus;
    #region privateVariables
    private List<bool> stepIsDone;
    private bool showGuide = true;
    private bool nextIsClicked = false;
    private bool rotationDone = false;
    private int i = 0;


    private GameObject[] cityLights;

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

        Debug.Log("Level index = " + levelIndex);

        stepIsDone = new List<bool>(stepCount);
        for (int j = 0; j < stepIsDone.Capacity; j++)
        {
            stepIsDone.Add(false);
        }

        // correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
        // if (levelIndex == "1" || levelIndex == "2")
        //     correctObjectsLights = GameObject.FindGameObjectsWithTag("MainComponentLight");
        if (levelIndex == "3")
            cityLights = GameObject.FindGameObjectsWithTag("CityLight");

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


    void Update()
    {
        if (i == stepCount)
        {
            uiButtons.interactable = true;
            globalLight.intensity = 1f;
            for (int j = 0; j < correctObjectsLights.Length; j++)
            {
                correctObjectsLights[j].SetActive(false);
            }
            showGuide = false;
            if (levelIndex == 3.ToString())
            {
                DataManager.Instance.SetTutorial(false);

                DataManager.Instance.Save();
            }

        }
        if (showGuide)
        {

            if (!stepIsDone[i])
            {

                switch (levelIndex)
                {
                    case "1":
                        print("1 Here");
                        showGuidText(i, true);
                        glowObjects(i);
                        showFixedObject();
                        break;
                    case "2":
                        print("2 Here");
                        glowObjects(i);
                        if (i == 0)
                        {
                            rotationButton();
                            showGuidText(0, true);

                        }
                        break;
                    case "3":
                        print("3 Here");
                        showCityLights();
                        break;
                }


                if (Object.ReferenceEquals(touchManager.activeGameObject, correctObjects[i]))//correctObjects[i - 2].GetComponent<TouchRotate>().touched)
                {

                    if (correctObjectsLights[i].active)
                        correctObjectsLights[i].GetComponent<Animator>().enabled = false;
                    if (levelIndex == "2" && i == 0)
                    {
                        if (touchManager.rotate == true)
                        {
                            if (rotateLight != null && rotateLight.active)
                            {
                                rotateLight.GetComponent<Animator>().enabled = false;
                                rotateLight.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;

                            }
                            rotationDone = true;
                        }
                    }


                    //when movement is over:
                    if (Input.touchCount == 0)
                    {



                        switch (levelIndex)
                        {
                            case "1":
                                showGuidText(i, false);
                                stepIsDone[i] = true;
                                // if (correctObjectsLights[i].active)
                                //     correctObjectsLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().falloffIntensity = 0.51f;
                                if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                                    fixedObjectLight[0].SetActive(false);
                                i++;
                                break;
                            case "2":
                                if (touchManager.rotate == false && rotationDone == true)
                                {
                                    showGuidText(0, false);
                                    stepIsDone[i] = true;
                                    // if (correctObjectsLights[i].active)
                                    //     correctObjectsLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().falloffIntensity = 0.51f;
                                    if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                                        fixedObjectLight[0].SetActive(false);
                                    i++;
                                }

                                break;
                        }
                        // stepIsDone[i] = true;
                        // // if (correctObjectsLights[i].active)
                        // //     correctObjectsLights[i].GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().falloffIntensity = 0.51f;
                        // if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                        //     fixedObjectLight[0].SetActive(false);
                        // i++;
                    }
                }
            }
        }

    }

    void glowObjects(int indx)
    {
        correctObjectsLights[indx].SetActive(true);

    }

    void showGuidText(int indx, bool active)
    {
        if (active)
        {
            if (tutorialPanels.Count > 0 && tutorialPanels[indx].name == i.ToString())
            {
                tutorialPanels[indx].SetActive(true);
            }
        }
        else
        {
            if (tutorialPanels[indx].active)
                tutorialPanels[indx].SetActive(false);
        }
    }

    void showFixedObject()
    {
        if (fixedObjectLight.Count > 0 && fixedObjectLight[0].name == i.ToString())
        {

            fixedObjectLight[0].SetActive(true);
            fixedObjectLight[0].GetComponent<Animator>().enabled = true;

            fixedObjectLight[0].GetComponent<Animator>().Play("Light");

        }
    }

    void showCityLights()
    {
        foreach (GameObject go in cityLights)
        {
            go.GetComponent<Animator>().enabled = true;
            go.GetComponent<Animator>().Play("FixObj");
        }
    }

    void rotationButton()
    {
        if (i == 0)
            rotateLight.SetActive(true);
    }

}
