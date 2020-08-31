using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;



public class SettingScene : MonoBehaviour
{

    [Header("Code Scpecific")]
    [SerializeField] private AudioMixer masterAM;
    public Slider sfxCheck;
    public Slider musicCheck;

    private LevelLoader mylevelLoader;
    void Start()
    {
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());

        musicCheck.value = DataManager.Instance.GetMusicLevel();
        sfxCheck.value = DataManager.Instance.GetSFXLevel();
        mylevelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneNames.Start);
        }
    }
    public void onHomeClicked()
    {
        mylevelLoader.LoadLevel(SceneNames.Start);
    }

    public void onDefaultSettingClicked()
    {
        DataManager.Instance.SetSFXLevel(1f);
        DataManager.Instance.SetMusicLevel(1f);
        masterAM.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        masterAM.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
        musicCheck.value = 1f;
        sfxCheck.value = 1f;
    }

    public void onReplayClicked()
    {
        DataManager.Instance.SetEpisode(1);
        DataManager.Instance.SetLevel(1, 1);
        DataManager.Instance.SetFeather(7);
        DataManager.Instance.SetTutorial(true);
        DataManager.Instance.Save();
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

}
