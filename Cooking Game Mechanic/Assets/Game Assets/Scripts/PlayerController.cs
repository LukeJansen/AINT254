using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private DataHolder data;

    public float movementSmoothing = 0.3f;
    public float mouseSmoothing;

    public Transform mainTransform;
    public Transform cameraTranform;
    
    private float clampY = 60f;
    private float rotX, rotY;


	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = cameraTranform.localRotation.eulerAngles;
        rotX = rot.x;
        rotY = rot.y;

        data = GameObject.Find("Data").GetComponent<DataHolder>();

        mouseSmoothing = data.Mouse;
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
        GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
    }
}
