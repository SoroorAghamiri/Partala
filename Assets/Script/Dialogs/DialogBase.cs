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

    public List<Button> restartButton = new List<Button>();
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

        if (restartButton.Count > 0)
        {
            foreach (var button in restartButton)
            {
                button.onClick.AddListener(() =>
                {
                    restartScene();
                });
            }
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
        //DialogContents.transform.localScale = Vector3.one * 0.8f;
        //        iTween.ScaleTo(DialogContents, Vector3.one, 1);
    }
    public override void closeView()
    {
        if (closeAction != null)
        {
            closeAction();
        }
        Destroy(this.gameObject);
    }

    public override void onBackPressed()
    {
        if (canClose)
        {
            ViewManager.instance.closeLastView();
        }
    }

    public override void restartScene()
    {
        print("Current scene name = " + SceneManager.GetActiveScene().name);
        if (ViewManager.instance.getLastView() != null)
        {
            ViewManager.instance.closeLastView();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}