using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker1Object : MonoBehaviour
{
    public GameObject obj;
    public bool flagWin;
    public float tarRot;
    public float dis;
    private void Start()
    {
      //  Debug.Log("2");
        flagWin = false;
    }
    // Update is called once per frame
    void Update()
    {
//        Debug.Log(Mathf.Abs(obj.transform.rotation.z));
        float rot = Mathf.Abs(obj.transform.rotation.z*180);
        if(Mathf.Abs(rot-tarRot)<=dis)
        {
            flagWin = true;
        }
        else
        {
            flagWin = false;
        }
    }
}
