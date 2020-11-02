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
    public Button prev;
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
            i.gameObject.SetActive(false);
            if (i.gameObject.name == callingHint)
            {
                i.gameObject.SetActive(true);
            }
        }
        exp.text = infoText["CurrencyView" + cHint];
    }

    public void nextPage()
    {
        if (callingHint == "AlamootInfo")
        {
            callingHint = "EkbatanInfo";
            explanation(callingHint);
            next.gameObject.SetActive(false);
            prev.gameObject.SetActive(true);
        }
        else if (callingHint == "EkbatanInfo")
        {
            callingHint = "AlamootInfo";
            explanation(callingHint);
            next.gameObject.SetActive(true);
            prev.gameObject.SetActive(false);
        }
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
        infoText.Add("CurrencyViewAlamootInfo", "جادویی که از این دیگ به ‌دست میاد رو میتونی برای حذف یکی از اشیا اضافه استفاده کنی تا بتونی زودتر مرحله رو تموم کنی. برای این جادو، باید  " + levelDisc1 + "پر طلایی خرج کنی!");
        infoText.Add("CurrencyViewEkbatanInfo", "دیگ اکباتان، خاص ترین جادو رو داره و میتونی با اون شکل نهایی همون مرحله رو ببینی و بازی برات آسون تر از همیشه میشه. برای این جادو، باید " + levelDisc2 + "پر طلایی خرج کنی!");
        infoText.Add("CurrencyViewInfo", "New Info");
    }
}
