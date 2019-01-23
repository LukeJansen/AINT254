using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

    private float volume, mouse, score;

    private bool setup;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        DefaultValues();
    }

    public void DefaultValues()
    {
        volume = 1f;
        mouse = 200f;
        score = 0;
    }

    public float Volume
    {
        get
        {
            return volume;
        }

        set
        {
            volume = value;
        }
    }

    public float Mouse
    {
        get
        {
            return mouse;
        }

        set
        {
            mouse = value;
        }
    }

    public float Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

}
