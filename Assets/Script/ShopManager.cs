using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public void BuyFeatherByCount(int countofFeathersToBuy)
    {
        DataManager.Instance.SetFeather(DataManager.Instance.GetFeather() + countofFeathersToBuy);
    }
}
