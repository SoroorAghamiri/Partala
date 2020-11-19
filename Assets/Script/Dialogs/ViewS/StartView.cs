using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartView : MonoBehaviour
{
    private LevelLoader mylevelLoader;
    private void Start()
    {
        mylevelLoader = FindObjectOfType<LevelLoader>();
    }
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
        mylevelLoader.LoadLevel(SceneNames.Setting);
        //SceneManager.LoadScene(SceneNames.Setting);
    }

    public void onStartClicked()
    {
        PersistentSceneManager.instance.LoadScene(SceneNames.EpisodeSelect);
        //mylevelLoader.LoadLevel(SceneNames.EpisodeSelect);
        //SceneManager.LoadScene(SceneNames.EpisodeSelect);

    }
    public void openShop()
    {
        mylevelLoader.LoadLevel(SceneNames.Shop);
    }
}
