using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Class to manage the timer behaviour.
public class TimerBehaviour : MonoBehaviour {

    // Variable to hold the timers text.
    private Text text;

    // Variables to hold the timings.
    [SerializeField]
    private int timeLeft, startTime;
    private int mins, secs;
    private float lastTime;
    // Variable to hold the scoreboard's behaviour
    [SerializeField]
    private ScoreboardBehaviour score;
    // Variable to reference the audio source.
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        text = GetComponent<Text>();
        lastTime = Time.time;
        startTime = 9;

        // Grabs the volume setting from the Data Object.
        audioSource.volume = GameObject.Find("Data").GetComponent<DataHolder>().Volume;
    }

    private void Update()
    {
        // Checks if a second has passed and if so updates the timer.
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
                if (!audioSource.isPlaying) audioSource.Play();

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

    // Function to send player to the game over scene.
    private void TimesUp()
    {
        GameObject.Find("Data").GetComponent<DataHolder>().Score = score.Score;
        SceneManager.LoadScene(2);

    }
}
