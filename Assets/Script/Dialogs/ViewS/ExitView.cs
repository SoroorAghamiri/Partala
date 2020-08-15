using System;
using UnityEngine;
using UnityEngine.UI;

public class ExitView:DialogBase{
    

    public void exitGame(){
        Application.Quit();
    }

    public void cancellExitGame(){
        ViewManager.instance.closeView(this);
    }
}