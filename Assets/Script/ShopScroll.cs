using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopScroll : MonoBehaviour
{
    public List<GameObject> hiddenOffers;
    GameObject currentButton;
    // public void testButton(){
    //     Debug.Log("Button clicked");
    // }
    public void moreButtonClicked(){
        currentButton = EventSystem.current.currentSelectedGameObject;
        iTween.ScaleTo(currentButton , iTween.Hash("scale" ,Vector3.zero , "time",  0.7f , "ignoretimescale" , true));
        // for(int i = 0 ; i < hiddenOffers.Count ; i++){
        //     // hiddenOffers[i].SetActive(true);
        //      iTween.ScaleTo(hiddenOffers[i] ,iTween.Hash("scale" ,Vector3.one , "time",  1f , "ignoretimescale" , true) );
        // }
        // currentButton.SetActive(false);

        StartCoroutine(enableHiddens());
        
    }

    IEnumerator enableHiddens(){
        yield return new WaitForSecondsRealtime(0.7f);
        Debug.Log("First ienum");
        currentButton.SetActive(false);
        for(int i = 0 ; i < hiddenOffers.Count ; i++){
            // hiddenOffers[i].SetActive(true);
            iTween.ScaleTo(hiddenOffers[i] ,iTween.Hash("scale" ,new Vector3(1.3f , 1.3f , 1.3f) , "time", 1f , "ignoretimescale" , true));//new Vector3(1.3f , 1.3f , 1.3f)
            // iTween.ScaleTo(hiddenOffers[i] ,iTween.Hash("scale" ,Vector3.one , "time",  1f , "ignoretimescale" , true) );
        }
        StartCoroutine(resizeHiddens());
    }
    IEnumerator resizeHiddens(){
        yield return new WaitForSecondsRealtime(0.3f);
        Debug.Log("Second ienum");
         for(int i = 0 ; i < hiddenOffers.Count ; i++){
            // hiddenOffers[i].SetActive(true);
            // iTween.ScaleTo(hiddenOffers[i] ,iTween.Hash("scale" ,new Vector3(1.3f , 1.3f , 1.3f) , "time", 1f , "ignoretimescale" , true));//new Vector3(1.3f , 1.3f , 1.3f)
            iTween.ScaleTo(hiddenOffers[i] ,iTween.Hash("scale" ,Vector3.one , "time",  1f , "ignoretimescale" , true) );
        }
    }
}
