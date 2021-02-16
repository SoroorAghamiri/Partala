using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Needs a button to add the points
public class SimorghCard : DialogBase
{
    [Header("Animation Properties")]
    public float y;
    public GameObject back;
    private bool backIsActive = false;
    [SerializeField] private int timer = 0;
    // private float x, z;
    [SerializeField]private Button collectB;
    private GameManger gm;

    private void Start()
    {
        collectB = GameObject.Find("CollectButton").GetComponent<Button>();
        collectB.interactable = false;

        // gm = GameObject.FindObjectOfType<GameManger>();

        StartCoroutine(CalculateFlip());
    }

    private void Flip()
    {
        if (backIsActive)
        {
            back.SetActive(false);
            backIsActive = false;
        }
        else
        {
            back.SetActive(true);
            backIsActive = true;
        }
    }

    private IEnumerator CalculateFlip()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 180; i++)
            {
                yield return new WaitForSecondsRealtime(0.005f);
                transform.Rotate(new Vector3(0, y, 0));
                timer++;

                if (timer == 90 || timer == -90)
                {
                    Flip();
                }
            }
            timer = 0;
        }
        collectB.interactable = true;
        timer = 0;
    }
    public void addPoints()
    {
        DataManager.Instance.SetGoldenCard(DataManager.Instance.GetGoldenCard() + 1);
        DataManager.Instance.Save();
        Debug.Log("Point added");
        //If tutorial is not shown, move to puzzle scene
        // PersistentSceneManager.instance.LoadScene(SceneNames.JigsawPuzzle, false);
        //If the tutorial is shown, move to next level
        ViewManager.instance.closeView(this);
    }
}
