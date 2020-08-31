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

    private LevelLoader mylevelLoader;



    private void Start()
    {
        // DataManager.Instance.Load();
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());

        musicCheck.value = DataManager.Instance.GetMusicLevel();
        sfxCheck.value = DataManager.Instance.GetSFXLevel();

        mylevelLoader = FindObjectOfType<LevelLoader>();
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
        mylevelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }



    public void onSoundChange(float newValue)
    {

        DataManager.Instance.SetSFXLevel(newValue);
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
    }

    public void onMusicChange(float newValue)
    {

        DataManager.Instance.SetMusicLevel(newValue);
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
    }

    public void onHomeClicked()
    {
        SceneManager.LoadScene(SceneNames.Start);
    }

}
