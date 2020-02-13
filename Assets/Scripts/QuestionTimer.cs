using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTimer : MonoBehaviour
{

    public Slider mainSlider;
    public TMPro.TextMeshProUGUI text;
    float currentTime = 0f;
    float startingTime = 15f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        mainSlider = GetComponent<Slider>();
        mainSlider.maxValue = startingTime;
        mainSlider.value = currentTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1f * Time.deltaTime;
        mainSlider.value = currentTime;

        if (mainSlider.value == 0)
        {
            text.CrossFadeAlpha(1, 3, false);
        }
    }
}
