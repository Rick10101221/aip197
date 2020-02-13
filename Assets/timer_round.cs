﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_round : MonoBehaviour
{
    private float nTime; // current time of the game
    private float minutes;
    private float seconds;
    private float start_T; // different start or limited time for each turn
    private Text display_time; //TODO!!!
    //should we include a variable whether the timer should be hidden?
    
    public float Start_Time // get and set start_time
    {
        get { return start_T; }
        set { start_T = value; }
    }

    public float Current_Time //??? May use when there is a "accident" in the game?increase or decrease current time
    {
        get { return nTime;  }
        set { nTime = value;  }
    }

    public void EndRound()
    {
        // The end of the current Round
    }
    
    // Start is called before the first frame update
    void Start()
    {
        display_time = GetComponent<Text>();
        nTime = start_T;
        minutes = nTime / 60f;
        seconds = nTime % 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //update current time
        nTime -= Time.deltaTime;
        minutes = nTime / 60f;
        seconds = nTime % 60f;

        // round end
        if (nTime <= 0)
        {
            // time display 0:00
            display_time.text = "0:00";
            //end this turn and go to next difficult level
            EndRound();
        }

        else
        {
            //display current remaining time
            display_time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
