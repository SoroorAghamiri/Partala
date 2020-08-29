using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;
using System.Diagnostics;
public class InfoView : DialogBase
{
    Dictionary<string, string> infoText = new Dictionary<string, string>();
    // public List<Button> hintButton = new List<Button>();
    public string callingHint;

    void Start()
    {
        if (infoText.Count == 0)
        {
            fillDictionary();
        }
        print("calling hint = " + callingHint);
        explanation(callingHint);
    }


    //Generalize this view later
    private void explanation(string hintNum)
    {
        string gorInfot = "CurrencyView" + "Hint" + hintNum;
        print(gorInfot);

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
        infoText.Add("CurrencyViewHint1", "راهنمای اول: حذف آبجکت های بی استفاده");
        infoText.Add("CurrencyViewHint2", "راهنمای اول: حذف آبجکت های بی استفاده");
    }
}
