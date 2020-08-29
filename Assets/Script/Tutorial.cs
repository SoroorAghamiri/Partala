using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{


    public List<GameObject> steps = new List<GameObject>();
    public GameObject dialogBox;
    public UPersian.Components.RtlText guide;

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
        if (guidLines.Count == 0)
        {
            addToDictionary();
        }
        if (!DataManager.Instance.GetTutorial())
            showGuide = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (i == steps.Count)
        {
            DataManager.Instance.SetTutorial(false);
            showGuide = false;
            dialogBox.SetActive(false);
        }
        if (showGuide)
        {
            dialogBox.SetActive(true);
            if (!stepIsDone[i])
            {
                if (!steps[i].active)
                    steps[i].SetActive(true);
                setTutorialText(i + 1);
                steps[i].GetComponent<Animator>().Play("Hand1");
                if (i == 2 || i == 3)
                {
                    if (correctObjects[i - 2].GetComponent<TouchRotate>().touched)
                    {
                        steps[i].SetActive(false);
                        stepIsDone[i] = true;
                        i++;
                    }
                }

                if (i == 1 || i == 2)
                {
                    if (correctObjects[i - 1].GetComponent<TouchRotate>().touched)
                    {
                        steps[i].SetActive(false);
                        stepIsDone[i] = true;
                        i++;
                    }
                }

            }
        }

    }

    public void next()
    {
        print("something");

        stepIsDone[i] = true;
        steps[i].SetActive(false);
        i++;
    }
    private void setTutorialText(int step)
    {
        string value = "";
        if (guidLines.TryGetValue(step, out value))
        {
            guide.text = value;
        }
        else
        {
            guide.text = "Guide line not found";
        }
    }

    void addToDictionary()
    {
        guidLines.Add(2, "این رو بکش اینجا");
        guidLines.Add(3, "این یکی رو هم بیارش");
        guidLines.Add(1, "اینجا اسم چیزی رو که باید درست کنی رو ببین");
        guidLines.Add(4, "اینا چراغ  های شهر سوخته هستند. وقتی حرکت درستی انجام بدی روشن میشن");
    }
}
