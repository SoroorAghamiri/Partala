using System;
using UnityEngine;
using UnityEngine.UI;

public class ReplayView : DialogBase
{

    public void onNoClicked(){
        ViewManager.instance.closeView(this);
    
    }
    public void onYesClicked(){
         DataManager.Instance.SetEpisode(1);
        DataManager.Instance.SetLevel(1, 1);
        DataManager.Instance.SetFeather(7);
        DataManager.Instance.SetTutorial(true);
        DataManager.Instance.SetFirstInfo(false);
        DataManager.Instance.Save();
        ViewManager.instance.closeView(this);
    }

    public void cancellExitGame()
    {
        if (ViewManager.instance.getLastView() != null)
        {

            ViewManager.instance.closeLastView();
        }
        else
        {

            ViewManager.instance.closeView(this);

        }
    }
}