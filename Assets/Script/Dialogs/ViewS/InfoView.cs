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
    public string callingHint;
    public List<Image> infoPs = new List<Image>();
    private List<string> infoIsShown;
    void Start()
    {
        if (infoText.Count == 0)
        {
            fillDictionary();
        }
        infoIsShown = new List<string>(infoPs.Count);
        print("calling hint = " + callingHint);
        explanation(callingHint);
    }


    //Generalize this view later
    private void explanation(string cHint)
    {
        // for (int i = 0; i < hintTexts.Count; i++)
        // {
        //     hintTexts[i].text = infoText["CurrencyViewHint" + (i + 1).ToString()];
        // }
        if (!DataManager.Instance.GetFirstInfo())
        {
            foreach (Image i in infoPs)
            {
                if (i.gameObject.name == callingHint)
                {
                    i.gameObject.SetActive(true);
                    infoIsShown.Add(i.gameObject.name);
                }
            }
        }
        exp.text = infoText["CurrencyView" + cHint];
    }

    // public void showExplanation()
    // {
    //     string name = EventSystem.current.currentSelectedGameObject.name;
    //     explanation(name);
    // }

    public void closePanel()
    {
        if (!DataManager.Instance.GetFirstInfo())
        {
            int j = 0;
            foreach (Image i in infoPs)
            {
                if (i.gameObject.active)
                {
                    i.gameObject.SetActive(false);
                }
                if (infoIsShown.Contains(i.gameObject.name))
                    j++;
            }
            if (j == infoIsShown.Count)
            {
                DataManager.Instance.SetFirstInfo(true);
                DataManager.Instance.Save();
            }
        }
        Destroy(this.gameObject);
    }

    void fillDictionary()
    {
        infoText.Add("CurrencyViewAlamootInfo", "این دیگ خاصیتش اینه که جادویی ازش سر میزنه که میتونه بهت کمک کنه یکی از اشیا اضافه تو بازی رو ناپدید کنی. واسه اینکار باید یه دونه پر طلایی سیمرغتو بهش بدی تا جادو عمل کنه.");
        infoText.Add("CurrencyViewEkbatanInfo", "این دیگ خیلی دیگ خاص و بزرگیه، جادوهایی که ازش سر میزنه جادوهای قدرتمندی هستن و باید تو استفاده ازش محتاط باشی. اگه از جادوش استفاده کنی بهت کمک میکنه شکل نهایی که میخوای درست کنی تو بازی رو بهت نشون بده ولی واسه اینکار باید سه پر طلایی رو بهش بدی.");
        infoText.Add("CurrencyViewInfo", "New Info");
    }
}
