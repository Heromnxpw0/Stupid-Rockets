using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    float time = 0f;
    float reset = 10f;
    public TextMeshProUGUI  timerText;
    void Start()
    {
        timerText.alignment = TextAlignmentOptions.Center;
        timerText.color = Color.black;
        timerText.fontSize = 70;
        transform.position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = "Time: " + time.ToString("F2") + "\n" + "Gen: " + GameManager.instance.generation;
        if (time >= reset) time = 0;
    }
}
