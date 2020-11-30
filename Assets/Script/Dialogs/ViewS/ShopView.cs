using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : DialogBase
{

    public void closeShop(){
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