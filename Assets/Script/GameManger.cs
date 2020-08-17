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
    public bool[] Check;
    public WinChecker1Object winCheck1;
    public WinCheckerMoreThan2Objects winCheck2;
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
    public Text featheText;
    public GameObject hintPanel;
    public int[][] featherAndLevelMatrix;
    private bool panelIsActive = false;

    private bool winFlagChangedByWinChecker;
    private int episodeNumber;
    private int levelNumberInEpisode;
    private GameObject[] mainComponents;
    ///Too much shit happening at the same time
    /// </summary>
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
        sfx.audioMixer.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        Musix.audioMixer.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());

        SingleTOne();//WHY?


    }

    void Start()
    {
        ShowNumberOfFeathers();
        LevelFactor();
        SettingInitialValues();
        AddingListenersToButtons();
        FindCorrectEpisodeNumberAndLevel();
    }

    private void FindCorrectEpisodeNumberAndLevel()
    {

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < DataManager.Instance.buildIndexOfLevelSelectors.Count; i++)
        {
            if (DataManager.Instance.buildIndexOfLevelSelectors[i] > currentSceneIndex)
            {
                levelNumberInEpisode = currentSceneIndex - DataManager.Instance.buildIndexOfLevelSelectors[i];
                episodeNumber = i + 1;
                break;
            }
        }

    }

    private void SettingInitialValues()
    {
        toggel_puase = false;//pause panel flag
        myEggsScript = FindObjectOfType<EggsScript>();
        Win = false;///////////////////////////So many flags with win confusing AF should be changed
        winFlagChangedByWinChecker = false;////
        Check = new bool[Colliderpoint.Length];//////////Another branch of wincheck that shouldn't be here
        wintoggler = true;
        Colliderpoint = GameObject.FindGameObjectsWithTag("ColliderPoint");
    }

    private void AddingListenersToButtons()
    {
        // buttons[0].onClick.AddListener(Onpause);/////Fatal
        buttons[1].onClick.AddListener(OnPlay);
        buttons[2].onClick.AddListener(OnHome);
        buttons[3].onClick.AddListener(OnRestart);
        buttons[4].onClick.AddListener(OnMusic);
        buttons[5].onClick.AddListener(OnSound);
    }

    void Update()
    {
        win();

    }
    //win condition 
    private void win()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex - episodeNumber;
        if (!wrongObjects)
        {
            int i;
            bool flag = true;//flag for win dection
            for (i = 0; i < Check.Length; i++)
            {
                //check if check have false condition
                if (Check[i] != true)
                {
                    flag = false;
                    break;
                }

            }
            if (flag == true && winFlagChangedByWinChecker == true)
            {
                myEggsScript.SetLastEgg();
                if (wintoggler)
                {
                    if (DataManager.Instance.GetLevel(episodeNumber - 1) == levelNumberInEpisode)
                    {
                        DataManager.Instance.SetLevel(DataManager.Instance.GetLevel(0) + 1, episodeNumber - 1);
                    }
                    StartCoroutine(DeleteMainComponentObjectsAfterWin());
                    StartCoroutine(Wait());
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

    IEnumerator Wait()
    {

        Win = true;

        this.gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(HintScript.hintscript.ShowFinalObject());
        yield return new WaitForSeconds(this.gameObject.GetComponent<AudioSource>().clip.length);
        next_level();
    }
    /////Fatal
    private void Onpause()
    {
        if (toggel_puase == false && panelIsActive == false)
        {
            PanelActivation();
            Puase_panel.gameObject.SetActive(true);
            toggel_puase = true;
            Debug.Log(toggel_puase);
            buttons[0].GetComponent<AudioSource>().Play();


        }
        else if (toggel_puase == true && panelIsActive == true)
        {
            PanelDeactivation();
            Puase_panel.gameObject.SetActive(false);
            toggel_puase = false;
            buttons[0].GetComponent<AudioSource>().Play();
        }




    }

    private void PanelDeactivation()
    {
        panelIsActive = false;
        Time.timeScale = 1;/////Fatal
    }

    private void PanelActivation()
    {
        panelIsActive = true;
        Time.timeScale = 0;/////Fatal
    }

    private void OnPlay()
    {
        if (panelIsActive == true)
        {
            Puase_panel.gameObject.SetActive(false);
            PanelDeactivation();
            toggel_puase = false;
            buttons[0].GetComponent<AudioSource>().Play();
        }

    }
    private void OnRestart()
    {
        PanelDeactivation();
        buttons[2].GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    public void OnHome()
    {
        PanelDeactivation();
        buttons[2].gameObject.GetComponent<AudioSource>().Play();
        Invoke("OnHome", buttons[2].gameObject.GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene(1);
    }

    public void GoBackToLevelSelect()
    {
        SceneManager.LoadScene(LevelSelect);
    }


    public void backButtonMusciFix(string LevelSelect)
    {

        this.LevelSelect = LevelSelect;
        buttons[0].gameObject.GetComponent<AudioSource>().Play();
        Invoke("GoBackToLevelSelect", buttons[0].gameObject.GetComponent<AudioSource>().clip.length);

    }
    private void next_level()
    {
        if (NextLevelname.Length == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(NextLevelname);

        }

    }
    private void OnSound()
    {
        SliderController.Instans.MakeSfxSliderValueZero();

    }
    private void OnMusic()
    {
        SliderController.Instans.MakeMusicSliderValueZero();
    }
    public void WrongObjectDetected()
    {
        wrongObjects = true;
    }
    public void NoWrongObjects()
    {
        wrongObjects = false;
    }
    public void SetSFXLevel(float volume)
    {
        DataManager.Instance.SetSFXLevel(volume);
        sfx.audioMixer.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
    }
    public void SetMusicLevel(float volume)
    {
        DataManager.Instance.SetMusicLevel(volume);
        Musix.audioMixer.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
    }

    public void ShowNumberOfFeathers()
    {
        featheText.text = DataManager.Instance.GetFeather().ToString();

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
    public void HintPanelOpen()
    {
        if (panelIsActive == false)
        {
            PanelActivation();
            buttons[7].gameObject.GetComponent<AudioSource>().Play();
            hintPanel.gameObject.SetActive(true);
        }

    }

    public void HintPanelColse()
    {
        if (panelIsActive == true)
        {
            PanelDeactivation();
            hintPanel.gameObject.SetActive(false);
        }

    }

    public void OnShop()
    {
        //TODO:OpenShopPanel;
    }


    public void SetWin()
    {
        winFlagChangedByWinChecker = true;
    }

}

