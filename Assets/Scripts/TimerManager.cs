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
        transform.position = WallpaperManager.instance.wallpaperSize / 2;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timerText.text = "Time: " + time.ToString("F2");
        if (time >= reset) time = 0;
    }
}
