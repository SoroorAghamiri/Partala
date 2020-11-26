using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialV2 : MonoBehaviour
{
    public TutorialObj tO;

    public UnityEngine.Experimental.Rendering.Universal.Light2D globalLight;
    public UnityEngine.Experimental.Rendering.Universal.Light2D rightLight;
    private bool rotationDone = false;
    private int i = 0;
    bool touched = false;
    private bool showGuide = true;
    private GameObject[] cityLights;

    private void Start() {
        if(!DataManager.Instance.GetTutorial()){
            showGuide = false;
        }else{
            tO.initializer();
            levelInitializer(tO.levelIndex);
        }
    }
    private void Update() {
        if(showGuide){
            //DoStuff
        }
    }

    private void levelInitializer(int index){
        switch(index){
            case 3: 
                cityLights = GameObject.FindGameObjectsWithTag("CityLight");
                if (tO.hintLight.active)
                {
                    tO.hintLight.SetActive(false);
                }
                break;
            default:
            tO.hint.interactable = false;
            break;
        }
    }
}
