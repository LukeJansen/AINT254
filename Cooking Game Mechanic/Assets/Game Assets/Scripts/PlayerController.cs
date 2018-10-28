using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float movementSmoothing = 0.3f;
    public float mouseSmoothing = 100f;

    public Transform mainTransform;
    public Transform cameraTranform;
    
    private float clampY = 40f;
    private float rotX, rotY;


	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 rot = cameraTranform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;
    }
	
	// Update is called once per frame
	void Update () {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseY * mouseSmoothing * Time.deltaTime;
        rotY += mouseX * mouseSmoothing * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampY, clampY);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        cameraTranform.rotation = localRotation;        

        mainTransform.position += (cameraTranform.forward * Input.GetAxis("Vertical")) * movementSmoothing;
        mainTransform.position += (cameraTranform.right * Input.GetAxis("Horizontal")) * movementSmoothing;
        mainTransform.position = new Vector3(mainTransform.position.x, 5, mainTransform.position.z);
    }
}
