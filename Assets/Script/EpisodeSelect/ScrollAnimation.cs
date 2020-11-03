using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{

    public RectTransform viewPort;
    [SerializeField] Button[] episodes;
    Image[] childImages;


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
    }

    private void Update()
    {
        if (episodes.Length > 0)
        {
            foreach (Button b in episodes)
            {

                float distance = Vector2.Distance(b.transform.position, viewPort.anchoredPosition);
                if (distance > 137 && distance < 139)
                {
                    iTween.ScaleTo(b.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.3f);
                    b.interactable = true;
                    childImages = b.GetComponentsInChildren<Image>();
                    foreach (Image i in childImages)
                    {
                        if (i.gameObject.name == "lock")
                            b.interactable = false;
                    }
                }
                else if (distance < 137 && b.transform.localScale != Vector3.one)
                {
                    iTween.ScaleTo(b.gameObject, Vector3.one, 0.3f);
                    b.interactable = false;
                }
            }
        }
    }



}
