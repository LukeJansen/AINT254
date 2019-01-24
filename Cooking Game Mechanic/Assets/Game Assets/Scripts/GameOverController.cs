using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Class to control the game over screen.
public class GameOverController : MonoBehaviour {

    // Variable to reference the text object on screen displaying the score.
    public Text text;

    private void Start()
    {
        // Set the score text to the score from the Data object.
        text.text = GameObject.Find("Data").GetComponent<DataHolder>().Score.ToString();

        // Reset the cursor so the user can click buttons.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnButton()
    {
        // Return to main menu scene.
        SceneManager.LoadScene(0);
    }
}
