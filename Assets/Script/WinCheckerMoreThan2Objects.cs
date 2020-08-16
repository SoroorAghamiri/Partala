using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using GameAnalyticsSDK.Setup;

public class WinCheckerMoreThan2Objects : MonoBehaviour
{

    public bool flagWin;
   
    [SerializeField] bool setup = false;
    private GameObject[] colliderPoints;
    private List<GameObject> listOfCPs;
    private GameManger gameManger;
    private GameObject[] lockerPoints;
    [SerializeField] private List<GameObject> listofLockerPoints; //Remove Serialize for debugging


    private List<string> distances;
    [SerializeField] float[] distancesInFloat;
    [SerializeField] float allowanceForObjects = 0.16f;
    [SerializeField] float allowanceForLockers = 0.1f;
    private int index = -1;
    private GameObject[] mainComponents;
    // Start is called before the first frame update
    void Start()
    {
        gameManger = GameObject.FindObjectOfType<GameManger>();
        flagWin = false;
        listOfCPs = new List<GameObject>();
        listofLockerPoints = new List<GameObject>();
        distances = new List<string>();
        if (setup)
        {
            PuttingCPsIntoList();
            CalculatingDistances();
            WriteDistanceTovalues();
        }
        else
        {
            PuttingCPsIntoList();
            PuttingLockerPointsIntoList();
        }
    }

    private void PuttingLockerPointsIntoList()
    {
        lockerPoints = GameObject.FindGameObjectsWithTag("Locker");
        listofLockerPoints = lockerPoints.OrderBy(go => go.name).ToList();
    }

    private void WriteDistanceTovalues()
    {

        for (int i = 0; i < distances.Count(); i++)
        {
            distancesInFloat[i] = float.Parse(distances[i]);
        }

    }

    private void CalculatingDistances()
    {
        for (int i = 0; i * 2 < listOfCPs.Count() + 1; i = i + 2)
        {
            for (int j = i; j < listOfCPs.Count(); j = j + 2)
            {
                if (i == j)
                {
                    continue;
                }
                var dist = DistanceBetween2points(listOfCPs[i].transform.position, listOfCPs[j].transform.position);
                distances.Add(dist.ToString());
                dist = DistanceBetween2points(listOfCPs[i].transform.position, listOfCPs[j + 1].transform.position);
                distances.Add(dist.ToString());
                dist = DistanceBetween2points(listOfCPs[i + 1].transform.position, listOfCPs[j].transform.position);
                distances.Add(dist.ToString());
                dist = DistanceBetween2points(listOfCPs[i + 1].transform.position, listOfCPs[j + 1].transform.position);
                distances.Add(dist.ToString());
            }
        }
    }

    private float DistanceBetween2points(Vector3 position1, Vector3 position2)
    {
        var powx = Math.Pow((position1.x - position2.x), 2.0);
        var powy = Math.Pow((position1.y - position2.y), 2.0);
        var dis = (float)Math.Sqrt(powx + powy);
        return dis;
    }

    private void PuttingCPsIntoList()
    {
        colliderPoints = GameObject.FindGameObjectsWithTag("ColliderPoint");
        listOfCPs = colliderPoints.OrderBy(go => go.name).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if (setup)
            return;
        //Pass
        bool win = true;
        win = CheckTheDistances(win);//The actual passes
        if (win)
        {
            flagWin = true;
            gameManger.SetWin();
        }


    }


    private bool CheckTheDistances(bool win)
    {
        index = -1;
        for (int i = 0; i * 2 < listOfCPs.Count() + 1; i = i + 2)
        {
            for (int j = i; j < listOfCPs.Count(); j = j + 2)
            {
                if (i == j)
                {
                    continue;
                }
                var dist = DistanceBetween2points(listOfCPs[i].transform.position, listOfCPs[j].transform.position);
                win = CheckIfItsInCorrectPlace(dist);
                if (!win) { return false; }
                dist = DistanceBetween2points(listOfCPs[i].transform.position, listOfCPs[j + 1].transform.position);
                win = CheckIfItsInCorrectPlace(dist);
                if (!win) { return false; }
                dist = DistanceBetween2points(listOfCPs[i + 1].transform.position, listOfCPs[j].transform.position);
                win = CheckIfItsInCorrectPlace(dist);
                if (!win) { return false; }
                dist = DistanceBetween2points(listOfCPs[i + 1].transform.position, listOfCPs[j + 1].transform.position);
                win = CheckIfItsInCorrectPlace(dist);
                if (!win) { return false; }
            }
        }
        ///Check for Lockers
        for (int i = 0; i < listofLockerPoints.Count(); i += 2)
        {
            var dist = DistanceBetween2points(listofLockerPoints[i].transform.position, listofLockerPoints[i + 1].transform.position);
            Debug.Log(dist);
            if (dist > allowanceForLockers)
            {
                return false;
            }
        }
        return win;
    }

    private bool CheckIfItsInCorrectPlace(float dist)
    {

        index++;
        if (dist <= distancesInFloat[index] + allowanceForObjects && dist >= distancesInFloat[index] - allowanceForObjects)
        {
            return true;
        }
        return false;


    }

    public bool FetchFlagWin() { return flagWin; }
}
