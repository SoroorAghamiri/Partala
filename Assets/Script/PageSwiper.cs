﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    private int pageIndex;
    public int numberOfPages;
    void Start()
    {
        panelLocation = transform.position;
        pageIndex = 0;
    }




    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {

            Vector3 newLocation = panelLocation;
            if (percentage > 0)
            {
                pageIndex++;
                if (pageIndex < numberOfPages)
                    newLocation += new Vector3(-Screen.width, 0, 0);
                else
                    pageIndex = numberOfPages - 1;
            }

            else if (percentage < 0)
            {
                pageIndex--;
                if (pageIndex >= 0)
                    newLocation += new Vector3(Screen.width, 0, 0);
                else
                    pageIndex = 0;
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
