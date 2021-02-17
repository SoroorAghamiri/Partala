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

        gm = GameObject.FindObjectOfType<GameManger>();

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
            for (timer = 0; timer < 36; timer++)
            {
                yield return new WaitForSecondsRealtime(0.001f);
                transform.Rotate(new Vector3(0, y, 0));

                if (timer == 18)
                {
                    Flip();
                }
            }
            
        }
        collectB.interactable = true;
    }
    public void addPoints()
    {
        DataManager.Instance.SetGoldenCard(DataManager.Instance.GetGoldenCard() + 1);
        DataManager.Instance.Save();
 
        if(!DataManager.Instance.GetFirstGoldenCard()){
            gm.puzzleScene();
        }else
        {
            gm.next_level();  
        }
        
        ViewManager.instance.closeView(this);
        
    }
}
