using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneManager : MonoBehaviour
{
    public static PersistentSceneManager instance;
    public Camera cam;
    public GameObject loadingScreen;
    public dynamic activeScene = 0;
    public Animator fade;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync(SceneNames.Start, LoadSceneMode.Additive);
        
        activeScene = 1;
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(activeScene));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void LoadScene(dynamic myDynamic)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0)); //Change Active scene to PersistentSceneManager
        StartCoroutine(StartFade(myDynamic));
    }

    public IEnumerator StartFade(dynamic myDynamic)
    {

        cam.gameObject.SetActive(true);
        loadingScreen.gameObject.SetActive(true);
        fade.ResetTrigger("Start");
        yield return new WaitForSeconds(0.53f);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(activeScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(myDynamic, LoadSceneMode.Additive));

        activeScene = myDynamic;

        StartCoroutine(GetSceneLoadProgress());
    }
    public IEnumerator GetSceneLoadProgress()
    {
 
        for ( int i=0;i<scenesLoading.Count;i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        cam.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        fade.SetTrigger("End");
        yield return new WaitForSeconds(0.53f);
        loadingScreen.gameObject.SetActive(false);


        if (activeScene is string)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(activeScene));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(activeScene));
        }
    }
}
