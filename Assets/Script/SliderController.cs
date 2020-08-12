using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider _slider;
    public static SliderController Instans;
    public void Awake()
    {
        if (Instans == null)
        {
            Instans = this;
        }
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if (gameObject.name == "SfxSlider")
        {Debug.Log(GameSys.Instans.GetSfxLevel());
            _slider.value = GameSys.Instans.GetSfxLevel();
            
        }
        else if (gameObject.name == "MusicSlider")
        {Debug.Log(GameSys.Instans.GetMusicLevel());
            _slider.value = GameSys.Instans.GetMusicLevel();
        }
        
    }

    // Start is called before the first frame update
    public void MakeSfxSliderValueZero()
    {
        _slider.value = 0f;
        GameSys.Instans.SetSfxLeve(0);
    }
    public void MakeMusicSliderValueZero()
    {
        _slider.value = 0f;
        GameSys.Instans.SetMusicLevel(0);
    }
    
}
