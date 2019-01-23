using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text text;

    private void Start()
    {
        text.text = GameObject.Find("Data").GetComponent<DataHolder>().Score.ToString();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnButton()
    {
        SceneManager.LoadScene(0);
    }
}
