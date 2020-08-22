using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : DialogBase
{
    // Working Params
    [Header("Currency Parameters")]
    public Text numberOfFeathers;
    public bool firstHint;
    public bool hintShown;
    public GameObject[] wrongComponents;
    public GameObject finalObject;
    [SerializeField] public int featherDiscount1;
    [SerializeField] public int featherDiscount2;
    //Config
    [SerializeField] private float speedForScale;
    [SerializeField] private float limit = 1;
    // public static HintScript hintscript;
    /// <summary>
    /// Remove Later
    /// </summary>
    private GameObject[] correctObjects;

    void Start()
    {
        DataManager.Instance.Load();
        initialization();
    }

    private void initialization()
    {
        finalObject = GameObject.FindGameObjectWithTag("Final");
        firstHint = false;
        hintShown = false;
        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
        wrongComponents = GameObject.FindGameObjectsWithTag("WrongComponent");
        numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
    }


    void Update()
    {
        if (hintShown)
        {
            if (Input.touchCount > 0)
            {
                finalObject.transform.localScale = new Vector3(0, 0, 0);
                hintShown = false;
            }

        }
    }


    public void cancelPanel()
    {
        ViewManager.instance.closeView(this);
    }


    public void FirstHintButton()//button for First Hint 
    {
        if (!firstHint)
        {
            if (DataManager.Instance.GetFeather() - featherDiscount1 >= 0)
            {
                DeactivateWrongComponents();
                firstHint = true;
                DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() - featherDiscount1);
                GameManger.Instans.ShowNumberOfFeathers();
            }
        }
    }

    public void SecondHintButton()//Button for Second Hint 
    {
        if (!hintShown)
        {
            if (DataManager.Instance.GetFeather() - featherDiscount2 >= 0)
            {
                finalObject.SetActive(true);
                StartCoroutine(ShowFinalObject());
                StartCoroutine(MakeFinalObjectHidden());
                hintShown = true;
                DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() - featherDiscount2);
                GameManger.Instans.ShowNumberOfFeathers();
                // GameManger.Instans.HintPanelColse();
                ViewManager.instance.closeView(this);
            }


        }
    }

    private IEnumerator MakeFinalObjectHidden()
    {
        yield return new WaitForSeconds(2.0f);
        finalObject.transform.localScale = new Vector3(0, 0, 0);
        ///
        ///Remove Later
        ///
        for (int i = 0; i < correctObjects.Length; i++)
        {
            correctObjects[i].SetActive(true);
        }
        for (int i = 0; i < wrongComponents.Length; i++)
        {
            wrongComponents[i].SetActive(true);
        }
    }

    private void DeactivateWrongComponents()
    {
        for (int i = 0; i < wrongComponents.Length; i++)
        {
            wrongComponents[i].SetActive(false);
        }
    }

    public IEnumerator ShowFinalObject()
    {
        ///
        ///Remove Later
        ///
        for (int i = 0; i < correctObjects.Length; i++)
        {
            correctObjects[i].SetActive(false);
        }
        for (int i = 0; i < wrongComponents.Length; i++)
        {
            wrongComponents[i].SetActive(false);
        }
        while (finalObject.transform.localScale.x < limit)
        {
            finalObject.transform.localScale += new Vector3(
                speedForScale * Time.deltaTime,
                speedForScale * Time.deltaTime,
                speedForScale * Time.deltaTime);
            yield return null;
        }
    }
}
