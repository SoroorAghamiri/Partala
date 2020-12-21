using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;
public class EpisodeManager : MonoBehaviour
{
    [SerializeField] Button[] episodeButtons;
    private dynamic buildIndexofCurrent;

    // Start is called before the first frame update
    void Start()
    {
        buildIndexofCurrent = PersistentSceneManager.instance.activeScene;
        if (buildIndexofCurrent is string)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(buildIndexofCurrent));
        }
        else
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndexofCurrent));
        }

        if (DataManager.Instance.GetEpisode() == 0)
        {
            DataManager.Instance.SetEpisode(1);

        }
        for (int i = 0; i < episodeButtons.Length; i++)//All Buttons except The First One
        {
            episodeButtons[i].interactable = false;
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Episode 1");
        }
        UnlockTillPlayerProgress();
    }

    private void UnlockTillPlayerProgress()
    {
        for (int i = 0; i < DataManager.Instance.GetEpisode() - 1; i++)//Needs Refactoring Calling Objects with their index
        {
            episodeButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            episodeButtons[i].interactable = true;
            episodeButtons[i].transform.GetChild(0).GetComponent<Image>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EpisodeOnClick(string episode)
    {

        this.GetComponent<AudioSource>().Play();
        PersistentSceneManager.instance.LoadScene(episode, true);
        // mylevelLoader.LoadLevel(episode);
    }

    public void SoundActive()
    {
        this.GetComponent<AudioSource>().Play();
    }



}
