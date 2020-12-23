/* program write by Amir Hossin Alishahi
 in 1/30/2019
 version :1.0
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CollisionChecker : MonoBehaviour
{
    private List<GameObject> current_Collisions = new List<GameObject>();
    public int countOfCorrectObjects;
    public GameManger gm;
    public bool correct;

    // Start is called before the first frame update
    void Start()
    {
        correct = true;
        gm = GameObject.FindObjectOfType<GameManger>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MainComponent"))
        {
            countOfCorrectObjects++;
        }
        correct = true;

        current_Collisions.Add(other.gameObject);
        foreach (GameObject game in current_Collisions)
        {
            if (game.tag == "WrongComponent")
            {
                correct = false;
                gm.WrongObjectDetected();
                break;
            }
        }
        if (correct)
        {
            gm.NoWrongObjects();
        }
    }
    //remove all of collision of game object
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "MainComponent") { countOfCorrectObjects--; }
        current_Collisions.Remove(other.gameObject);
        correct = true;
        foreach (GameObject game in current_Collisions)
        {

            if (game.tag == "WrongComponent")
            {
                correct = false;
                gm.WrongObjectDetected();
                break;

            }
        }
        if (correct)
        {
            gm.NoWrongObjects();
        }

    }
    public int FetchCorrectObjects() { return countOfCorrectObjects; }

}
