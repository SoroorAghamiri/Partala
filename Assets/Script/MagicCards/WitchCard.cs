using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Needs a button to diable the second hint
public class WitchCard : DialogBase
{
    public void diableSecondHint()
    {
        DataManager.Instance.SetEnableSecondHint(false);
        //DateManager.Instance.Save();
    }
}
