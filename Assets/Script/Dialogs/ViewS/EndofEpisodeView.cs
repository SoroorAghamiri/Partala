using System;
using UnityEngine;
using UnityEngine.UI;

public class EndofEpisodeView : DialogBase
{
    private GameManger gm;

    private void Start() {
        gm = GameObject.FindObjectOfType<GameManger>();
    }

    public void closeEoE(){
        PersistentSceneManager.instance.LoadScene(gm.NextLevelname, true);
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