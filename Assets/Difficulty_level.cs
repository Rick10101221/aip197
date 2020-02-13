using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty_level : MonoBehaviour
{

    public static float level;

    public float Difficulty // get and set start_time
    {
        get { return level; }
        set { return level; }
    }

    public float Change_level(int round, int anxiety)
    {
        //calculate the change in difficuly level and return a float
        //TODO!!!
        return 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1; 
    }

    // Update is called once per frame
    // TODO!!! We need to decide when 
    //we update the difficulty level
    void Update()
    {
        float change_level = Change_level(1, 2); //TODO!!!
        level = level + change_level; //maybe
    }
}
