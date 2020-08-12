using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HintScript : MonoBehaviour
{    
    // Working Params
    public bool firstHint;
    public bool hintShown;
    public GameObject[] wrongComponents;
    public GameObject finalObject;
    [SerializeField] public int featherDiscount1;
    [SerializeField] public int featherDiscount2;
    //Config
    [SerializeField] private float speedForScale;
    [SerializeField] private float limit =1;
    public static HintScript hintscript;
    /// <summary>
    /// Remove Later
    /// </summary>
    private GameObject[] correctObjects;

    void Start()
    {
        finalObject = GameObject.FindGameObjectWithTag("Final");
        firstHint = false;
        hintShown = false;
        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
    }
    private void Awake()
    {
        if (hintscript == null)
        {
            hintscript = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hintShown)
        {
            if (Input.touchCount > 0)
            {
                finalObject.transform.localScale=new Vector3(0,0,0);
                hintShown = false;
            }
            
        }
    }

  
    
    public void FirstHintButton()//button for First Hint 
    {
        if (!firstHint )
        {
            if (GameSys.Instans.GetFeather() >= 0 && (GameSys.Instans.GetFeather()-featherDiscount1) >= 0 )
               
            {
                DeactivateWrongComponents();
                firstHint = true;
                GameSys.Instans.SetFeather(GameSys.Instans.GetFeather() - featherDiscount1);
                GameManger.Instans.ShowNumberOfFeather();
            }
        }
    }

    public void SecondHintButton()//Button for Second Hint 
    {Debug.Log("WORKING");
        if (!hintShown )
        {
            if (GameSys.Instans.GetFeather() >= 0 && (GameSys.Instans.GetFeather() - featherDiscount2)>=0)
            {
               // Debug.Log(GameSys.Instans.GetFeather() - featherDiscount2);
                finalObject.SetActive(true);
                StartCoroutine(ShowFinalObject());
                StartCoroutine(MakeFinalObjectHidden());
                hintShown = true;
                GameSys.Instans.SetFeather(GameSys.Instans.GetFeather() - featherDiscount2);
                GameManger.Instans.ShowNumberOfFeather();
                GameManger.Instans.HintPanelColse();
                Debug.Log("WORKING");
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
        wrongComponents = GameObject.FindGameObjectsWithTag("WrongComponent");
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
        for(int i=0;i<correctObjects.Length;i++)
        {
            correctObjects[i].SetActive(false);
        }
        for(int i=0;i<wrongComponents.Length;i++)
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
