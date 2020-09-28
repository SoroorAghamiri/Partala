using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class DialogBase : ViewObject
{
    [Header("Gloabal Configs")]
    public GameObject dialogObject;
    public List<Button> closeButtons = new List<Button>();

    // public Image blurImage;
    //public GameObject DialogContents;
    public bool canClose = true;


    public void Start()
    {
        foreach (var button in closeButtons)
        {
            button.onClick.AddListener(() =>
            {
                onBackPressed();
            });
        }



    }
    public virtual void prepare()
    {

    }

    public override void openView()
    {
        prepare();
        if (openAction != null)
        {
            openAction();
        }
        dialogObject.SetActive(true);
        // blurImage.gameObject.SetActive(true);
        dialogObject.transform.localScale = Vector3.zero * 0.8f;
        iTween.ScaleTo(dialogObject, Vector3.one, 1.5f);
    }

    public override void closeView()
    {
        if (closeAction != null)
        {
            closeAction();
        }
        iTween.ScaleTo(dialogObject, Vector3.zero, 0.5f);
        Destroy(this.gameObject);
        // StartCoroutine(ExecuteAfterTime(0.5f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);
    }

    public override void onBackPressed()
    {
        if (canClose)
        {
            ViewManager.instance.closeLastView();
        }
    }



}