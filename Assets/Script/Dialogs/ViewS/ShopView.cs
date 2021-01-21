using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : DialogBase
{
    // public UPersian.Components.RtlText numberOfFeathers;

    private void OnEnable() {

        //  numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
    }
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