using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardBehaviour : MonoBehaviour {

    private Text text;

    private int score;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void AddScore(int state)
    {
        if (state == 0 || state == 2) score -= 1;
        if (state == 1) score += 3;
        if (state == 3) score -= 3;

        text.text = "Score: " + score;
    }
}
