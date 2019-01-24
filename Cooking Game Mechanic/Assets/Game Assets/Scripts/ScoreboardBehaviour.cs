using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to manage the scoreboard's behaviour.
public class ScoreboardBehaviour : MonoBehaviour {

    // Variable to reference the text object.
    private Text text;

    // Variable to keep track of the score.
    private int score;

    private void Start()
    {
        // Grab the text component and reference it.
        text = GetComponent<Text>();
    }

    // Function to add to the score.
    public void AddScore(int state)
    {
        // Takes the state of the given meal and adds the corresponding points.
        if (state == 0 || state == 2) score -= 1;
        if (state == 1) score += 5;
        if (state == 3) score -= 3;
        if (state == 4) score -= 5;

        // Updates the text.
        text.text = "Score: " + score;
    }

    public int Score
    {
        get
        {
            return score;
        }
    }
}
