using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to control the player in the scene.
public class PlayerController : MonoBehaviour {

    // Variable to reference the data object.
    private DataHolder data;

    // Variable to set movement smoothing.
    public float movementSmoothing = 0.3f;
    // Variable to hold mouse smoothing from data object.
    public float mouseSmoothing;
    // Variable to hold the height of player in the scene.
    public float height;

    // Variable to hold the transform of the main player object.
    public Transform mainTransform;
    // Variable to hold the trandform of the player camera.
    public Transform cameraTranform;
    
    // Variable to hold the max angle range you can look up and down.
    private float clampY = 70f;
    // Variable to hold the current rotation.
    private float rotX, rotY;

	void Start () {
        // Hide the cursor and lock it to the centre of the screen.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Grab the current camera rotations.
        Vector3 rot = cameraTranform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        // Find the data object and reference it.
        data = GameObject.Find("Data").GetComponent<DataHolder>();

        // Grab the mouse smoothing setting from the data object.
        mouseSmoothing = data.Mouse;
    }
	
	void Update () {

        // Take the current mouse inputs.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        // Apply the mouse movements to the camera rotation.
        rotX += mouseY * mouseSmoothing * Time.deltaTime;
        rotY += mouseX * mouseSmoothing * Time.deltaTime;

        // Clamp the angle of the up and down rotation.
        rotX = Mathf.Clamp(rotX, -clampY, clampY);

        // Apply the rotations to the camera.
        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        cameraTranform.rotation = localRotation;        

        // Take keyboard inputs and apply them to the player.
        mainTransform.position += (cameraTranform.forward * Input.GetAxis("Vertical")) * movementSmoothing;
        mainTransform.position += (cameraTranform.right * Input.GetAxis("Horizontal")) * movementSmoothing;
        mainTransform.position = new Vector3(mainTransform.position.x, height, mainTransform.position.z);
        GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
    }
}
