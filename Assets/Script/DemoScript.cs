using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DemoScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void NextScene()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(y + 1);
    }
    public void GoBack()
    {
        int y = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(y -1);
    }
}
