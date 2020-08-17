using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameSys : MonoBehaviour
{
    [Header("Change for the number of Episodes")]
    [SerializeField] private int totalNumberOfEpisodes;
    // public static GameSys Instance { get; private set; }
    private static GameSys _instance;

    public AudioMixerGroup music;
    public AudioMixerGroup sfx;

    // Start is called before the first frame update
    // private void Awake()
    // {
    //     if (Instance != null)
    //     {
    //         Destroy(gameObject);

    //     }
    //     else
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }

    // }

    public static GameSys Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameSys").AddComponent<GameSys>();
            }
            return _instance;
        }
    }


    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DataManager.Instance.Load();
            music.audioMixer.SetFloat("musicvol", DataManager.Instance.GetMusicLevel());
            sfx.audioMixer.SetFloat("sfxvol", DataManager.Instance.GetSFXLevel());
        }
    }
}
