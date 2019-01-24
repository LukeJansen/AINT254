using UnityEngine;

// Class used with a data object to hold data accross scenes.
public class DataHolder : MonoBehaviour {

    // Define varaibles to hold data;
    private float volume, mouse, score;

    // Define variable to hold whether the data object has been setup yet.
    private bool setup;

    private void Awake()
    {
        // Make sure this object is kept on loading different scenes.
        DontDestroyOnLoad(this);

        // Makes sure there is only one data object.
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        // Sets default values for the data.
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
