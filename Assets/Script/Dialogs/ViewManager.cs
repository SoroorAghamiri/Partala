using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewManager : MonoBehaviour
{
    private static ViewManager _instance;
    public static ViewManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("ViewManager").AddComponent<ViewManager>();
            }
            return _instance;
        }
    }

    private viewStackManagement viewStackManagement;
    public Tools.Timer timer = new Tools.Timer();
    private bool canclose = true;

    void Awake()
    {
        this.viewStackManagement = new viewStackManagement();
    }

    public void registerView(ViewObject view)
    {
        print("register view " + view.gameObject.name);
        viewStackManagement.pushToStack(view);
    }

    public void openView(ViewObject view, Action openAction = null, Action closeAction = null)
    {
        // print("open view " + view.name);
        if (getLastView() != view)
        {
            viewStackManagement.pushToStack(view);

            view.openView();

            if (openAction != null)
            {
                openAction();
            }
            view.openAction = openAction;
            view.closeAction = closeAction;

            canclose = false;
            timer.addMiliSecondAction(50, () =>
            {
                canclose = true;
            });
        }
    }
    public void closeLastView()
    {
        if (canclose)
        {
            ViewObject oldView = viewStackManagement.popFromStack();
            ViewObject view = viewStackManagement.getLastView();

            // print("Popped from stack" + oldView.name);

            if (oldView != null)
                oldView.closeView();
            if (view != null)
            {
                view.openView();
                // print("lastView " + view.name);
            }
        }
    }
    public void closeView(ViewObject view)
    {
        if (canclose)
        {
            // print("close view " + view.name);
            view.closeView();
            viewStackManagement.removeView(view);
        }
    }

    public void removeLastHistory()
    {
        ViewObject oldView = viewStackManagement.popFromStack();
    }

    public ViewObject getLastView()
    {
        return viewStackManagement.getLastView();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (getLastView() != null)
            {
                // print("back on " + getLastView().name);
                getLastView().onBackPressed();
            }
        }
    }
}