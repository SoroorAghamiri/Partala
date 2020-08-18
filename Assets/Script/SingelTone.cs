using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

#if UNITY_EDITOR
using UnityEditor;
#endif    

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SingelTone : MonoBehaviour
{
    public static SingelTone instance;
    public AudioMixer audioMixer;
    private Scene _mScene;
    private Scene _nScene;
    private bool _sceneIsChanged;
    private void Awake()
    {
        if (instance != null )
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
          
        }
        DontDestroyOnLoad(this.gameObject);
            
        #if UNITY_EDITOR
        audioMixer =(AudioMixer) AssetDatabase.LoadAssetAtPath("Assets/MainMixer.mixer",typeof(AudioMixer)); //load Mixer From Asset Folder
        #endif
        gameObject.GetComponent<AudioSource>().outputAudioMixerGroup =audioMixer.FindMatchingGroups("Music")[0]; //Get Audio Mixer Group From Audio Mixer; 
       
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
      

       
        
    }

    private void Start()
    {
         PlayMusic();
        _mScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        _mScene = SceneManager.GetActiveScene();
     
        if (_mScene.buildIndex != _nScene.buildIndex)
        {
            _sceneIsChanged = true;
        }
        _nScene = SceneManager.GetActiveScene();
        MusicManger();
      
        
      
    }
    
    public void StopMusic()
    {
        gameObject.GetComponent<AudioSource>().Stop();
      
    }

    public void PlayMusic()
    {
        gameObject.GetComponent<AudioSource>().Play();
      
    }
    public void MusicManger()
    {
        if (_sceneIsChanged)
        {
            if (SceneManager.GetActiveScene().buildIndex <= 2)
            {
                if (!this.GetComponent<AudioSource>().isPlaying)
                {
                    PlayMusic();
                }

            
            }
            else if (SceneManager.GetActiveScene().buildIndex > 2)
            {
                StopMusic();
              
            }

            _sceneIsChanged = false;
        }

    }
}
