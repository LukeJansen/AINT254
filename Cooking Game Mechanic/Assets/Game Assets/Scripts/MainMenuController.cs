using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Class to control the main menu scene.
public class MainMenuController : MonoBehaviour {

    // Variables to reference panels that can be opened and closed.
    public GameObject settingsPanel, helpPanel;
    // Variables to reference the settings sliders.
    public Slider volumeSlider, mouseSlider;
    // Variable to reference the data object.
    public DataHolder data;

    void Start()
    {
        // Brings the cursor onto the screen.
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Finds the data object and references it.
        data = GameObject.Find("Data").GetComponent<DataHolder>();
    }

    public void PlayButton()
    {
        // Loads the main game scene.
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        // Opens the settings panel with the predefined values from the data object.
        volumeSlider.value = data.Volume;
        mouseSlider.value = data.Mouse;

        settingsPanel.SetActive(true);
    }

    public void CloseSettingsButton()
    {
        // Closes the setting panel and sends the values to the data object.
        data.Volume = volumeSlider.value;
        data.Mouse = mouseSlider.value;
        
        settingsPanel.SetActive(false);
    }

    public void HelpButton()
    { 
        // Opens the help panel.
        helpPanel.SetActive(true);
    }

    public void CloseHelpButton()
    {
        // Closes the help panel.
        helpPanel.SetActive(false);
    }

    public void ExitButton()
    {
        // Closes the application.
        Application.Quit();
    }
}
