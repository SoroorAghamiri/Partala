using System.Collections;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private Collider2D collider;
    public bool touched;
    public bool rotate;
    private Collider2D panel2Collider;
    
    private float offsetAngle;
    private Vector2 movement;
    public bool bigPanelCollistuin;

    private float speedRot;
    private float speedMove;

    private RotateButtonInGame rotateButton;
    private Collider2D rotateButtonCollider;
    private Coroutine fadeCoroutine;
    private Material objectMaterial;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        panel2Collider = GameObject.Find("Panel2").GetComponent<Collider2D>();
        speedRot = 15f;
        speedMove = 15f;
        rotateButton = GameObject.FindObjectOfType<RotateButtonInGame>();
        rotateButtonCollider = rotateButton.GetComponent<Collider2D>();
        objectMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForTouch();
    }

    private IEnumerator FadeInFadeOutWhenTouched()
    {
        while (true)
        {
            while (objectMaterial.color.a > 0.70f)
            {
                objectMaterial.color = new Color(1f, 1f, 1f, objectMaterial.color.a - 0.002f);
                yield return null;
            }
            while (objectMaterial.color.a <= 1)
            {
                objectMaterial.color = new Color(1f, 1f, 1f, objectMaterial.color.a + 0.002f);
                yield return null;
            }
        }
    }
    void CheckForTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = NormalizeTouchPosition(Input.GetTouch(0).position);

            if (collider == Physics2D.OverlapPoint(touchPosition))
            {
                touched = true;
                rotate = false;
                if (fadeCoroutine == null)
                    fadeCoroutine = StartCoroutine(FadeInFadeOutWhenTouched());
            }
            else if (panel2Collider == Physics2D.OverlapPoint(touchPosition) && touched)
            {
                //Here were GameObject is inThe Panel ?
                bigPanelCollistuin = true;
                if (collider == Physics2D.OverlapPoint(touchPosition))
                {
                    touched = true;
                    rotate = false;
                    if (fadeCoroutine == null)
                        fadeCoroutine = StartCoroutine(FadeInFadeOutWhenTouched());
                }
                else
                {
                    rotate = true;
                    if (fadeCoroutine == null)
                        fadeCoroutine = StartCoroutine(FadeInFadeOutWhenTouched());
                    SetRotateandOffsetAngle(touchPosition);
                }
            }
            else if(rotateButtonCollider == Physics2D.OverlapPoint(touchPosition))
            {
                if(touched && rotate)
                {
                    ///DO NOTHING
                }

            }
            else
            {
                touched = false;
                rotate = false;
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    objectMaterial.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }

        if (rotate && touched && rotateButton.RotateButtonIsPressed())
        {
            RotateAndLookAtTheTouch();

        }
        else if (touched && !rotate && !rotateButton.RotateButtonIsPressed())
        {
            MoveToTheLocationOfTheTouch();
        }
    }

    private static Vector2 NormalizeTouchPosition(Vector2 touch)
    {
        var wp = Camera.main.ScreenToWorldPoint(touch);
        var touchPosition = new Vector2(wp.x, wp.y);
        return touchPosition;
    }

    private void SetRotateandOffsetAngle(Vector2 touchPosition)
    {

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
        foreach(Touch touch in Input.touches)
        {
            Vector2 touchPosition = NormalizeTouchPosition(touch.position);
            if(rotateButtonCollider != Physics2D.OverlapPoint(touchPosition))
            {
                Vector3 newPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, newPos, speedMove * Time.deltaTime);
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    transform.position = Vector3.Lerp(transform.position, newPos, speedMove * Time.deltaTime);
                }
            }
        }
        
    }

    private void RotateAndLookAtTheTouch()
    {
        if (Input.touchCount == 0)
            return;
        foreach(Touch touch in Input.touches)
        {
            Vector2 touchPosition = NormalizeTouchPosition(touch.position);
            if(panel2Collider == Physics2D.OverlapPoint(touchPosition))
            {
                Vector2 currentPosition = transform.position;
                Vector2 moveTowards = Camera.main.ScreenToWorldPoint(touch.position);
                movement = moveTowards - currentPosition;
                movement.Normalize();
                float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle + offsetAngle), speedRot * Time.deltaTime);
            }
        }   
    }
}
