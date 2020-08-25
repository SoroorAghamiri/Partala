using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
public class InfoView : DialogBase
{
    Dictionary<string, string> infoText = new Dictionary<string, string>();
    public Text info;
    void Start()
    {
        if (infoText.Count == 0)
        {
            fillDictionary();
        }
        explanation();
    }


    //Generalize this view later
    private void explanation()
    {

        info.text = Fa.faConvertLine(infoText["CurrencyView"]);
    }

    public void closePanel()
    {
        Destroy(this.gameObject);
    }

    void fillDictionary()
    {
        infoText.Add("CurrencyView", "راهنمای اول: حذف آبجکت های بی استفاده   راهنمای دوم: نمایش شکل نهایی");
    }
}
