using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{

    public RectTransform viewPort;
    public GameObject dots;
    public Color targetColor;
     Button[] episodes;
    Image[] childImages;
    [SerializeField] UPersian.Components.RtlText childTexts;
    Image[] childDots;
    Color originalColor;

    void Start()
    {
        episodes = this.GetComponentsInChildren<Button>();
        if (episodes.Length > 0)
        {
            foreach (Button b in episodes)
            {
                b.interactable = false;
            }
        }
        childDots = dots.GetComponentsInChildren<Image>();
        originalColor = childDots[0].color;
    }

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
                    
                    childDots[i].color = targetColor;
                    episodes[i].interactable = true;
                    
                    childTexts = episodes[i].GetComponentInChildren<UPersian.Components.RtlText>();
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
                    childTexts.color = new Color(1 , 1 , 1 , 0);

                    childDots[i].color = originalColor;
                    episodes[i].interactable = false;
                }
              
            }
        }
    }



}
