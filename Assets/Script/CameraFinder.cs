using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFinder : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        this.GetComponent<Canvas>().worldCamera =Camera.main;
    }

    void Start()
    {
       // th
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
