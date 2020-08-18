using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeveManger : MonoBehaviour
{
    
    [SerializeField] private int currentEpisode;
    [SerializeField] private Button[] levelButtons;



    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        if(DataManager.Instance.buildIndexOfLevelSelectors.Contains(SceneManager.GetActiveScene().buildIndex)==false)
        {
            DataManager.Instance.buildIndexOfLevelSelectors.Add(SceneManager.GetActiveScene().buildIndex);
            currentEpisode = DataManager.Instance.buildIndexOfLevelSelectors.Count;

        }
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;

        }
        UnlockLevelsTillPlayerProgesss();


    }



    public void UnlockLevelsTillPlayerProgesss()
    {
        for (int i = 0; i < DataManager.Instance.GetLevel(currentEpisode); i++) 
        {
            levelButtons[i].interactable = true;
        }
    }


    public void LevelOnClick(int level)
    {

        audioSource.Play();
        SceneManager.LoadScene(level + SceneManager.GetActiveScene().buildIndex);

    }

    public void Onback(string episode)
    {
        SceneManager.LoadScene(episode);
    }

    public void SoundActive()
    {
        audioSource.Play();
    }
}
