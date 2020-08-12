using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class LevelSelectSwipe : MonoBehaviour
{
    [SerializeField] private float limitDown = 0.0f;
    [SerializeField] private float limitUp;
    [SerializeField] private float speedForSwipe=0.021f;
    void Update()
    {
        if (transform.position.y >= limitDown)
        {
            transform.position = new Vector3(0, limitDown, 0);
        }
        if (transform.position.y < limitUp)
        {
            transform.position = new Vector3(0, limitUp, 0);
        }
    }
}
