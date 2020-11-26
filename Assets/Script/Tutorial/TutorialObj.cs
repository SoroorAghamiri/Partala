using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TutorialObj", menuName = "TutorialObj", order = 51)]
public class TutorialObj : ScriptableObject
{
    public int levelIndex = 0;
    public int stepCount;
    public List<GameObject> guidText = new List<GameObject>();
    public CorrectGameObject correctGO;
     [Tooltip("Defined for Level1")]
    public GameObject nameLight = null;
    public Button hint;
    public GameObject hintLight = null;
    [Tooltip("Defined for level2")]
    public GameObject rotateLight = null;

    private TouchManager touchManager;
    private GameManger gameManager;
    private ObjectFixer objectFixer;
    private List<bool> stepIsDone;
    [SerializeField]private CurrencyView cview;
    
    

    public void initializer(){
        touchManager = GameObject.FindObjectOfType<TouchManager>();
        gameManager = GameObject.FindObjectOfType<GameManger>();
        objectFixer = GameObject.FindObjectOfType<ObjectFixer>();
        cview = GameObject.FindObjectOfType<CurrencyView>();

        stepIsDone = new List<bool>(stepCount);
        for (int j = 0; j < stepIsDone.Capacity; j++)
        {
            stepIsDone.Add(false);
        }

        for (int j = 0; j < correctGO.light.Count; j++)
        {
            correctGO.light[j].SetActive(false);
        }
    }

}

public class CorrectGameObject  
{
    public List<GameObject> obj = new List<GameObject>();
    public List<GameObject> light = new List<GameObject>();
    public List<GameObject> outline = new List<GameObject>();
}