/* program write by Amir Hossin Alishahi
 in 12/27/2019
 version :1.0
 */
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
//using UnityEditor.IMGUI.Controls;
using UnityEngine.Audio;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using UnityEngine.UIElements;
using System;
using GameAnalyticsSDK;


#region SoroorComments
/*
    Check for level number, if it equals the specified numbers in the design document, call showSimorghCard.
    
*/
#endregion
public class GameManger : MonoBehaviour
{
    /// <summary>
    ///Too much shit happening at the same time
    [SerializeField] private AudioMixerGroup sfx;
    [SerializeField] private AudioMixerGroup Musix;
    public static GameManger Instans;//Create Instans for GameManger class///FUCKING WHY?
    [Tooltip("collide point count ")]
    public bool Win;// win condition for game 
    public GameObject[] Colliderpoint;// get collider points 
    public string NextLevelname;
    public Button[] buttons = new Button[6];
    public Image Puase_panel;
    private bool toggel_puase = false;
    public int indx = 0;
    public bool wrongObjects;
    private bool wintoggler;
    private EggsScript myEggsScript;
    private string LevelSelect;
    [SerializeField] private int[] episodeFeather;//earned feather after every episode ending
    // [SerializeField] private Text featherText;
    public GameObject hintPanel;
    public int[][] featherAndLevelMatrix;
    private bool panelIsActive = false;

    private bool winFlagChangedByWinChecker;
    private int episodeNumber;
    private int levelNumberInEpisode;
    private GameObject[] mainComponents;

    private float timer = 0f;

    //private LevelLoader mylevelLoader;
    private int[] simorghLevels = { 5, 8, 12 };
    private int[] witchLevels = { 4, 7, 13 };
    ///Too much shit happening at the same time
    /// </summary>
    /// 

    private dynamic buildIndexofCurrent;
    public void SingleTOne()
    {
        if (Instans == null)
        {
            Instans = this;
        }
        else
        {
            Destroy(this);
        }

    }
    private void Awake()
    {
        // sfx.audioMixer.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        // Musix.audioMixer.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());

        //SingleTOne();//WHY?


    }

