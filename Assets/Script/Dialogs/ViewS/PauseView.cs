using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseView : DialogBase {


    public void exitThisGame()
    {
        SceneManager.LoadScene(SceneNames.LevelSelect1);
    }
}
