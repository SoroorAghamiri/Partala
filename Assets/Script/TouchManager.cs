using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    private Collider2D[] collider2DObjects;
    public GameObject activeGameObject;
    private int movingFingerID;

    private bool rotate;
    private Collider2D rightPanelCollider;
    private Vector2 movement;
    private float offsetAngle;
    private RotateButtonInGame rotateButton;

    [SerializeField] private float speedMove = 15f;
    [SerializeField] private float speedRot = 15f;
    // Start is called before the first frame update
    void Start()
    {
        collider2DObjects = new Collider2D[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            collider2DObjects[i] = objects[i].GetComponent<Collider2D>();
        }
        movingFingerID = -1;
        rightPanelCollider = GameObject.Find("Panel2").GetComponent<Collider2D>();
        rotateButton = GameObject.FindObjectOfType<RotateButtonInGame>();
        rotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (movingFingerID != -1 || rotate)
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
            if (activeGameObject != null && movingFingerID == -1)
            {
                if (rightPanelCollider == Physics2D.OverlapPoint(touchPosition))
                {
                    SetRotateandOffsetAngle(touchPosition);
                    rotate = true;

                }
            }

        }
        if (movingFingerID != -1 && !rotateButton.RotateButtonIsPressed())
            MoveToTheLocationOfTheTouch(movingFingerID);
        else if (rotate && rotateButton.RotateButtonIsPressed())
        {
            RotateAndLookAtTheTouch();
        }
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
    private void RotateAndLookAtTheTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector2 touchPosition = NormalizeTouchPosition(touch.position);
            if (rightPanelCollider == Physics2D.OverlapPoint(touchPosition))
            {

                Vector2 currentPosition = activeGameObject.transform.position;
                Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touch.position);
                movement = moveTowards - currentPosition;
                movement.Normalize();
                float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                activeGameObject.transform.rotation = Quaternion.Slerp(activeGameObject.transform.rotation, Quaternion.Euler(0, 0, targetAngle + offsetAngle), speedRot * Time.deltaTime);
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    rotate = false;
                }
            }
        }
    }
    private void SetRotateandOffsetAngle(Vector2 touchPosition)
    {
        //Now we Have to have the initial angle when the touch happened, to offset it later
        Vector2 pos = activeGameObject.transform.position;
        movement = touchPosition - pos;
        movement.Normalize();
        //Key code 
        offsetAngle = (Mathf.Atan2(activeGameObject.transform.right.y, activeGameObject.transform.right.x) - Mathf.Atan2(movement.y, movement.x)) * Mathf.Rad2Deg;
    }
}
