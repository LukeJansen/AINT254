using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTextBehaviour : MonoBehaviour {

    public string objectName;
    public Transform cameraTransform;

    private Vector3 originalPosition;
    private TextMesh text;

	void Start () {
        cameraTransform = Camera.main.transform;

        transform.Translate(Vector3.up * 0.5f);
        originalPosition = transform.position;

        text = GetComponent<TextMesh>();
        text.text = "+1 " + objectName;
	}
	
	void FixedUpdate () {
        transform.Translate(Vector3.up * 0.025f);

        text.color = new Color(255, 255, 255, text.color.a - 0.02f);
        
        if (transform.position.y - originalPosition.y > 1)
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        transform.LookAt(cameraTransform.position);
        transform.Rotate(0, 180, 0);
    }
}
