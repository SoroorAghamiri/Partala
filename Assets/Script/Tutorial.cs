using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    [Header("Tutorial Components")]
    public List<GameObject> steps = new List<GameObject>();
    public List<GameObject> tutorialPanels = new List<GameObject>();
    // public UPersian.Components.RtlText guide;
    public TouchManager touchManager;
    #region privateVariables
    private List<bool> stepIsDone = new List<bool>(5) { false, false, false, false, false };
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
        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
        // if (guidLines.Count == 0)
        // {
        //     addToDictionary();
        // }
        // print("Get tutoral = " + DataManager.Instance.GetTutorial());
        if (DataManager.Instance.GetTutorial() == false)
            showGuide = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (i == steps.Count)
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
                if (!steps[i].active)
                    steps[i].SetActive(true);
                if (!tutorialPanels[i].active)
                    tutorialPanels[i].SetActive(true);
                // setTutorialText(i + 1);
                steps[i].GetComponent<Animator>().Play("Hand1");
                if (i == 1 || i == 2)
                {
                    if (System.Object.ReferenceEquals(touchManager.activeGameObject, correctObjects[i - 1]))//correctObjects[i - 2].GetComponent<TouchRotate>().touched)
                    {
                        steps[i].SetActive(false);
                        //when movement is over:
                        if (Input.touchCount == 0)
                        {
                            stepIsDone[i] = true;
                            tutorialPanels[i].SetActive(false);
                            i++;
                        }
                    }
                }
            }
        }

    }

    public void next()
    {
        stepIsDone[i] = true;
        steps[i].SetActive(false);
        tutorialPanels[i].SetActive(false);
        i++;
    }

    //If the texts are given as anythig other than text, the dictionary and methods below are no longer needed
    // private void setTutorialText(int step)
    // {
    //     string value = "";
    //     if (guidLines.TryGetValue(step, out value))
    //     {
    //         guide.text = value;
    //     }
    //     else
    //     {
    //         guide.text = "Guide line not found";
    //     }
    // }

    // void addToDictionary()
    // {
    //     guidLines.Add(2, "این رو بکش اینجا");
    //     guidLines.Add(3, "این یکی رو هم بیارش");
    //     guidLines.Add(1, "اینجا اسم چیزی رو که باید درست کنی رو ببین");
    //     guidLines.Add(4, "اینا چراغ  های شهر سوخته هستند. وقتی حرکت درستی انجام بدی روشن میشن");
    // }
}
