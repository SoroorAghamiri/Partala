using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeveManger : MonoBehaviour
{
    private LevelLoader mylevelLoader;
    private int currentEpisode;
    [SerializeField] private Button[] levelButtons;



    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        mylevelLoader = FindObjectOfType<LevelLoader>();
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
        if (DataManager.Instance.buildIndexOfLevelSelectors.Contains(SceneManager.GetActiveScene().buildIndex) == false)
        {
            DataManager.Instance.buildIndexOfLevelSelectors.Add(SceneManager.GetActiveScene().buildIndex);
            currentEpisode = DataManager.Instance.buildIndexOfLevelSelectors.Count;
        }
        else
        {
            for (int i = 0; i < DataManager.Instance.buildIndexOfLevelSelectors.Count; i++)
            {
                if (DataManager.Instance.buildIndexOfLevelSelectors[i] == SceneManager.GetActiveScene().buildIndex)
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
        mylevelLoader.LoadLevel(level + SceneManager.GetActiveScene().buildIndex);
    }

    public void Onback(string episode)
    {
        mylevelLoader.LoadLevel(episode);
    }

    public void SoundActive()
    {
        audioSource.Play();
    }
}
