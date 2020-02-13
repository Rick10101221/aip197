using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTimer : MonoBehaviour
{

    float currentTime = 0.0f;
    float startingTime = 300.0f;

    [SerializeField] TMPro.TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        double minutes = System.Math.Floor(currentTime / 60);
        int seconds = (int)currentTime % 60;
        if (seconds == 60)
            seconds = 0;
        if (seconds < 10)
            timerText.text = minutes.ToString("0") + ":0" + seconds.ToString("0");
        else
            timerText.text = minutes.ToString("0") + ":" + seconds.ToString("0");

        if (currentTime <= 0)
            currentTime = 0;
    }
}
