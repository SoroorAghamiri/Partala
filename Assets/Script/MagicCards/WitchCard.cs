using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Needs a button to diable the second hint
public class WitchCard : DialogBase
{
    [Header("Animation Properties")]
    public float y;
    public GameObject back;
    private bool backIsActive = false;
    [SerializeField] private int timer = 0;
    private float x, z;
    public bool showEmptyCard;
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
    public void diableSecondHint()
    {
        if (!showEmptyCard)
        {
            DataManager.Instance.SetEnableSecondHint(false);
            DataManager.Instance.Save();
            GameManger.Instans.next_level();
            // }else{

        }
    }
}
