using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    [SerializeField] float transitionTime = 1f;




    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLevel(dynamic myDynamic)
    {
        StartCoroutine(StartAnimationForLoading(myDynamic));
        
    }
    IEnumerator StartAnimationForLoading(dynamic myDynamic)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(myDynamic);
    }
}


