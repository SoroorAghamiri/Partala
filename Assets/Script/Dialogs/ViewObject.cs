using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ViewObject : MonoBehaviour
{
    [Header("Base Config")]
    public VIEW_TYPE viewType;
    public abstract void openView();
    public abstract void closeView();
    // public abstract void restartScene();
    public abstract void onBackPressed();

    public Action openAction;
    public Action closeAction;

}

public enum VIEW_TYPE
{
    dialog,
    scene
}
