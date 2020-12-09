﻿using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeveManger : MonoBehaviour
{
    [HideInInspector] public int currentEpisode;
    [SerializeField] private Button[] levelButtons;

    private dynamic buildIndexofCurrent;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        buildIndexofCurrent = PersistentSceneManager.instance.activeScene;
        if (buildIndexofCurrent is string)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(buildIndexofCurrent));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndexofCurrent));
        }
        
        if(buildIndexofCurrent is string)
        {
            buildIndexofCurrent = SceneManager.GetSceneByName(buildIndexofCurrent).buildIndex;
        }
        FindCurrentEpisode();
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;

        }
        UnlockLevelsTillPlayerProgesss();

    }

    private void FindCurrentEpisode()
    {
        if (DataManager.Instance.buildIndexOfLevelSelectors.Contains(buildIndexofCurrent) == false)
        {
            DataManager.Instance.buildIndexOfLevelSelectors.Add(buildIndexofCurrent);
            currentEpisode = DataManager.Instance.buildIndexOfLevelSelectors.Count;
        }
        else
        {
            for (int i = 0; i < DataManager.Instance.buildIndexOfLevelSelectors.Count; i++)
            {
                if (DataManager.Instance.buildIndexOfLevelSelectors[i] == buildIndexofCurrent)
                {
                    currentEpisode = i + 1;
                    break;
                }
            }
        }
    }

    public void UnlockLevelsTillPlayerProgesss()
    {
        var levelUnlock = DataManager.Instance.GetLevel(currentEpisode);
        for (int i = 0; i < levelUnlock; i++) 
        {
            levelButtons[i].interactable = true;
        }
    }


    public void LevelOnClick(int level)
    {
        audioSource.Play();
        PersistentSceneManager.instance.LoadScene(level + buildIndexofCurrent,false);
        //mylevelLoader.LoadLevel(level + SceneManager.GetActiveScene().buildIndex);
    }

    public void Onback(string episode)
    {
        PersistentSceneManager.instance.LoadScene(episode, false);
        //mylevelLoader.LoadLevel(episode);
    }

    public void SoundActive()
    {
        audioSource.Play();
    }
}
