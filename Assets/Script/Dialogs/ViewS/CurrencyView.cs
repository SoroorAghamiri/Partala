using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CurrencyView : DialogBase
{
    private int[] firstHintDiscountInEpisodes = { 1, 2, 3 };
    private int[] secondHintDiscountInEpisodes = { 3, 4, 5 };
    // Working Params
    [Header("Currency Parameters")]
    public Text numberOfFeathers;
    public bool firstHint;
    public bool hintShown;
    public GameObject[] wrongComponents;
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
    private int level;
    void Start()
    {
        initialization();
    }

    private void initialization()
    {
        string temp = SceneManager.GetActiveScene().name;
        level = System.Convert.ToInt32(temp.Substring(temp.Length - 1));
        print(level);
        featherDiscount1 = firstHintDiscountInEpisodes[level - 1];
        featherDiscount2 = secondHintDiscountInEpisodes[level - 1];
        firstHint = false;
        hintShown = false;
        wrongComponents = GameObject.FindGameObjectsWithTag("WrongComponent");
        numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
        speedForScale = 10.0f;
    }


    void Update()
    {
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
                // GameManger.Instans.ShowNumberOfFeathers();
                numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
            }
        }
    }

    public void SecondHintButton()//Button for Second Hint 
    {
        if (!hintShown)
        {
            if (DataManager.Instance.GetFeather() - featherDiscount2 >= 0)
            {
                CreateNewObjectForHandlingHintAfterViewCloses();
                hintShown = true;
                DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() - featherDiscount2);
                numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
                ViewManager.instance.closeView(this);
            }
        }
    }

    private void CreateNewObjectForHandlingHintAfterViewCloses()
    {
        GameObject finalObjectManager = new GameObject("finalObjectManager");
        finalObjectManager.AddComponent<FinalObjectManager>();
        finalObjectManager.GetComponent<FinalObjectManager>().ManageSecondHint(firstHint);
    }

    private void DeactivateWrongComponents()
    {
        for (int i = 0; i < wrongComponents.Length; i++)
        {
            wrongComponents[i].SetActive(false);
        }
    }

}
