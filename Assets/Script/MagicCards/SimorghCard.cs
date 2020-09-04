using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private WinChecker winChecker;
    private bool levelIsOver;
    // Start is called before the first frame update
    void Start()
    {
        winChecker = GameObject.Find("WinChecker").GetComponent<WinChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        levelIsOver = winChecker.flagWin;
    }
}
