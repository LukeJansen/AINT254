using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage behavior of the pot text when an item is added.
public class PotTextBehaviour : MonoBehaviour {

    // Variable to hold name of the collision object.
    public string objectName;
    // Variable to hold the transform of the player camera.
    public Transform cameraTransform;

    // Variable to hold the original position of the text.
    private Vector3 originalPosition;
    // Variable to hold the text.
    private TextMesh text;

	void Start () {
        // Grabs the player camera's transform.
        cameraTransform = Camera.main.transform;

        // Moves the object into position and stores that position.
        transform.Translate(Vector3.up * 0.5f);
        originalPosition = transform.position;

        // Sets the text to show the correct name.
        text = GetComponent<TextMesh>();
        text.text = "+1 " + objectName;
	}
	
	void FixedUpdate () {
        // Make the text move up.
        transform.Translate(Vector3.up * 0.025f);

        // Make the opacity slowly degrade.
        text.color = new Color(255, 255, 255, text.color.a - 0.02f);
        
        // Checks if the object has moved one unit and if so destroys it.
        if (transform.position.y - originalPosition.y > 1)
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        // Constantly look at the player.
        transform.LookAt(cameraTransform.position);
        transform.Rotate(0, 180, 0);
    }
}
