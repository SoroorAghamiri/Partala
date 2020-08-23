using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalObjectManager: MonoBehaviour
{
    private GameObject finalObject;
    private float speedForScale= 1f;
    private float limit = 1f;
    private GameObject[] correctObjects;
    private GameObject[] wrongComponents;
    // Start is called before the first frame update
 
    public void ManageSecondHint(bool firstHint)
    {
        Initialization();
        StartCoroutine(ShowFinalObject());
        StartCoroutine(MakeFinalObjectHidden(firstHint));
    }
    public void ShowFinalObjectAfterWin()
    {
        Initialization();
        StartCoroutine(ShowFinalObject());

    }
    private void Initialization()
    {
        finalObject = GameObject.FindGameObjectWithTag("Final");
        correctObjects = GameObject.FindGameObjectsWithTag("MainComponent");
        wrongComponents = GameObject.FindGameObjectsWithTag("WrongComponent");
    }

    public IEnumerator ShowFinalObject()
    {
        ///
        ///Remove Later
        ///
        for (int i = 0; i < correctObjects.Length; i++)
        {
            correctObjects[i].SetActive(false);
        }
        for (int i = 0; i < wrongComponents.Length; i++)
        {
            wrongComponents[i].SetActive(false);
        }
        while (finalObject.transform.localScale.x < limit)
        {
            finalObject.transform.localScale += new Vector3(
                speedForScale * Time.deltaTime,
                speedForScale * Time.deltaTime,
                speedForScale * Time.deltaTime);
            yield return null;
        }
    }
    private IEnumerator MakeFinalObjectHidden(bool firstHint)
    {
        yield return new WaitForSeconds(2.0f);
        finalObject.transform.localScale = new Vector3(0, 0, 0);
        ///
        ///Remove Later
        ///
        for (int i = 0; i < correctObjects.Length; i++)
        {
            correctObjects[i].SetActive(true);
        }
        if (firstHint == false)
            for (int i = 0; i < wrongComponents.Length; i++)
            {
                wrongComponents[i].SetActive(true);
            }
        Destroy(gameObject);
    }
}
