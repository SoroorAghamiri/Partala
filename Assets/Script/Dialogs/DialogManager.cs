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
                _instance = new GameObject("DialogManager").AddComponent<DialogManager>();
            }
            return _instance;
        }
    }
    private static RectTransform canvasParent;

    private RectTransform getCanvasParent()
    {
        if (canvasParent == null)
        {
            GameObject cc;
            cc = GameObject.Find("Canvas");
            if (cc != null)
            {
                print("Found Canvas");
                canvasParent = cc.GetComponent<RectTransform>();
            }

            else
            {
                print("Did Not Find Canvas");

                cc = GameObject.Find("GameManger");
                GameObject childOfCC = cc.transform.Find("UI PA").gameObject;
                GameObject cOfCOfCC = childOfCC.transform.Find("Canvas").gameObject;

                canvasParent = cOfCOfCC.GetComponent<RectTransform>();
            }

        }
        return canvasParent;
    }

    private void initDialog(GameObject dialog)
    {
        dialog.transform.SetParent(getCanvasParent().transform , false); //If anything was messed up, remove false value
        dialog.transform.localPosition = Vector3.zero;
        dialog.transform.localScale = Vector3.one;

        RectTransform rectTransform = dialog.GetComponent<RectTransform>();
      

        Resources.UnloadUnusedAssets();
    }

    private Image blurImg;

    public void showBlur()
    {

        blurImg = Resources.Load<Image>("Views/BLUR");
        Image prefab = Instantiate(blurImg, Vector3.zero, Camera.main.transform.rotation);

        prefab.gameObject.transform.SetParent(getCanvasParent(), false);

        prefab.gameObject.transform.localPosition = Vector3.zero;
        prefab.rectTransform.anchoredPosition = Vector3.zero;

    }
    public ExitView showExitView()
    {
        showBlur();
        ExitView prefab = Resources.Load<ExitView>("Views/ExitPanel");
        ExitView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }

    public SettingView showSettingView()
    {
        showBlur();
        SettingView prefab = Resources.Load<SettingView>("Views/SettingPanel");
        SettingView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }



    public CurrencyView showCurrencyView()
    {
        showBlur();
        CurrencyView prefab = Resources.Load<CurrencyView>("Views/CurrencyPanel");
        CurrencyView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }

    public EndofEpisodeView showEoE(){
        showBlur();
        EndofEpisodeView prefab = Resources.Load<EndofEpisodeView>("Views/EndofEpisode");
        EndofEpisodeView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }

    public ReplayView showReplayView(){
        showBlur();
        ReplayView prefab = Resources.Load<ReplayView>("Views/ReplayPanel");
        ReplayView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        initDialog(dialog.gameObject);

        ViewManager.instance.openView(dialog);
        return dialog;
    }

     public ShopView showShop(){
        ShopView prefab = Resources.Load<ShopView>("Views/ShopPanel");
        ShopView dialog = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation);
        // dialog.transform.SetParent(this.GetComponent<RectTransform>());
        initDialog(dialog.gameObject);
     
        ViewManager.instance.openView(dialog);
        
        return dialog;
    }

}
