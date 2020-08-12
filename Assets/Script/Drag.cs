using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private float StarPosX;
    private float StartPosY;
    private bool IsBeginHeld = false;
    Vector3 mousepos;
    public GameManger Gm;

    void Start()
    {
        Gm=GameObject.Find("GameManger").GetComponent<GameManger>();   
    }

// Update is called once per frame
    void Update()
    {
        if (IsBeginHeld == true&& Gm.Win!=true)
        {
            
            mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToViewportPoint(mousepos*15);
            this.gameObject.transform.localPosition = new Vector3(mousepos.x-StarPosX, mousepos.y-StartPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0) && Gm.Win!=true)
        {
    
            mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToViewportPoint(mousepos*15);
            StarPosX = mousepos.x - this.transform.localPosition.x;
            StartPosY = mousepos.y - this.transform.localPosition.y;
            IsBeginHeld = true;
        }
    }

    private void OnMouseUp()
    {
        IsBeginHeld = false;
    }
    
}