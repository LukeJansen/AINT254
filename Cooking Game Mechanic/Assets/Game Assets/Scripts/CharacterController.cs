using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public float movementSmoothing = 0.5f;

    public Transform mainTransform;
    public Transform cameraTranform;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;

        Debug.Log(Vector3.up);
    }
	
	// Update is called once per frame
	void Update () {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            cameraTranform.Rotate(new Vector3(-mouseY, mouseX, 0));

            cameraTranform.eulerAngles = new Vector3(cameraTranform.eulerAngles.x, cameraTranform.eulerAngles.y, 0);            
        }

        mainTransform.position += (cameraTranform.forward * Input.GetAxis("Vertical")) * movementSmoothing;
        mainTransform.position += (cameraTranform.right * Input.GetAxis("Horizontal")) * movementSmoothing;
        mainTransform.position = new Vector3(mainTransform.position.x, 5, mainTransform.position.z);
	}
}
