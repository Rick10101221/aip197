using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer_round : MonoBehaviour
{
    private float difficult_level; //difficult level for each turn
    private float nTime; // current time of the game
    private float start_T; // start or limited time for each turn
    private Text display_time;
    
    public float start_Time // get and set start_time
    {
        get { return start_T; }
        set { return start_T; }
    }

    public float Current_Time //??? May use when there is a "accident" in the game?increase or decrease current time
    {
        get { return nTime;  }
        set { return nTime;  }
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
    }

    // Update is called once per frame
    void Update()
    {
        nTime -= Time.deltaTime;
        display_time = nTime.ToString("f2");
        if (nTime <= 0)
        {
            //end this turn and go to next difficult level
            EndRound();
        }
    }
}
