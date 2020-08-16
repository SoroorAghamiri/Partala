using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingView : DialogBase
{

    public void openExitPanel()
    {
        DialogManager.instance.showSettingView();
    }
    public void cancellExitGame()
    {
        ViewManager.instance.closeView(this);
    }
}
