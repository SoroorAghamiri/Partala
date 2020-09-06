/* program write by Amir Hossin Alishahi
 in 12/27/2019
 version :1.0
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Win Condition for 2 objects
public class CollideDE : MonoBehaviour
{
    public GameObject GameObj;//name of the game object that must be collide
    public bool Coll;

    public GameManger GM;

    private void Start()
    {
        //        Debug.Log("4");
        GameObj = GameObject.Find("Panel2");
        GM = FindObjectOfType<GameManger>();
    }
    //برسی برخورد نقطه تماس درست با شی 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == GameObj.name)
        {
            Coll = true;
            Debug.Log(Coll);
            // GameManger.Instans.Check[++GM.indx] = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == GameObj.name)
        {
            Coll = true;
            Debug.Log("Exit");
            GameManger.Instans.Check[GM.indx--] = false;
        }
    }




}
