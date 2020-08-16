using GameAnalyticsSDK.Setup;
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
        {
            _slider.value = DataManager.Instance.GetSFXLevel();
        }
        else if (gameObject.name == "MusicSlider")
        {
            _slider.value = DataManager.Instance.GetMusicLevel();
        }

    }

    // Start is called before the first frame update
    public void MakeSfxSliderValueZero()
    {
        _slider.value = 0f;
        DataManager.Instance.SetSFXLevel(0);
    }
    public void MakeMusicSliderValueZero()
    {
        _slider.value = 0f;
        DataManager.Instance.SetMusicLevel(0);
    }

}
