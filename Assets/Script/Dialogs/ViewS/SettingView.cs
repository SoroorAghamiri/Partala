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
        float firstMusicValue = DataManager.Instance.GetMusicLevel();
        float firstSfxValue = DataManager.Instance.GetSFXLevel();

        masterAM.SetFloat("sfxvol", firstMusicValue);
        masterAM.SetFloat("musicvol", firstSfxValue);

        musicCheck.value = Mathf.Pow(10, (firstMusicValue / 20));
        sfxCheck.value = Mathf.Pow(10, (firstSfxValue / 20));

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
        PersistentSceneManager.instance.LoadScene(SceneManager.GetActiveScene().name, false);
        //mylevelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }



    public void onSoundChange(float newValue)
    {

        DataManager.Instance.SetSFXLevel(Mathf.Log10(newValue) * 20);
        DataManager.Instance.Save();
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
    }

    public void onMusicChange(float newValue)
    {

        DataManager.Instance.SetMusicLevel(Mathf.Log10(newValue) * 20);
        DataManager.Instance.Save();
        print("Music value : " + DataManager.Instance.GetMusicLevel());
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
    }

    public void onHomeClicked()
    {
        ViewManager.instance.closeLastView();
        PersistentSceneManager.instance.LoadScene(SceneNames.Start, true);
        
       // SceneManager.LoadScene(SceneNames.Start);
    }

}
