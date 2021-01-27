using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private PlayerData playerData = new PlayerData();
    public bool debug = false;
    private string file = "player.txt";
    public int lastSceneIndex = 0;

    private AssetBundle currentAssetBundle;
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
        Load();
        SetBuildIndexesOfLevelSelectors();//Setting build indexes of Level Selectors, Just Change it Later
      //  UnlockAllLevels();
        Save();
    }

    private void SetBuildIndexesOfLevelSelectors()
    {
        playerData.buildIndexOfLevelSelectors.Clear();
        playerData.buildIndexOfLevelSelectors.Add(0);//just add this for not using the 0 index
        playerData.buildIndexOfLevelSelectors.Add(5);
        playerData.buildIndexOfLevelSelectors.Add(21);
        playerData.buildIndexOfLevelSelectors.Add(37);
    }

    private void UnlockAllLevels()
    {
        while(playerData.levelinEpisode.Count < 3)
        {
            playerData.levelinEpisode.Add(1);
        }
        for(int i=0;i<playerData.levelinEpisode.Count;i++)
        {
            playerData.levelinEpisode[i] = 15;
        }
        playerData.episode = 3;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(playerData);
        WriteToFile(file, json);
    }


    public void Load()
    {
        if (playerData.initialized)
            return;
        playerData = new PlayerData();
        playerData.initialized = true;
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

    public int GetLevel(int episode) //Works With Episode Itself Should Think of A Standard Way of Doing This
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

    public void SetFirstInfo(bool firstInfo)
    {
        playerData.firstInfoIsShown = firstInfo;
    }
    public bool GetFirstInfo()
    {
        return playerData.firstInfoIsShown;
    }

    public bool GetEnableSecondHint()
    {
        return playerData.enableSecondHint;
    }

    public void SetEnableSecondHint(bool enableSecondHint)
    {
        playerData.enableSecondHint = enableSecondHint;
    }
    public void SetNoAdActive()
    {
        playerData.noAdForGame = true;
    }
    public bool GetnoAdflag()
    {
        return playerData.noAdForGame;
    }
    public void SetNewFlagForEpisodeNoAd()
    {
        playerData.noAdForEachEpisode.Add(false);
    }
    public void SetNoAdForGivenEpisode(int indexOfEpisode)
    {
        playerData.noAdForEachEpisode[indexOfEpisode] = true;
    }
    public bool GetNoAdForGivenEpisode(int indexOfEpisode)//works with episode index (episode number -1)
    {
        return playerData.noAdForEachEpisode[indexOfEpisode];
    }
    public bool IfListofEpisodeFlagsAreNotInitialized()
    {
        if (playerData.noAdForEachEpisode.Count == 0)
            return true;
        else
            return false;
    }
    public int ReturnSizeOfBuildIndexList()
    {
        return playerData.buildIndexOfLevelSelectors.Count;
    }
    public void AddbuildIndexToListOfBuildIndex(int buildIndex)
    {
        playerData.buildIndexOfLevelSelectors.Add(buildIndex);
        return;
    }
    public int ReturnBuildIndexByEpisodeNumber(int episodeNumber)
    {
        return playerData.buildIndexOfLevelSelectors[episodeNumber];
    }
    public void SetBuildIndexByEpisodeNumber(int episodeNumber, int buildIndex)
    {
        playerData.buildIndexOfLevelSelectors[episodeNumber] = buildIndex;
        return;
    }
    //Saves On pausing for mobile Obviously
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }


    public void SetAssetBundle(AssetBundle assetBundle)
    {
        currentAssetBundle = assetBundle;
    }

    public AssetBundle GetAssetBundle()
    {
        return currentAssetBundle;
    }
}

