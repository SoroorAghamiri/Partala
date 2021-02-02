using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatev2 : MonoBehaviour
{
    private GameObject[] correctObj;
    private GameObject[] correctPos;
    private List<GameObject> rotateable = new List<GameObject>();
    private GameObject selected = null;
    private Vector3 prevScale;

    private GameObject Panel2;
    private float minRandX = 0;
    private float maxRandX = 0;
    private float minRandY = 0;
    private float maxRandY = 0;

    private List<float> correctAngle = new List<float>();


    private ObjectFixer objfixer;

    private void Start() {
        objfixer = GameObject.FindObjectOfType<ObjectFixer>();

        Panel2 = GameObject.Find("Panel2");

        GetCoordinates();

        correctObj = GameObject.FindGameObjectsWithTag("MainComponent");

        correctPos = GameObject.FindGameObjectsWithTag("MainPosition");

        correctPos = objfixer.SortArrays(correctObj , correctPos);

       FindRotateable(correctObj , correctPos);
    }

   private void GetCoordinates(){
       Bounds boxBounds = Panel2.GetComponent<BoxCollider2D>().bounds;
       minRandX = boxBounds.center.x - boxBounds.extents.x;
       maxRandX = boxBounds.center.x + boxBounds.extents.x;

       minRandY = boxBounds.center.y - boxBounds.extents.y;
       maxRandY = boxBounds.center.y + boxBounds.extents.y;
   }


    private void OnTriggerEnter2D(Collider2D other) {
        for(int i=0; i < rotateable.Count; i++)
        {
            if(GameObject.ReferenceEquals(other.gameObject , rotateable[i])){
            StartCoroutine(ReceiveObj(rotateable[i] , i));
            StartCoroutine(ThrowOut(rotateable[i]));
            }else if(other.gameObject.tag != "ColliderPoint")
            {
                selected = other.gameObject;
                StartCoroutine(ReceiveObj(selected , 0));
                StartCoroutine(ThrowOut(selected));
            }
        }
        
    }


    private void FindRotateable(GameObject[] toCheck , GameObject[] reference){
        
        for(int i = 0 ; i < toCheck.Length ;i++){
            float end = reference[i].transform.localEulerAngles.z;
            float ch = toCheck[i].transform.localEulerAngles.z;
         
            if(end != ch){
                rotateable.Add(toCheck[i]);
                correctAngle.Add(end);
            }
        }
    }

 
    

    private IEnumerator ReceiveObj(GameObject receivable , int indx){
        prevScale = receivable.transform.localScale;
        iTween.ScaleTo(receivable , Vector3.zero, 0.8f);
        yield return new WaitForSeconds(1);
        if(GameObject.ReferenceEquals(receivable , rotateable[indx])){
            receivable.transform.rotation = Quaternion.Euler(new Vector3(0 , 0 , correctAngle[indx]));
        }
    }

    private IEnumerator ThrowOut(GameObject throwable){
        yield return new WaitForSeconds(5);
        //Play Animation
        float xtobe = Random.Range(minRandX , maxRandX);
        float ytobe = Random.Range(minRandY , maxRandY);
      
        iTween.ScaleTo(throwable , prevScale, 0.2f);
        Vector3 dest = new Vector3(xtobe , ytobe , 0);
        // throwable.transform.Translate(dest * Time.deltaTime , Space.World);
        //Can later be replaced by iTween.MoveTo
        throwable.transform.position = dest;
    }
}
