using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Needs a button to add the points
public class SimorghCard : DialogBase
{
    public void addPoints()
    {
        DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + 1);
        DataManager.Instance.Save();
        //Play the animation or anything else that must happen while showing the dialog. Add the code here
    }
}
