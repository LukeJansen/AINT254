using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour {

    public float volume, mouse;

    public bool setup;

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

}
