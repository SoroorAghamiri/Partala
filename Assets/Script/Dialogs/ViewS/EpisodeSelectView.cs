using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EpisodeSelectView : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            openSettingView();
        }

    }

    // public void openPauseView()
    // {
    //     if (ViewManager.instance.getLastView() == null)
    //     {
    //         DialogManager.instance.showPauseView();
    //     }
    // }

    private void playAudioSource()
    {
        EventSystem.current.currentSelectedGameObject.GetComponent<AudioSource>().Play();
    }
    public void openSettingView()
    {
        playAudioSource();
        if (ViewManager.instance.getLastView() == null)
        {
            DialogManager.instance.showSettingView();
        }
    }

    public void openCurrencyView()
    {
        playAudioSource();

        if (ViewManager.instance.getLastView() == null)
        {
            DialogManager.instance.showCurrencyView();
        }
    }
}
