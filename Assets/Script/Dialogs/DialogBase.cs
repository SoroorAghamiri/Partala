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
    public GameObject uiBs =null;

    public void Start()
    {
        foreach (var button in closeButtons)
        {
            button.onClick.AddListener(() =>
            {
                onBackPressed();
            });
        }

        GameObject gmGO = GameObject.Find("GameManger");
        GameObject uiPA = gmGO.transform.Find("UI PA").gameObject;
        uiBs = uiPA.transform.Find("UI").gameObject;

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
        iTween.ScaleTo(dialogObject, iTween.Hash("x", 1, "y", 1, "z", 1, "time", 1.25f, "ignoretimescale", true));

        Time.timeScale = 0;
    }

    public override void closeView()
    {
        if (closeAction != null)
        {
            closeAction();
        }
        iTween.ScaleTo(dialogObject, Vector3.zero, 0.5f);
        Time.timeScale = 1;
        if(!uiBs.active){
            uiBs.SetActive(true);
        }
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