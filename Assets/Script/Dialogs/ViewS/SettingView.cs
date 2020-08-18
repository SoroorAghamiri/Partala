using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingView : DialogBase
{

    [Header("Code Scpecific")]
    [SerializeField] private AudioMixer masterAM;
    public Slider sfxCheck;
    public Slider musicCheck;




    private void Start()
    {
        // DataManager.Instance.Load();
        // masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        // masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
        //musicCheck.value = DataManager.Instance.GetMusicLevel();
        //sfxCheck.value= DataManager.Instance.GetSFXLevel();
    }



    public void openExitPanel()
    {
        ViewManager.instance.closeView(this);
        DialogManager.instance.showExitView();
    }
    public void cancellExitGame()
    {
        ViewManager.instance.closeView(this);
    }

    public void reloadScene()
    {
        ViewManager.instance.closeLastView();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //Date manager does not work currently because it can't find the json file
    //Get date from DateManager Later
    public void onSoundChange(float newValue)
    {

        // DataManager.Instance.SetSFXLevel(Mathf.Log10(newValue) * 20);
        masterAM.SetFloat("sfxvol", Mathf.Log10(newValue) * 20);//DataManager.Instance.GetSFXLevel()
    }

    public void onMusicChange(float newValue)
    {

        // DataManager.Instance.SetMusicLevel(newValue);
        masterAM.SetFloat("musicvol", Mathf.Log10(newValue) * 20);//DataManager.Instance.GetMusicLevel()
    }

    public void onHomeClicked()
    {
        SceneManager.LoadScene(SceneNames.Start);
    }

}
