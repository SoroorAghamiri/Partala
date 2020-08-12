using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameSys : MonoBehaviour
{
    public static GameSys Instans;
    private const string TapsellPlusKey = "ptilftqsoihrrcgigcktijbtmtplcsdalatbcopcfrhetdggplphedgaeaabmimirdhpol";
    private const string Level = "Level";
    private const string Win = "Win";
    public bool[] WIN = new bool[30];
    private const string MusixLevel = "MusixLevel";
    private const string SfxLevel = "SfxLevel";
    private const string Feathers = "feathers";
    public bool firsTime;
    public AudioMixerGroup music;
    public AudioMixerGroup sfx;

    public bool HelpActiveter;
    //    private const string ForFirstTIm="ForFirstTIm"
    // Start is called before the first frame update
    private void Awake()
    {
        // MusicManger();
        PlayerPrefs.DeleteAll();
        MakeInstans();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            music.audioMixer.SetFloat("musicvol", GetMusicLevel());

            sfx.audioMixer.SetFloat("sfxvol", GetSfxLevel());
        }

        //PlayerPrefs.DeleteAll();


        if (!PlayerPrefs.HasKey("ForFirstTIm"))
        {
            PlayerPrefs.SetFloat(SfxLevel, 1);
            PlayerPrefs.SetInt(Level, 1);
            PlayerPrefs.SetInt(Win, 0);
            PlayerPrefs.SetInt("ForFirstTIm", 1);
            PlayerPrefs.SetFloat(MusixLevel, 1);
            PlayerPrefs.SetFloat(Feathers, 7);
            HelpActiveter = true;
            firsTime = true;
        }



    }

    public void Update()
    {

    }


    void MakeInstans()
    {
        Debug.Log("test");
        if (Instans != null)
        {
            Destroy(gameObject);

        }
        else
        {
            Instans = this;
            DontDestroyOnLoad(gameObject);
        }


    }

    public void Set_Level(int level)
    {
        PlayerPrefs.SetInt(Level, level);
    }

    public int Get_level()
    {
        return PlayerPrefs.GetInt(Level);

    }
    public void Set_Win(int win)
    {
        PlayerPrefs.SetInt(Win, win);
    }

    public int Get_Win()
    {
        return PlayerPrefs.GetInt(Win);

    }

    public void SetSfxLeve(float volume)
    {
        PlayerPrefs.SetFloat(SfxLevel, volume);
    }
    public void SetMusicLevel(float volume)
    {
        PlayerPrefs.SetFloat(MusixLevel, volume);
    }

    public float GetSfxLevel()
    {
        return PlayerPrefs.GetFloat(SfxLevel);
    }

    public float GetMusicLevel()
    {
        return PlayerPrefs.GetFloat(MusixLevel);
    }

    public void SetFeather(float feather)
    {
        PlayerPrefs.SetFloat(Feathers, feather);
    }

    public float GetFeather()
    {
        return PlayerPrefs.GetFloat(Feathers);
    }


}