    void Start()
    {
        buildIndexofCurrent = PersistentSceneManager.instance.activeScene;
        if (buildIndexofCurrent is string)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(buildIndexofCurrent));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndexofCurrent));
        }
        // ShowNumberOfFeathers();
        LevelFactor();
        SettingInitialValues();
        FindCorrectEpisodeNumberAndLevel();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Episode " + episodeNumber.ToString(), "Level " + levelNumberInEpisode.ToString());

    }

    private void FindCorrectEpisodeNumberAndLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var lastEpisodeNumber = DataManager.Instance.ReturnSizeOfBuildIndexList();
        for(int i=1;i<DataManager.Instance.ReturnSizeOfBuildIndexList()-1;i++)
        {
            if(DataManager.Instance.ReturnBuildIndexByEpisodeNumber(i) < currentSceneIndex && currentSceneIndex < DataManager.Instance.ReturnBuildIndexByEpisodeNumber(i+1))
            {
                levelNumberInEpisode = currentSceneIndex - DataManager.Instance.ReturnBuildIndexByEpisodeNumber(i);
                episodeNumber = i;
                return;
            }
        }
        episodeNumber= DataManager.Instance.ReturnSizeOfBuildIndexList() -1;
        levelNumberInEpisode = currentSceneIndex - DataManager.Instance.ReturnBuildIndexByEpisodeNumber(episodeNumber);

    }

    private void SettingInitialValues()
    {
        toggel_puase = false;//pause panel flag
        myEggsScript = FindObjectOfType<EggsScript>();
        Win = false;///////////////////////////So many flags with win confusing AF should be changed
        winFlagChangedByWinChecker = false;////
        wintoggler = true;
        Colliderpoint = GameObject.FindGameObjectsWithTag("ColliderPoint");
    }

    void Update()
    {
        CountTime();
        win();
        

    }

    private void CountTime()
    {
        timer += Time.deltaTime;
    }

    //win condition 
    private void win()
    {
        if (!wrongObjects)
        {
            if (winFlagChangedByWinChecker == true && myEggsScript.CheckBothEggsAreActive())
            {

                myEggsScript.SetLastEgg();
                if (wintoggler)
                {
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Episode " + episodeNumber.ToString(), "Level " + levelNumberInEpisode.ToString());


                    if (DataManager.Instance.GetLevel(episodeNumber) == levelNumberInEpisode)
                    {
                        DataManager.Instance.SetLevel(DataManager.Instance.GetLevel(episodeNumber) + 1, episodeNumber);
                        GameAnalytics.NewDesignEvent("Episode " + episodeNumber.ToString() + ":Level " + levelNumberInEpisode.ToString(), timer);
                        DataManager.Instance.Save();

                    }
                    //Calling Cards
                    // print("level number" + levelNumberInEpisode);
                    // foreach (int ii in simorghLevels)
                    // {
                    //     if (levelNumberInEpisode == ii)
                    //     {
                    //         showSimorghCard();
                    //         break;
                    //     }
                    // }
                    // foreach (int ii in witchLevels)
                    // {
                    //     if (levelNumberInEpisode == ii)
                    //     {
                    //         showWitchCard(false);
                    //     }
                    // }
                    StartCoroutine(DeleteMainComponentObjectsAfterWin());
                    StartCoroutine(ShowFinalOBjectAndCallAd());
                    wintoggler = false;
                }


            }
        }

    }
    private IEnumerator DeleteMainComponentObjectsAfterWin()
    {
        yield return new WaitForSeconds(0.2f);

        mainComponents = GameObject.FindGameObjectsWithTag("MainComponent");
        foreach (GameObject gameObject in mainComponents)
        {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    IEnumerator ShowFinalOBjectAndCallAd()
    {

        Win = true;

        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);
        GameObject finalObjectManager = new GameObject("finalObjectManager");
        finalObjectManager.AddComponent<FinalObjectManager>();
        finalObjectManager.GetComponent<FinalObjectManager>().ShowFinalObjectAfterWin();
        yield return new WaitForSeconds(this.gameObject.GetComponent<AudioSource>().clip.length);


        if(NextLevelname.Length == 0)
        {
            Debug.Log(episodeNumber);
            AdManager.Instance.AdShow(episodeNumber - 1);
        }
            

        // if (!Array.Exists(simorghLevels, element => element == levelNumberInEpisode) || !Array.Exists(witchLevels, element => element == levelNumberInEpisode))
        next_level();
    }
    

    private void OnPlay()
    {
        if (panelIsActive == true)
        {
            Puase_panel.gameObject.SetActive(false);

            toggel_puase = false;
            buttons[0].GetComponent<AudioSource>().Play();
        }

    }




    public void GoBackToLevelSelect()
    {
        PersistentSceneManager.instance.LoadScene(LevelSelect, true);
        //mylevelLoader.LoadLevel(LevelSelect);
    }


    public void backButtonMusciFix(string LevelSelect)
    {

        this.LevelSelect = LevelSelect;
        buttons[0].gameObject.GetComponent<AudioSource>().Play();
        Invoke("GoBackToLevelSelect", buttons[0].gameObject.GetComponent<AudioSource>().clip.length);

    }
    public void next_level()
    {
        if (NextLevelname.Length == 0)
        {
            PersistentSceneManager.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, false);
        }
        else
        {
            if (episodeNumber == DataManager.Instance.GetEpisode()) 
            {
                DataManager.Instance.SetEpisode(DataManager.Instance.GetEpisode() + 1);
                DataManager.Instance.SetNewFlagForEpisodeNoAd();
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Episode " + episodeNumber.ToString());
                DataManager.Instance.Save();
            }
            DialogManager.instance.showEoE();
            // PersistentSceneManager.instance.LoadScene(NextLevelname, true);
        }

    }

    public void WrongObjectDetected()
    {
        wrongObjects = true;
    }
    public void NoWrongObjects()
    {
        wrongObjects = false;
    }


    public void LevelFactor()//this Function added feather after every episode end  
    {

        // int levelFactor = GameSys.Instans.Get_level() / 15;
        //Debug.Log(levelFactor);
        //for (int i = 1; i <= episodeNumber; i++)
        //{
        //    if (levelFactor == i)
        //    {
        // //       GameSys.Instans.SetFeather(GameSys.Instans.GetFeather() + episodeFeather[i - 1]);

        //    }
        //}
    }


    public void SetWin()
    {
        winFlagChangedByWinChecker = true;
    }

    private SimorghCard showSimorghCard()
    {
        SimorghCard prefab = Resources.Load<SimorghCard>("Cards/SimorghCard");
        SimorghCard dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);



        GameObject cc = GameObject.Find("GameManger");
        GameObject childOfCC = cc.transform.Find("UI PA").gameObject;
        GameObject cOfCOfCC = childOfCC.transform.Find("Canvas").gameObject;

        dialog.transform.SetParent(cOfCOfCC.GetComponent<RectTransform>());



        ViewManager.instance.openView(dialog);
        return dialog;
    }

    private WitchCard showWitchCard(bool empty)
    {
        WitchCard prefab = Resources.Load<WitchCard>("Cards/WitchCard");
        WitchCard dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);



        GameObject cc = GameObject.Find("GameManger");
        GameObject childOfCC = cc.transform.Find("UI PA").gameObject;
        GameObject cOfCOfCC = childOfCC.transform.Find("Canvas").gameObject;

        dialog.transform.SetParent(cOfCOfCC.GetComponent<RectTransform>());
        dialog.showEmptyCard = empty;

        ViewManager.instance.openView(dialog);
        return dialog;
    }
    public int GetEpisodeNumber()
    {
        return episodeNumber;
    }
}

