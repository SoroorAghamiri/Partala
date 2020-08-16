using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpisodeSelectView : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openExitView();
        }

    }

    public void openExitView()
    {
        if (ViewManager.instance.getLastView() == null)
        {
            DialogManager.instance.showSettingView();
        }
    }
}
