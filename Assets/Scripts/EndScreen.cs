using UnityEngine;

public class EndScreen : MonoBehaviour
{
    private int correct, incorrect;

    public void Correct()
    {
        correct++;
        Debug.Log("Correct");
        Debug.Log(correct);
        Debug.Log(incorrect);
    }

    public void Incorrect()
    {
        incorrect++;
        Debug.Log("Incorrect");
        Debug.Log(correct);
        Debug.Log(incorrect);
    }
}