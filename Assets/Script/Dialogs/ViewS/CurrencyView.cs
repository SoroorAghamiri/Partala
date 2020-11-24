using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class CurrencyView : DialogBase
{
    private int[] firstHintDiscountInEpisodes = { 1, 2, 3 };
    private int[] secondHintDiscountInEpisodes = { 3, 4, 5 };
    // Working Params
    [Header("Currency Parameters")]
    public UPersian.Components.RtlText numberOfFeathers;
    public UPersian.Components.RtlText disc1;
    public UPersian.Components.RtlText disc2;
    public GameObject firstType;
    public GameObject alamoot;
    public GameObject alamootDig;
    
    [HideInInspector]public bool firstHint;
    [HideInInspector]public bool hintShown;
    [Header("Hints Parameters")]
    public GameObject[] wrongComponents;
    [SerializeField] public int featherDiscount1;
    [SerializeField] public int featherDiscount2;
    //Config
    [SerializeField] private float speedForScale;
    [SerializeField] private float limit = 1;

    private GameObject[] correctObjects;
    private int level;
    private int episode;
    [SerializeField] private List<string> infoIsShown;

    private LevelLoader mylevelLoader;
    // Animation anim;
    bool scalingI = false;
    [HideInInspector] public bool scalingD = false;

    void Start()
    {

        // //*These two lines are only for debuggind. Delete when you're gonna publish it.
        // if (DataManager.Instance.GetFirstInfo() == true)
        //     DataManager.Instance.SetFirstInfo(false);
        // //*Up to here

        if (!DataManager.Instance.GetFirstInfo())
        {
            firstType.SetActive(true);
        }
       
                
                // anim = alamoot.GetComponent<Animation>();
               
      
        if (!DataManager.Instance.GetEnableSecondHint())
            GameObject.Find("Ekbatan").GetComponent<Button>().interactable = false;
        initialization();

    }


    private void Update()
    {
        numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
        if(DataManager.Instance.GetTutorial()){
            if(!firstHint && !scalingI && firstType.active){
                // Debug.Log("itween must be played now");
                iTween.ScaleTo(alamoot.gameObject , iTween.Hash("x",1.3f , "y",1.3f ,"z" ,1.3f , "time" , 0.5f , "loopType" , "loop","ignoretimescale", true));
                // iTween.ScaleTo(alamoot.gameObject , iTween.Hash("x",1f , "y",1f ,"z" ,1f , "time" , 1f , "loopType" , "loop","ignoretimescale", true));
                scalingI = true;
            }
            if(scalingI && infoIsShown.Contains("AlamootInfo") && !scalingD && firstType.active){
                iTween.ScaleTo(alamootDig.gameObject , iTween.Hash("x",1.3f , "y",1.3f ,"z" ,1.3f , "time" , 0.5f , "loopType" , "loop","ignoretimescale", true));
                scalingD = true;
                // Debug.Log("currency scalingD:" + scalingD);
            }
        }
        if (!DataManager.Instance.GetFirstInfo())
        {

            if (infoIsShown.Count == 2)
            {
                DataManager.Instance.SetFirstInfo(true);
                firstType.SetActive(false);
                DataManager.Instance.Save();
            }
        }
    }
    private void initialization()
    {
        string temp = SceneManager.GetActiveScene().name;
        level = System.Convert.ToInt32(temp.Substring(temp.Length - 1));
        episode = System.Convert.ToInt32(temp.Substring(temp.Length - 3, 1));
        // print(level);
        featherDiscount1 = firstHintDiscountInEpisodes[episode - 1];
        featherDiscount2 = secondHintDiscountInEpisodes[episode - 1];
        disc1.text = featherDiscount1.ToString();
        disc2.text = featherDiscount2.ToString();
        firstHint = false;
        hintShown = false;
        wrongComponents = GameObject.FindGameObjectsWithTag("WrongComponent");
        mylevelLoader = FindObjectOfType<LevelLoader>();
        speedForScale = 10.0f;
    }

    public void callShowInfo()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        if (!infoIsShown.Contains(name))
        {
            infoIsShown.Add(name);

        }
        showInfo(name);
        if (name != "Info")
            GameObject.Find(name).SetActive(false);
    }

    public InfoView showInfo(string callingHint)
    {
        InfoView prefab = Resources.Load<InfoView>("Views/InfoPanel");
        InfoView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        dialog.transform.SetParent(this.GetComponent<RectTransform>());
        dialog.transform.localPosition = Vector3.zero;
        dialog.transform.localScale = Vector3.one;

        ViewManager.instance.openView(dialog);
        dialog.callingHint = callingHint;
        dialog.levelDisc1 = featherDiscount1;
        dialog.levelDisc2 = featherDiscount2;
        return dialog;
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
                DataManager.Instance.Save();
                numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
                ViewManager.instance.closeView(this);
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
                DataManager.Instance.Save();
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

    public void OnShopClicked()
    {
        DataManager.Instance.lastSceneIndex = SceneManager.GetActiveScene().buildIndex; //remove later
        mylevelLoader.LoadLevel(SceneNames.Shop);
    }

}
