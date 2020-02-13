using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_overall : MonoBehaviour
{
    private float current_T; // current time of the game
    private float minutes;
    private float seconds;
    private float max_T; // total time for the game
    private Text display_T; //TODO!!!

    public float Max_Time // get and set start_time
    {
        get { return max_T; }
        set { return max_T; }
    }

    public float Current_T //??? May use when there is a "accident" in the game?increase or decrease current time
    {
        get { return current_T; }
        set { return current_T; }
    }

    public void EndGame()
    {
        // The end of the game
    }

    // Start is called before the first frame update
    void Start()
    {
        display_T = GetComponent<Text>();
        current_T = 0;
        minutes = 0;
        seconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //update current time
        nTime += Time.deltaTime;
        minutes = nTime / 60f;
        seconds = nTime % 60f;

        //before the end of game
        if (nTime <= max_T)
        {
            //display current remaining time
            display_time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        //end of the game
        else
        {
            display_time.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            EndGame();
        }
    }
}
