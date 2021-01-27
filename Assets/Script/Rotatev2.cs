using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatev2 : MonoBehaviour
{
    private GameObject[] correctObj;
    private GameObject[] correctPos;
    private GameObject rotateable = null;
    private GameObject selected = null;

    public float minRandX = 0;
    public float maxRandX = 0;
    public float minRandY = 0;
    public float maxRandY = 0;

    private float correctAngle;


    private ObjectFixer objfixer;

    private void Start() {
        objfixer = GameObject.FindObjectOfType<ObjectFixer>();

        correctObj = GameObject.FindGameObjectsWithTag("MainComponent");

        correctPos = GameObject.FindGameObjectsWithTag("MainPosition");

        correctPos = objfixer.SortArrays(correctObj , correctPos);

        correctAngle = FindRotateable(correctObj , correctPos);
        // Debug.Log("correct Angle = " + correctAngle);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("1.Other's name: "+ other.gameObject.name);
        if(GameObject.ReferenceEquals(other.gameObject , rotateable)){
            // Debug.Log("Succeed");
            RotateRotateable();
            StartCoroutine(ThrowOut(rotateable));
        }else if(other.gameObject.tag != "ColliderPoint")
        {
            selected = other.gameObject;
            // Debug.Log("Failed");
            // Debug.Log("On Object " + other.gameObject.name);
            StartCoroutine(ThrowOut(selected));
        }
    }


    private float FindRotateable(GameObject[] toCheck , GameObject[] reference){
        for(int i = 0 ; i < toCheck.Length ;i++){
            float end = reference[i].transform.localEulerAngles.z;

            float ch = toCheck[i].transform.localEulerAngles.z;
            // Debug.Log("reference angle: "+ end + " tocheck angle: " + ch);
            if(end != ch){
                rotateable = toCheck[i];
                return end;
            }
        }
        return 0;
    }

    //Change to private when collision detected
    public void RotateRotateable(){
        rotateable.transform.rotation = Quaternion.Euler(new Vector3(0 , 0 , correctAngle));
    }

    private IEnumerator ThrowOut(GameObject throwable){
        yield return new WaitForSeconds(5);
        //Play Animation
        float xtobe = Random.Range(minRandX , maxRandX);
        float ytobe = Random.Range(minRandY , maxRandY);
        // Debug.Log("random x:" + xtobe + " random y: "+ytobe);
        Vector3 dest = new Vector3(xtobe , ytobe , 0);
        // throwable.transform.Translate(dest * Time.deltaTime , Space.World);
        throwable.transform.localPosition = dest;
    }
}
