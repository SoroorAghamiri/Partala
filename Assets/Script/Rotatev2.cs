using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatev2 : MonoBehaviour
{
    [SerializeField]private GameObject[] correctObj;
    [SerializeField]private GameObject[] correctPos;
    [SerializeField]private GameObject rotateable = null;

    private float correctAngle;


    private ObjectFixer objfixer;

    private void Start() {
        objfixer = GameObject.FindObjectOfType<ObjectFixer>();

        correctObj = GameObject.FindGameObjectsWithTag("MainComponent");

        correctPos = GameObject.FindGameObjectsWithTag("MainPosition");

        correctPos = objfixer.SortArrays(correctObj , correctPos);

        correctAngle = FindRotateable(correctObj , correctPos);
        Debug.Log("correct Angle = " + correctAngle);
    }

    private float FindRotateable(GameObject[] toCheck , GameObject[] reference){
        for(int i = 0 ; i < toCheck.Length ;i++){
            float end = reference[i].transform.localEulerAngles.z;

            float ch = toCheck[i].transform.localEulerAngles.z;
            Debug.Log("reference angle: "+ end + " tocheck angle: " + ch);
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
}
