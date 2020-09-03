using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using System.Diagnostics;
public class InfoView : DialogBase
{
    Dictionary<string, string> infoText = new Dictionary<string, string>();
    // public List<UPersian.Components.RtlText> hintTexts = new List<UPersian.Components.RtlText>();
    public UPersian.Components.RtlText exp;

    public List<Image> infoPs = new List<Image>();
    public Button next;
    [HideInInspector]
    public string callingHint;
    [HideInInspector]
    public int levelDisc1;
    [HideInInspector]
    public int levelDisc2;

    // private List<string> infoIsShown;
    void Start()
    {
        if (infoText.Count == 0)
        {
            fillDictionary();
        }
        // infoIsShown = new List<string>(infoPs.Count);
        print("calling hint = " + callingHint);
        if (callingHint != "Info")
        {
            explanation(callingHint);
        }
        else
        {
            next.gameObject.SetActive(true);
            callingHint = "AlamootInfo";
            explanation(callingHint);
        }
    }



    //Generalize this view later
    private void explanation(string cHint)
    {

        foreach (Image i in infoPs)
        {
            if (i.gameObject.name == callingHint)
            {
                i.gameObject.SetActive(true);
            }
        }
        exp.text = infoText["CurrencyView" + cHint];
    }

    public void nextPage()
    {
        callingHint = "EkbatanInfo";
        explanation(callingHint);
        next.gameObject.SetActive(false);
    }

    public void closePanel()
    {
        if (!DataManager.Instance.GetFirstInfo())
        {
            foreach (Image i in infoPs)
            {
                if (i.gameObject.active)
                {
                    i.gameObject.SetActive(false);
                }
            }
        }
        Destroy(this.gameObject);
    }

    void fillDictionary()
    {
        infoText.Add("CurrencyViewAlamootInfo", "این دیگ خاصیتش اینه که جادویی ازش سر میزنه که میتونه بهت کمک کنه یکی از اشیا اضافه تو بازی رو ناپدید کنی. واسه اینکار باید " + levelDisc1 + " دونه پر طلایی سیمرغتو بهش بدی تا جادو عمل کنه.");
        infoText.Add("CurrencyViewEkbatanInfo", "این دیگ خیلی دیگ خاص و بزرگیه، جادوهایی که ازش سر میزنه جادوهای قدرتمندی هستن و باید تو استفاده ازش محتاط باشی. اگه از جادوش استفاده کنی بهت کمک میکنه شکل نهایی که میخوای درست کنی تو بازی رو بهت نشون بده ولی واسه اینکار باید " + levelDisc2 + " پر طلایی رو بهش بدی.");
        infoText.Add("CurrencyViewInfo", "New Info");
    }
}
