using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private static DialogManager _instance;
    public static DialogManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("DIalogManager").AddComponent<DialogManager>();
            }
            return _instance;
        }
    }
    private static RectTransform canvasParent;

    private RectTransform getCanvasParent()
    {
        if (canvasParent == null)
        {
            RectTransform prefab = Resources.Load<RectTransform>("Prefabs/Canvas");
            canvasParent = Instantiate(prefab);
            Canvas canvas = canvasParent.GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
        }
        return canvasParent;
    }

    private void initDialog(GameObject dialog)
    {
        dialog.transform.SetParent(getCanvasParent());
        dialog.transform.localPosition = Vector3.zero;
        dialog.transform.localScale = Vector3.one;

        RectTransform rectTransform = dialog.GetComponent<RectTransform>();
        // rectTransform.SetLeft(0f);
        // rectTransform.SetRight(0f);
        // rectTransform.SetTop(0f);
        // rectTransform.SetBottom(0f);

        Resources.UnloadUnusedAssets();
    }

    //When Exit dialog added, edit the path
    public ExitView showExitView()
    {
        ExitView prefab = Resources.Load<ExitView>("Views/ExitPanel");
        ExitView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }



    //When pause dialog added, edit the path
    public PauseView showPauseView()
    {
        PauseView prefab = Resources.Load<PauseView>("Prefabs/UI PA\"");
        PauseView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }

}
