using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelOfFortune : MonoBehaviour
{
    [SerializeField] float timeToRotate;
    [SerializeField] float speedf;
    [SerializeField] float amountToRotate;
    [SerializeField] float decliningSpeed;
    [SerializeField] int rewards;
    [SerializeField] float dea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartRotating()
    {
        StartCoroutine(IERotate());
    }

    private IEnumerator IERotate()
    {
         dea= amountToRotate + UnityEngine.Random.Range(0f, 5f);
        var remainingTime= timeToRotate + UnityEngine.Random.Range(-5f,6f);
        while(remainingTime>0)
        {
            yield return null;
            remainingTime -= Time.deltaTime;
            var z = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, z + dea));
        }
        var decliningAmount = amountToRotate;
        while(decliningAmount-0.5>0)
        {
            yield return null;
            decliningAmount = Mathf.Lerp(decliningAmount, 0, decliningSpeed);
            var z = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, z + decliningAmount));

        }
        float angle = transform.rotation.eulerAngles.z;
        var e = 360 / rewards;
        var d = e;
        var i = 0;
        int rew;
        while(true)
        {
            if(angle<d)
            {
                rew = i;
                break;
            }
            d = d + e;
            i++;
        }
        Debug.Log(rew+2);
    }
}
