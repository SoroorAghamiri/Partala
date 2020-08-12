using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LeveManger : MonoBehaviour
{
    public static LeveManger Instans;//Create Instans for class
    public Button [] level_button=new Button[15];//Buttons of Levels
    public Sprite [] SpirteImage= new Sprite[15];//complete picture of  solved puzzle 
    public Sprite Unlock;

    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
      
        MakeInstans();//
       
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        for (int i = 0; i < level_button.Length; i++)
        {
            level_button[i].interactable = false;//make buttons uninteractable

        }
        LevelUnlockCheck();//unlock the level 


    }
    
    //Create Instans
    public void MakeInstans()
    {
        if (Instans == null)
            Instans = this;
    }

    //Check for New Unlocked Levels
    public void LevelUnlockCheck()
    {
        for (int i = 1; i <= GameSys.Instans.Get_level(); i++) //check for playerprefs 
        {
            level_button[i-1].interactable = true; //make buttons interactable

        }

    }


    public void  levelOnclick(int level )
    {
        if (GameSys.Instans.Get_level() >= level)
        {
            audioSource.Play();
            SceneManager.LoadScene(level + SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    public void Onback(string episode)
    {
        SceneManager.LoadScene(episode);
    }

    public void SoundActive()
    {
        audioSource.Play();
    }
}
