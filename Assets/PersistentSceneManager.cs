using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentSceneManager : MonoBehaviour
{
    public static PersistentSceneManager instance;

    public GameObject loadingScreen;
    public dynamic activeScene = 0;

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync(SceneNames.Start, LoadSceneMode.Additive);
        activeScene = 1;
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
        loadingScreen.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(activeScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(myDynamic, LoadSceneMode.Additive));

        activeScene = myDynamic;

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for( int i=0;i<scenesLoading.Count;i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        loadingScreen.gameObject.SetActive(false);

    }
}
