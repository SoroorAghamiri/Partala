using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectFixer : MonoBehaviour
{
    [HideInInspector] public bool isFixed = false;
    [HideInInspector] public GameObject fixedObj = null;
    [SerializeField] private GameObject[] correctObj;
    [SerializeField] private GameObject[] correctPosition;

    [SerializeField] private TouchManager touchManager;

    [SerializeField] private Transform[] correctObjTrs;
    [SerializeField] private Transform[] correctPosTrs;
    float offset;
    int indx = 0;
    // Start is called before the first frame update
    void Start()
    {
        correctObj = GameObject.FindGameObjectsWithTag("MainComponent");
        correctPosition = GameObject.FindGameObjectsWithTag("MainPosition");

        correctObjTrs = new Transform[correctObj.Length];
        correctPosTrs = new Transform[correctPosition.Length];

        touchManager = (TouchManager)FindObjectOfType(typeof(TouchManager));

        SortArrays(correctObj, correctPosition);

        GetTheTransforms(correctObj, correctObjTrs);
        GetTheTransforms(correctPosition, correctPosTrs);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            isFixed = false;
            for (int i = 0; i < correctObj.Length; i++)
            {
                if (correctObj[i].name == touchManager.activeGameObject.name)
                {
                    indx = i;
                    break;
                }
            }

        }

        if (Input.touchCount == 0)
        {
            if (indx < correctObj.Length)
                offset = Vector3.Distance(correctPosTrs[indx].position, correctObjTrs[indx].position);
            if (offset < 1.5f && indx < correctObj.Length)
            {
                correctObj[indx].transform.position = correctPosition[indx].transform.position;
                fixedObj = correctObj[indx];
                indx++;
                isFixed = true;
            }
        }
    }

    void SortArrays(GameObject[] a, GameObject[] b)
    {
        int i = 0;
        int j = 0;
        GameObject[] temp = new GameObject[a.Length];
        string target;
        while (i < a.Length)
        {
            target = a[i].name + " (1)";
            // Debug.Log("target name " + target);
            for (j = 0; j < a.Length; j++)
            {
                if (b[j].name == target)
                {
                    temp[i] = b[j];
                    i++;
                }
            }
        }
        for (j = 0; j < a.Length; j++)
            b[j] = temp[j];
    }

    void GetTheTransforms(GameObject[] obj, Transform[] trns)
    {
        // Debug.Log("Getting objects Transform");
        for (int i = 0; i < obj.Length; i++)
        {
            trns[i] = obj[i].transform;
        }
    }
}
