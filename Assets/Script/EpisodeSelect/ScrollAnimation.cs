using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{

    public RectTransform viewPort;
    public GameObject dots;
    public Color targetColor;
    public string sceneName;
     Button[] episodes;
    Image[] childImages;
    
    UPersian.Components.RtlText childTexts;
    Image[] childDots;
    Color originalColor;
    bool enableDots = false;
    // LeveManger levelMan;

    void Start()
    {
        // levelMan = GameObject.FindObjectOfType<LeveManger>();
        episodes = this.GetComponentsInChildren<Button>();
        
        if(String.Equals(sceneName , SceneNames.EpisodeSelect)){
            childDots = dots.GetComponentsInChildren<Image>();
            originalColor = childDots[0].color;
            enableDots = true;
            if (episodes.Length > 0)
            {
                foreach (Button b in episodes)
                {
                    b.interactable = false;
                }
            }
        }
        // else if(String.Equals(sceneName , SceneNames.LevelSelect1)){
            
        //     foreach (Button b in episodes)
        //     {
        //         Image[] temp = b.GetComponentsInChildren<Image>();
        //         foreach (Image im in temp)
        //         {
        //             if(im.name == "Puzzle"){
        //                 puzzles.Add(im);
        //             }
        //         }
        //     }

        //     ShowDonePuzzles();
        // }
    }

    // void ShowDonePuzzles(){
    //     var levelUnlock = DataManager.Instance.GetLevel(levelMan.currentEpisode);
    //     for (int i = 0; i < levelUnlock; i++) 
    //     {
    //         puzzles[i].color = Color.white;
    //     }
    // }
    private void Update()
    {
        if (episodes.Length > 0)
        {
            for(int i = 0 ; i < episodes.Length ; i++)
            {
                
                    
                float distance = Vector2.Distance(episodes[i].transform.position, viewPort.anchoredPosition);
                if (distance > 137 && distance < 139)
                {
                    iTween.ScaleTo(episodes[i].gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
                    
                    if(enableDots){
                        childDots[i].color = targetColor;
                    episodes[i].interactable = true;
                    }
                    
                    childTexts = episodes[i].GetComponentInChildren<UPersian.Components.RtlText>();
                    if(childTexts != null)
                        childTexts.color = new Color(1 , 1 , 1 , 1);

                    childImages = episodes[i].GetComponentsInChildren<Image>();
                    foreach (Image im in childImages)
                    {
                        if (im.gameObject.name == "lock")
                            episodes[i].interactable = false;
                    }
                }
                else if ((distance < 137 || distance > 139 )&& episodes[i].transform.localScale != Vector3.one)
                {
                    iTween.ScaleTo(episodes[i].gameObject, Vector3.one, 0.3f);

                    childTexts = episodes[i].GetComponentInChildren<UPersian.Components.RtlText>();
                    if(childTexts != null)
                        childTexts.color = new Color(1 , 1 , 1 , 0);

                    if(enableDots){
                        childDots[i].color = originalColor;
                    episodes[i].interactable = false;
                    }
                }
              
            }
        }
    }



}
