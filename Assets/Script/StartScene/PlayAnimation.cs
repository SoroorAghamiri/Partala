﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        Invoke("invokeAnimation", 3f);
    }

    void invokeAnimation()
    {
        if (!anim.isPlaying)
            anim.Play("ZoomIn_Out");
        Invoke("invokeAnimation", 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
