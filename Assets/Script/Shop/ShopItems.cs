using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ShopItems", menuName = "Partala/ShopItems", order = 0)]
public class ShopItems : ScriptableObject {
    public int id;
    public string name;
    public int numOfFeathers;
    public int giftFeathers;
    public bool noAdsFor1Episode;
    public bool noAds;
    public float price;

}
