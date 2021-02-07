using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int feather = 7;
    public bool noAdForGame = false;
    public List<bool> noAdForEachEpisode = new List<bool>();
    public int episode = 0;
    public List<int> levelinEpisode = new List<int>();
    public float sfxLevel = 1.0f;
    public float musicLevel = 1f;
    public bool tutorial = true;
    public bool firstInfoIsShown = false;
    public bool enableSecondHint = true;
    public bool initialized = false;
    public List<int> buildIndexOfLevelSelectors = new List<int>();
    public int goldenCard = 0;
}
