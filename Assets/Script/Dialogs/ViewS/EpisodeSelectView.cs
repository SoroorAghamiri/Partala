using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeSelectView : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openSettingView();
        }

    }

    public void openPauseView()
    {
        if (ViewManager.instance.getLastView() == null)
        {
            DialogManager.instance.showPauseView();
        }
    }

    public void openSettingView()
    {
        if (ViewManager.instance.getLastView() == null)
        {
            DialogManager.instance.showSettingView();
        }
    }
}
