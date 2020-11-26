using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Runtime.Remoting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{

    [Header("Tutorial Components")]
    public string levelIndex;
    public int stepCount;
    public List<GameObject> tutorialPanels = new List<GameObject>();
    public List<GameObject> fixedObjectLight = new List<GameObject>();

    public GameObject[] correctObjects;
    public GameObject[] correctObjectsLights;
    public GameObject[] correctObjectsShades;

    [Space(20)]
    public Button uiButtons;
    public GameObject rotateLight = null;
    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public UnityEngine.Experimental.Rendering.Universal.Light2D rightLight;
    public GameObject hintLight = null;

    #region privateVariables
    private List<bool> stepIsDone;
    private bool showGuide = true;
    private bool rotationDone = false;
    [SerializeField]private GameManger gameManager;
    [SerializeField]private ObjectFixer objectFixer;
    [SerializeField]private TouchManager touchManager;

    private int i = 0;
    private CurrencyView cview;
    
    private GameObject[] cityLights;
     bool touched = false;
     List<float> steadyDist;
     float movingDist = 0;
    #endregion

    
 



    // Start is called before the first frame update
    void Start()
    {
        // //*These two lines are only for debuggind. Delete when you're gonna publish it.
        // if (DataManager.Instance.GetTutorial() == false)
        //     DataManager.Instance.SetTutorial(true);
        // //*Up to here

        

        // dynamic _active = PersistentSceneManager.instance.activeScene;//SceneManager.GetActiveScene().name;
        // Type unknown = _active.GetType();
        // Debug.Log("unknown is " + unknown);
        // levelIndex = _active.ToString();//levelIndex.Substring(levelIndex.Length - 1);
        // Debug.Log("level index " + levelIndex);

        
        
        

        if (DataManager.Instance.GetTutorial() == false)
        {
            showGuide = false;
        }
        else{
            InititlaizeVariables();
            Initialization(levelIndex);
        }
    }

    void InititlaizeVariables(){
        gameManager = GameObject.FindObjectOfType<GameManger>();
        objectFixer = GameObject.FindObjectOfType<ObjectFixer>();
        touchManager = GameObject.FindObjectOfType<TouchManager>();

        stepIsDone = new List<bool>(stepCount);
        for (int j = 0; j < stepIsDone.Capacity; j++)
        {
            stepIsDone.Add(false);
        }

        for (int j = 0; j < correctObjectsLights.Length; j++)
        {
            correctObjectsLights[j].SetActive(false);
        }

        steadyDist = new List<float>(correctObjects.Length);
        for(int j = 0 ; j < correctObjectsShades.Length ; j++){
            steadyDist.Add( Vector2.Distance(correctObjects[j].transform.position , correctObjectsShades[j].transform.position));
        }
    }

    void Initialization(string levelToDisplay)
    {
        switch (levelToDisplay)
        {
            case "1":
                uiButtons.interactable = false;
                break;
            case "2":
                uiButtons.interactable = false;
                break;
            case "3":
                cityLights = GameObject.FindGameObjectsWithTag("CityLight");
                if (hintLight.active)
                {
                    hintLight.SetActive(false);
                }
                break;
        }
    }



    void Update()
    {
        
        if (showGuide)
        {
            if(cview == null){
            cview = GameObject.FindObjectOfType<CurrencyView>();
            }
            if (i == stepCount)
            {
                rightLight.intensity = 0f;
                uiButtons.interactable = true;
                if(hintLight != null && hintLight.active)
                    hintLight.SetActive(false);
                globalLight.intensity = 1f;

                if (correctObjectsShades.Length > 0)
                {
                    for (int j = 0; j < correctObjectsShades.Length; j++)
                    {
                        correctObjectsShades[j].SetActive(false);
                    }
                }
                
                // if (correctObjectsLights.Length > 0)
                // {
                //     for (int j = 0; j < correctObjectsLights.Length; j++)
                //     {
                //         correctObjectsLights[j].SetActive(false);
                //     }
                // }
                
                if (levelIndex == "3")
                {
                    // Debug.Log("Tutorial turned false");
                    DataManager.Instance.SetTutorial(false);

                    DataManager.Instance.Save();
                }
                showGuide = false;
            }
            if (!stepIsDone[i])
            {

                switch (levelIndex)
                {
                    case "1":
                        showGuidText(i, true);
                        glowObjects(i);
                        showFixedObject();
                        break;
                    case "2":
                        switch (i)
                        {
                            case 0:
                                glowObjects(i);
                                showGuidText(i, true);
                                break;
                            case 1:
                                rotationButton();
                                showGuidText(i, true);
                                break;
                            default:
                                glowObjects(i);
                                break;
                        }
                        break;
                    case "3":
                        switch (i)
                        {
                            case 0:
                                hintLight.SetActive(true);
                                showGuidText(i, true);
                                break;
                            case 1:
                                showCityLights();
                                globalLight.intensity = 1f;
                                rightLight.intensity = 0f;
                                break;
                        }
                        break;
                }

                if(levelIndex == "3" && i==0 && cview.scalingD){
                    
                        showGuidText(i, false);
                        hintLight.SetActive(false);
                            i++;
                    
                }
                if(Input.touchCount >0){
                    if (UnityEngine.Object.ReferenceEquals(touchManager.activeGameObject, correctObjects[i]))
                    {
                        if (correctObjectsLights[i].active)
                            correctObjectsLights[i].GetComponent<Animator>().enabled = false;
                        if (levelIndex == "2" && i == 1)
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
                        movingDist = Vector2.Distance(correctObjects[i].transform.position , correctObjectsShades[i].transform.position);
                        if(movingDist < steadyDist[i] && rightLight.intensity >0){
                            rightLight.intensity -= 0.01f;
                        }else if(movingDist > steadyDist[i] && rightLight.intensity < 1){
                            rightLight.intensity += 0.01f;
                        }
                    }
                  touched = true;  
                }
                
                
                //when movement is over:
                if (Input.touchCount == 0 && objectFixer.isFixed && touched)
                {

                    switch (levelIndex)
                    {
                        case "1":
                            showGuidText(i, false);
                            stepIsDone[i] = true;
                            if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                                fixedObjectLight[0].SetActive(false);
                            if(correctObjectsLights[i].active)
                                correctObjectsLights[i].SetActive(false);
                            i++;
                            break;
                        case "2":
                            if (touchManager.rotate == false && rotationDone == true && i == 1)
                            {
                                showGuidText(1, false);
                                stepIsDone[i] = true;
                                if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                                    fixedObjectLight[0].SetActive(false);
                                if(correctObjectsLights[i].active)
                                    correctObjectsLights[i].SetActive(false);
                                i++;
                            }
                            else if (i != 1)
                            {
                                if (tutorialPanels[0].active)
                                    showGuidText(0, false);
                                stepIsDone[i] = true;
                                if (fixedObjectLight.Count > 0 && fixedObjectLight[0].active)
                                    fixedObjectLight[0].SetActive(false);
                                if(correctObjectsLights[i].active && i!=0)
                                    correctObjectsLights[i].SetActive(false);
                                i++;
                            }
                            break;
                    }
                    rightLight.intensity = 1f;
                    touched = false;
                }
                
                
            }
        }
        else if (!showGuide)
        {
            uiButtons.interactable = true;
            globalLight.intensity = 1f;

        }
    }

    void glowObjects(int indx)
    {
        correctObjectsLights[indx].SetActive(true);
        correctObjectsShades[indx].SetActive(true);
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
        if (i == 1)
            rotateLight.SetActive(true);
    }

}
