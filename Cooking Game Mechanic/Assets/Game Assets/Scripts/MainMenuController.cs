using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject settingsPanel, helpPanel;
    public Slider volumeSlider, mouseSlider;
    public DataHolder data;


    private bool settingsOpen, helpOpen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        data = GameObject.Find("Data").GetComponent<DataHolder>();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        settingsOpen = true;

        volumeSlider.value = data.Volume;
        mouseSlider.value = data.Mouse;

        settingsPanel.SetActive(true);
    }

    public void CloseSettingsButton()
    {
        settingsOpen = false;

        data.Volume = volumeSlider.value;
        data.Mouse = mouseSlider.value;
        
        settingsPanel.SetActive(false);
    }

    public void HelpButton()
    {
        helpOpen = true;

        helpPanel.SetActive(true);
    }

    public void CloseHelpButton()
    {
        helpOpen = false;

        helpPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
