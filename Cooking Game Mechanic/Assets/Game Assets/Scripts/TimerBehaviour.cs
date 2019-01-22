using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour {

    private Text text;

    private int timeLeft, mins, secs;
    private float lastTime;

    private void Start()
    {
        text = GetComponent<Text>();
        timeLeft = 10;
        lastTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastTime > 1)
        {
            lastTime = Time.time;
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
            }
            
        }
    }
}
