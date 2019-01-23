using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour {

    private Text text;

    [SerializeField]
    private int timeLeft, startTime;
    private int mins, secs;
    private float lastTime;
    [SerializeField]
    private ScoreboardBehaviour score;

    private void Start()
    {
        text = GetComponent<Text>();
        lastTime = Time.time;
        startTime = 10;
    }

    private void Update()
    {
        if (Time.time - lastTime > 1)
        {
            lastTime = Time.time;

            if (startTime >= 0)
            {
                text.text = "Starting in " + startTime;
                startTime -= 1;
            }
            else
            {
                timeLeft -= 1;

                mins = timeLeft / 60;
                secs = timeLeft % 60;

                if (timeLeft >= 0)
                {
                    text.text = "Time Left: " + mins + ":" + secs.ToString("00");
                }
                else
                {
                    text.text = "Times Up!";
                    TimesUp();
                }
            }
        }
    }

    private void TimesUp()
    {
        GameObject.Find("Data").GetComponent<DataHolder>().Score = score.Score;
        SceneManager.LoadScene(2);
    }
}
