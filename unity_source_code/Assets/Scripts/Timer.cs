using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLimit = 300.0f;
    private float timer;
    public Text timerText;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        // Format the timer text to show minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        timerText.text = string.Format("Time Left: {0:0}:{1:00}", minutes, seconds);

        timer -= Time.deltaTime;

        if (timer <= 1)
        {
            LoadLoseScene();
        }
    }

    void LoadLoseScene()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
