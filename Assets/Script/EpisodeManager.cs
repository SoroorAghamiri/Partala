using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpisodeManager : MonoBehaviour
{
    [SerializeField] Button[] episodeButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EpisodeOnClick(string episode)
    {
       
        this.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(episode);
    }

    public void SoundActive()
    {
        this.GetComponent<AudioSource>().Play();
    }
    
}
