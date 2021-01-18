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
        PersistentSceneManager.instance.LoadScene(SceneNames.Setting,true);
        //mylevelLoader.LoadLevel(SceneNames.Setting);
        
    }

    public void onStartClicked()
    {
        PersistentSceneManager.instance.LoadScene(SceneNames.EpisodeSelect,true);
        //mylevelLoader.LoadLevel(SceneNames.EpisodeSelect);

    }
    public void openShop()
    {
        PersistentSceneManager.instance.LoadScene(SceneNames.Shop, true);
        //mylevelLoader.LoadLevel(SceneNames.Shop);
    }
    public void OpenTestLevel(int buildIndex)
    {
        PersistentSceneManager.instance.LoadScene(buildIndex, false);
    }
}
