using System;
using UnityEngine;
using UnityEngine.UI;

public class ExitView : DialogBase
{


    public void exitGame()
    {
        Application.Quit();
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