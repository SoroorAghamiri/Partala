using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartView : MonoBehaviour
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
            DialogManager.instance.showExitView();
        }
    }

    public void openSettingScene()
    {
        SceneManager.LoadScene(SceneNames.Setting);
    }

    public void onStartClicked()
    {
        SceneManager.LoadScene(SceneNames.EpisodeSelect);

    }
}
