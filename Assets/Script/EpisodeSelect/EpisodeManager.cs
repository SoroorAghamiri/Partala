using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{
    private LevelLoader mylevelLoader;
    [SerializeField] Button[] episodeButtons;


    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.GetEpisode() == 0)
        {
            DataManager.Instance.SetEpisode(1);
        }
        mylevelLoader = FindObjectOfType<LevelLoader>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EpisodeOnClick(string episode)
    {

        this.GetComponent<AudioSource>().Play();
        mylevelLoader.LoadLevel(episode);
    }

    public void SoundActive()
    {
        this.GetComponent<AudioSource>().Play();
    }



}
