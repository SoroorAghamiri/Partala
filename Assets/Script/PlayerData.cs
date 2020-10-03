using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int feather = 7;
    public bool noAd = false;
    public int episode = 0;
    public List<int> levelinEpisode = new List<int>();
    public float sfxLevel = 1.0f;
    public float musicLevel = 1f;
    public bool tutorial = true;
    public bool firstInfoIsShown = false;
    public bool enableSecondHint = true;
}
