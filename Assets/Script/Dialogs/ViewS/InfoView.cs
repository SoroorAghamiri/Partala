using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using System.Diagnostics;
public class InfoView : DialogBase
{
    Dictionary<string, string> infoText = new Dictionary<string, string>();
    public List<UPersian.Components.RtlText> hintTexts = new List<UPersian.Components.RtlText>();

    void Start()
    {
        if (infoText.Count == 0)
        {
            fillDictionary();
        }
        // print("calling hint = " + callingHint);
        explanation();
    }


    //Generalize this view later
    private void explanation()
    {
        for (int i = 0; i < hintTexts.Count; i++)
        {
            hintTexts[i].text = infoText["CurrencyViewHint" + (i + 1).ToString()];
        }
    }

    // public void showExplanation()
    // {
    //     string name = EventSystem.current.currentSelectedGameObject.name;
    //     explanation(name);
    // }

    public void closePanel()
    {
        Destroy(this.gameObject);
    }

    void fillDictionary()
    {
        infoText.Add("CurrencyViewHint1", "این دیگ خاصیتش اینه که جادویی ازش سر میزنه که میتونه بهت کمک کنه یکی از اشیا اضافه تو بازی رو ناپدید کنی. واسه اینکار باید یه دونه پر طلایی سیمرغتو بهش بدی تا جادو عمل کنه.");
        infoText.Add("CurrencyViewHint2", "این دیگ خیلی دیگ خاص و بزرگیه، جادوهایی که ازش سر میزنه جادوهای قدرتمندی هستن و باید تو استفاده ازش محتاط باشی. اگه از جادوش استفاده کنی بهت کمک میکنه شکل نهایی که میخوای درست کنی تو بازی رو بهت نشون بده ولی واسه اینکار باید سه پر طلایی رو بهش بدی.");
    }
}
