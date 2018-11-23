using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public GameObject settingsPanel;
    public Slider volumeSlider;

    private bool settingsOpen;
    private float volume;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsButton()
    {
        settingsOpen = true;
        settingsPanel.SetActive(true);
    }

    public void CloseSettingsButton()
    {
        settingsOpen = false;
        volume = volumeSlider.value;
        settingsPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
