using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : DialogBase
{
    // public UPersian.Components.RtlText numberOfFeathers;
    private GameObject uiBs;
    private void OnEnable() {
        GameObject gmGO = GameObject.Find("GameManger");
        GameObject uiPA = gmGO.transform.Find("UI PA").gameObject;
        uiBs = uiPA.transform.Find("UI").gameObject;
        if(uiBs!= null)
            uiBs.SetActive(false);
        //  numberOfFeathers.text = DataManager.Instance.GetFeather().ToString();
    }
    public void closeShop(){
        if(!uiBs.active)
            uiBs.SetActive(true);
        ViewManager.instance.closeView(this);
    }

    public void cancellExitGame()
    {
        if (ViewManager.instance.getLastView() != null)
        {
            if(!uiBs.active)
                uiBs.SetActive(true);
            ViewManager.instance.closeLastView();
        }
        else
        {
            if(!uiBs.active)
                uiBs.SetActive(true);
            ViewManager.instance.closeView(this);
        }
    }
}