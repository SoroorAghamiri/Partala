using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameAnalyticsSDK.Setup;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private PlayerData playerData = new PlayerData();
    public bool debug = false;
    public List<int> buildIndexOfLevelSelectors = new List<int>();
    private string file = "player.txt";

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }




    public void Save()
    {
        string json = JsonUtility.ToJson(playerData);
        WriteToFile(file, json);
    }


    public void Load()
    {
        playerData = new PlayerData();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, playerData);
    }
    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.Log("File Not Found");
            return "";
        }
    }
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }


    public float GetMusicLevel()
    {
        return playerData.musicLevel;
    }
    public float GetSFXLevel()
    {
        return playerData.sfxLevel;
    }

    public void SetMusicLevel(float musicLevel)
    {
        playerData.musicLevel = musicLevel;
    }

    public void SetSFXLevel(float sfxLevel)
    {
        playerData.sfxLevel = sfxLevel;
    }
    public void SetEpisode(int episode)
    {
        playerData.episode = episode;
        playerData.levelinEpisode.Add(1);
    }

    public void SetLevel(int level, int episode)
    {
        playerData.levelinEpisode[episode - 1] = level;
    }

    public void SetFeather(int feather)
    {
        playerData.feather = feather;
    }

    public void SetTutorial(bool tutorial)
    {
        playerData.tutorial = tutorial;
    }

    public int GetEpisode()
    {
        return playerData.episode;
    }

    public int GetLevel(int episode)
    {
        return playerData.levelinEpisode[episode - 1];
    }

    public bool GetTutorial()
    {
        return playerData.tutorial;
    }
    public int GetFeather()
    {
        return playerData.feather;
    }
    //Saves On pausing for mobile Obviously
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }

}

