using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker2Objects : MonoBehaviour
{
    public List<float> epsilons;
    
    public List<GameObject> colliderPoints;
    private float[,] distances = new float[4, 4];
    public float dealDeg;
    public bool flagWin;
    // Start is called before the first frame update
    void Start()
    {
//        Debug.Log("3");
        flagWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculating and Storing the Distances of colliderPoints
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= i; j++)
            {
                distances[i, j] = Vector3.Distance(colliderPoints[i].transform.position, colliderPoints[j].transform.position);

            }
        }//if you wanna use distance[0,1] use distance[1,0] IE ***** the bigger number should be first ********
        WinChecking();

    }
    public void WinChecking()
    {

        List<bool> passes = new List<bool>();
        //Pass 1:: Checking the Locks
        float pass1 = Vector3.Distance(colliderPoints[1].transform.position, colliderPoints[2].transform.position);//distance between the 2 lock
        if (pass1 <= epsilons[0])
        {
            passes.Add(true);
        }
        else
        {
            passes.Add(false);
        }
        //Pass 2:: Checking the Replacement Locks
        float replaceLock1 = Math.Abs(distances[1, 0] - distances[2, 0]);
        if (replaceLock1 <= epsilons[1])
        {
            passes.Add(true);
        }
        else
        {
            passes.Add(false);
        }
        float replaceLock2 = Math.Abs(distances[3, 1] - distances[3, 2]);
        if (replaceLock2 <= epsilons[1])
        {
            passes.Add(true);
        }
        else
        {
            passes.Add(false);
        }
        //Pass 3:: Checking the Deal
        double dealtheta = (dealDeg * Math.PI) / 180; //transform to rad
        double r = distances[1, 0];//One of the fixed sides
        double p = distances[3, 2];// the other fixed side
        double t = 2 * r * p * Math.Cos(dealtheta); // using the cos formula
        r = Math.Pow(r, 2);
        p = Math.Pow(p, 2);
        double theDeal = Math.Sqrt(r + p - t); //finding the deal with cos formula
        if (Math.Abs(theDeal - distances[3, 0]) <= epsilons[2])
        {
            passes.Add(true);
        }
        else
        {
            passes.Add(false);
        }
        bool d = true;
        for (int i = 0; i < passes.Count; i++)
        {
            if (!passes[i])
            {
                d = false;
                flagWin = false;
                break;
            }

        }
        if (d)
        {
            flagWin = true;
        }

    }
}
