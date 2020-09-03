using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    [Header("Tutorial Components")]
    public List<GameObject> tutorialObjects = new List<GameObject>();
    public List<GameObject> fixedObjects = new List<GameObject>();
    public List<GameObject> tutorialPanels = new List<GameObject>();
    // public UPersian.Components.RtlText guide;
    public TouchManager touchManager;
    #region privateVariables
    private List<bool> stepIsDone = new List<bool>(2) { false, false };
    private bool showGuide = true;
    private bool nextIsClicked = false;
    int i = 0;
    [SerializeField] private GameObject[] correctObjects;
    #endregion

    #region dictionary
    Dictionary<int, string> guidLines = new Dictionary<int, string>();

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        // //*These two lines are only for debuggind. Delete when you're gonna publish it.
        // if (DataManager.Instance.GetTutorial() == false)
        //     DataManager.Instance.SetTutorial(true);
        // //*Up to here

        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
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
        if (i == stepIsDone.Count)
        {
            DataManager.Instance.SetTutorial(false);
            showGuide = false;
            DataManager.Instance.Save();
            // print("set tutoral = " + DataManager.Instance.GetTutorial());

            // dialogBox.SetActive(false);
        }
        if (showGuide)
        {
            // dialogBox.SetActive(true);
            if (!stepIsDone[i])
            {
                if (!tutorialObjects[i].active)
                    tutorialObjects[i].SetActive(true);
                if (!tutorialPanels[i].active)
                    tutorialPanels[i].SetActive(true);
                // setTutorialText(i + 1);
                fixedObjects[i].GetComponent<Animator>().enabled = true;
                tutorialObjects[i].GetComponent<Animator>().Play("Hand1");
                fixedObjects[i].GetComponent<Animator>().Play("FixObj");
                // if (i == 1 || i == 2)
                // {
                if (System.Object.ReferenceEquals(touchManager.activeGameObject, correctObjects[i - 1]))//correctObjects[i - 2].GetComponent<TouchRotate>().touched)
                {
                    tutorialObjects[i].SetActive(false);
                    fixedObjects[i].GetComponent<Animator>().enabled = false;
                    //when movement is over:
                    if (Input.touchCount == 0)
                    {
                        stepIsDone[i] = true;
                        tutorialPanels[i].SetActive(false);
                        i++;
                    }
                }
                //}
            }
        }

    }

    public void next()
    {
        stepIsDone[i] = true;
        tutorialObjects[i].SetActive(false);
        fixedObjects[i].GetComponent<Animator>().enabled = false;
        tutorialPanels[i].SetActive(false);
        i++;
    }


}
