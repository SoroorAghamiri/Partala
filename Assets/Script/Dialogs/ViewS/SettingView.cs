using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SettingView : DialogBase
{


    [SerializeField] private AudioMixer masterAM;
    // [SerializeField] private AudioMixerGroup Musix;

    private void Start()
    {
        DataManager.Instance.Load();
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());

    }
    public void openExitPanel()
    {
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

    public void onSoundChange(float newValue)
    {
        DataManager.Instance.SetSFXLevel(Mathf.Log10(newValue) * 20);
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
    }

    public void onMusicChange(float newValue)
    {
        DataManager.Instance.SetMusicLevel(newValue);
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
    }
}
