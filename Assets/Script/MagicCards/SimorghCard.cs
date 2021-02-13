using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Needs a button to add the points
public class SimorghCard : DialogBase
{
    [Header("Animation Properties")]
    public float y;
    public GameObject back;
    private bool backIsActive = false;
    [SerializeField] private int timer = 0;
    private float x, z;
    private void Start()
    {
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
        int i = 0;
        for (int j = 0; j < 2; j++)
        {
            for (i = 0; i < 180; i++)
            {
                yield return new WaitForSeconds(0.01f);
                transform.Rotate(new Vector3(x, y, z));
                timer++;

                if (timer == 90 || timer == -90)//timer == 90 || timer == -90
                {
                    Flip();
                }
            }
            i = 0;
            timer = 0;
        }
        timer = 0;
    }
    public void addPoints()
    {
        DataManager.Instance.SetGoldenCard(DataManager.Instance.GetGoldenCard() + 1);
        DataManager.Instance.Save();
        Debug.Log("Point added");
        //TODO: For CardTutorial move to puzzle scene, else move to next level
        PersistentSceneManager.instance.LoadScene(SceneNames.JigsawPuzzle, false);

    }
}
