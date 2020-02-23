using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{

    public Slider slider;
    [SerializeField] private float thresholdAnxiety = 0.1f;


    float currentAnxiety = 0.0f;
    float fullAnxiety = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = fullAnxiety;
        slider.value = currentAnxiety;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentAnxiety += 1 * Time.deltaTime;
        slider.value = currentAnxiety;
        if (currentAnxiety >= thresholdAnxiety)
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color
                = Color.red;
        } else
        {
            slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color
                = new Color(0, 255, 0);
        }
    }
}
