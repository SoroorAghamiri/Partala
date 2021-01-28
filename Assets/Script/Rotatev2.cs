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
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(GameObject.ReferenceEquals(other.gameObject , rotateable)){
            StartCoroutine(ReceiveObj(rotateable));
            StartCoroutine(ThrowOut(rotateable));
        }else if(other.gameObject.tag != "ColliderPoint")
        {
            selected = other.gameObject;
            StartCoroutine(ReceiveObj(selected));
            StartCoroutine(ThrowOut(selected));
        }
    }


    private float FindRotateable(GameObject[] toCheck , GameObject[] reference){
        for(int i = 0 ; i < toCheck.Length ;i++){
            float end = reference[i].transform.localEulerAngles.z;
            float ch = toCheck[i].transform.localEulerAngles.z;
         
            if(end != ch){
                rotateable = toCheck[i];
                return end;
            }
        }
        return 0;
    }

 
    

    private IEnumerator ReceiveObj(GameObject receivable){
        iTween.ScaleTo(receivable , Vector3.zero, 0.8f);
        yield return new WaitForSeconds(1);
        if(GameObject.ReferenceEquals(receivable , rotateable)){
            rotateable.transform.rotation = Quaternion.Euler(new Vector3(0 , 0 , correctAngle));
        }
    }

    private IEnumerator ThrowOut(GameObject throwable){
        yield return new WaitForSeconds(5);
        //Play Animation
        float xtobe = Random.Range(minRandX , maxRandX);
        float ytobe = Random.Range(minRandY , maxRandY);
      
        iTween.ScaleTo(throwable , Vector3.one, 0.2f);
        Vector3 dest = new Vector3(xtobe , ytobe , 0);
        // throwable.transform.Translate(dest * Time.deltaTime , Space.World);
        //Can later be replaced by iTween.MoveTo
        throwable.transform.localPosition = dest;
    }
}
