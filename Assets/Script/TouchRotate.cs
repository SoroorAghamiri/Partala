using System.Collections;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private Collider2D collider;
    public bool touched;
    public bool rotate;
    private Transform obj;
    private Collider2D panel2Collider;
    
    private float offsetAngle;
    private Vector2 movement;
    public bool bigPanelCollistuin;

    private float speedRot;
    private float speedMove;


    void Start()
    {
        collider = GetComponent<Collider2D>();
        panel2Collider = GameObject.Find("Panel2").GetComponent<Collider2D>();
        speedRot = 15f;
        speedMove = 15f;
        obj = transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTouch();
    }

    void CheckForTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = NormalizeTouchPosition();

            if (collider == Physics2D.OverlapPoint(touchPosition))
            {
                touched = true;
                rotate = false;

            }
            else if (panel2Collider == Physics2D.OverlapPoint(touchPosition) && touched)
            {
                //Here were GameObject is inThe Panel ?
                bigPanelCollistuin = true;
                if (collider == Physics2D.OverlapPoint(touchPosition))
                {
                    touched = true;
                    rotate = false;
                }
                else
                {
                    SetRotateandOffsetAngle(touchPosition);
                }
            }
            else
            {
                touched = false;
                rotate = false;
            }
        }

        if (rotate && touched)
        {
            RotateAndLookAtTheTouch();

        }
        else if (touched && !rotate)
        {
            MoveToTheLocationOfTheTouch();
        }
    }

    private static Vector2 NormalizeTouchPosition()
    {
        var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        var touchPosition = new Vector2(wp.x, wp.y);
        return touchPosition;
    }

    private void SetRotateandOffsetAngle(Vector2 touchPosition)
    {
        rotate = true;
        //Now we Have to have the initial angle when the touch happened, to offset it later
        Vector2 pos = transform.position;
        movement = touchPosition - pos;
        movement.Normalize();
        //Key code 
        offsetAngle = (Mathf.Atan2(transform.right.y, transform.right.x) - Mathf.Atan2(movement.y, movement.x)) * Mathf.Rad2Deg;
    }

    private void MoveToTheLocationOfTheTouch()
    {
        if (Input.touchCount == 0)
            return;
        Touch touch = Input.GetTouch(0);
        Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, obj.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, speedMove * Time.deltaTime);
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, speedMove * Time.deltaTime);
        }
    }

    private void RotateAndLookAtTheTouch()
    {
        if (Input.touchCount == 0)
            return;
        Touch touch = Input.GetTouch(0);
        Vector2 currentPosition = transform.position;
        Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touch.position);
        movement = moveTowards - currentPosition;
        movement.Normalize();
        float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle + offsetAngle), speedRot * Time.deltaTime);
    }
}
