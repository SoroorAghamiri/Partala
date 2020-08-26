using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private Collider2D[] collider2DObjects ;
    public GameObject activeGameObject;
    private int movingFingerID = -1;

    [SerializeField] private float speedMove = 15f;
    // Start is called before the first frame update
    void Start()
    {
        collider2DObjects = new Collider2D[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            collider2DObjects[i] = objects[i].GetComponent<Collider2D>();
        }
        movingFingerID = -1;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (movingFingerID != -1)
            {
                break;
            }
            Vector2 touchPosition = NormalizeTouchPosition(touch.position);
            for (int i = 0; i < collider2DObjects.Length; i++)
            {
                if (collider2DObjects[i] == Physics2D.OverlapPoint(touchPosition))
                {
                    activeGameObject = objects[i];
                    movingFingerID = touch.fingerId;
                }
            }
            
        }
        if(movingFingerID!=-1)
            MoveToTheLocationOfTheTouch(movingFingerID);
    }

    private Vector2 NormalizeTouchPosition(Vector2 touchPositionUnnormal)
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(touchPositionUnnormal);
        var touchPosition = new Vector2(worldPoint.x, worldPoint.y);
        return touchPosition;
    }
    private void MoveToTheLocationOfTheTouch(int indexofTouch)
    { 
        Touch touch = Input.GetTouch(indexofTouch);
        Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(indexofTouch).position).x,
                                    Camera.main.ScreenToWorldPoint(Input.GetTouch(indexofTouch).position).y,
                                    activeGameObject.transform.position.z);
        activeGameObject.transform.position = Vector3.Lerp(activeGameObject.transform.position, newPos, speedMove * Time.deltaTime);
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            activeGameObject.transform.position = Vector3.Lerp(activeGameObject.transform.position, newPos, speedMove * Time.deltaTime);
            movingFingerID = -1;
        }
    }
}
