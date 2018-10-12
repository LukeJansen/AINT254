using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    private void OnGUI()
    {
        GUI.Label(new Rect(5.0f, 5.0f, 200, 20), "Horizontal: " + Input.GetAxis("Horizontal"));
        GUI.Label(new Rect(5.0f, 20.0f, 200, 20), "Vertical: " + Input.GetAxis("Vertical"));
    }
}
