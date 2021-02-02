﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggsScript : MonoBehaviour
{
    private int count;
    private GameObject eggs;
    [SerializeField] private int winnableCount;
    private int firstEgg;
    private int secondEgg;
    private CollisionChecker myCollider;

    private bool bothEggsAreActive; 

    // Start is called before the first frame update
    void Start()
    {
        bothEggsAreActive = false;
        myCollider = GameObject.Find("Panel2").GetComponent<CollisionChecker>();
        eggs = GameObject.Find("Eggs");
        switch (winnableCount)
        {
            case 2:
                firstEgg = 1;
                secondEgg = 2;
                break;
            case 3:
                firstEgg = 1;
                secondEgg = 3;
                break;
            case 4:
                firstEgg = 2;
                secondEgg = 4;
                break;
            case 5:
                firstEgg = 2;
                secondEgg = 5;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        SettingFirst2Eggs();//In need to move this to some where else 
    }

    public void SetLastEgg()
    {
        eggs.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void SettingFirst2Eggs()
    {
        count = myCollider.FetchCorrectObjects();
        if (count < firstEgg)
        {
            bothEggsAreActive = false;
            eggs.transform.GetChild(0).gameObject.SetActive(false);
            eggs.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (count >= firstEgg && count < secondEgg)
        {
            bothEggsAreActive = false;

            eggs.transform.GetChild(0).gameObject.SetActive(true);

            eggs.transform.GetChild(1).gameObject.SetActive(false);

        }
        else if (count >= secondEgg)
        {


            eggs.transform.GetChild(0).gameObject.SetActive(true);

            eggs.transform.GetChild(1).gameObject.SetActive(true);

            bothEggsAreActive = true;
        }
    }


    public bool CheckBothEggsAreActive()
    {
        return bothEggsAreActive;
    }
}
