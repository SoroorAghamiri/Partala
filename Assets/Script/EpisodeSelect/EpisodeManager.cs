using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{
    [SerializeField] Button[] episodeButtons;
    private dynamic buildIndexofCurrent;

    // Start is called before the first frame update
    void Start()
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

        if (DataManager.Instance.GetEpisode() == 0)
        {
            DataManager.Instance.SetEpisode(1);
        }
        for(int i=0;i<episodeButtons.Length;i++)
        {
            episodeButtons[i].interactable = false;
        }
        for(int i=0;i<DataManager.Instance.GetEpisode();i++)
        {
            episodeButtons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EpisodeOnClick(string episode)
    {

        this.GetComponent<AudioSource>().Play();
        PersistentSceneManager.instance.LoadScene(episode,true);
       // mylevelLoader.LoadLevel(episode);
    }

    public void SoundActive()
    {
        this.GetComponent<AudioSource>().Play();
    }



}
