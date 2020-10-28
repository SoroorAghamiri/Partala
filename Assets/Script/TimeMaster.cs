using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeMaster : MonoBehaviour
{
    DateTime currentDate;
    DateTime oldDate;

    public string saveLocation;
    public static TimeMaster Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //Set our player prefs to the save location
            saveLocation = "lastSavedDate";
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }
    /// <summary>
    /// Checks the current time against the saved time
    /// </summary>
    /// <returns></returns>
    public float CheckDate()
    {
        //Store the current time when it starts
        currentDate = System.DateTime.Now;
        string temp = PlayerPrefs.GetString(saveLocation, "1");

        //Grab the old time from the player prefs as a long
        long tempLong = Convert.ToInt64(temp);

        //Convert the old time from binary to a DateTime
        oldDate = DateTime.FromBinary(tempLong);
        print("old Time : " + oldDate);

        //Use the substract method and store the result as a timespan
        TimeSpan difference = currentDate.Subtract(oldDate);
        print("Difference : " + difference);

        return (float)difference.TotalSeconds;
    }


    /// <summary>
    /// Saves the current time, this is necessary so we can accurately check the difference later
    /// </summary>
    public void SaveDate()
    {
        //Save the Current System time
        PlayerPrefs.SetString(saveLocation, System.DateTime.Now.ToBinary().ToString());
        print("Saving This Date to player prefs :" + System.DateTime.Now);
    }
}
