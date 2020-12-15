using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistentSceneManager : MonoBehaviour
{
    public static PersistentSceneManager instance;
    [Header("Fade Properties")]
    public Camera camFade;
    public GameObject fadeScreen;
    public Animator fadeScreenAnimator;
    [Header("FX properties")]
    public Camera camFX;
    public GameObject FXScreen;
    public Animator FXfadeAnimator;

    public dynamic activeScene = 0;
    public GameObject blinks;
    private Camera activeCam;
    private GameObject activeScreen;
    private Animator activeAnimator;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync(SceneNames.Start, LoadSceneMode.Additive);

        activeScene = 1;
       // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(activeScene));
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void LoadScene(dynamic myDynamic, bool FX)
    {
        if (FX)
        {
            activeCam = camFX;
            activeScreen = FXScreen;
            activeAnimator = FXfadeAnimator;
        }
        else
        {
            activeCam = camFade;
            activeScreen = fadeScreen;
            activeAnimator = fadeScreenAnimator;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0)); //Change Active scene to PersistentSceneManager
        StartCoroutine(StartFade(myDynamic));
    }

    public IEnumerator StartFade(dynamic myDynamic)
    {
        activeScreen.gameObject.SetActive(true);



        activeAnimator.ResetTrigger("Start");
        blinks.SetActive(true);
        yield return new WaitForSeconds(0.53f);
        activeCam.gameObject.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(activeScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(myDynamic, LoadSceneMode.Additive));

        activeScene = myDynamic;

        StartCoroutine(GetSceneLoadProgress());
    }
    public IEnumerator GetSceneLoadProgress()
    {

        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        activeCam.gameObject.SetActive(false);
        blinks.SetActive(false);
        activeAnimator.SetTrigger("End");
        yield return new WaitForSeconds(0.53f);

        activeScreen.gameObject.SetActive(false);
    }
}
