using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotTextBehaviour : MonoBehaviour {

    public string objectName;
    public Transform cameraTransform;

    private Vector3 originalPosition;

	void Start () {
        originalPosition = transform.position;
        GetComponent<TextMesh>().text = "+1 " + objectName;
	}
	
	void FixedUpdate () {
        transform.Translate(Vector3.up * 0.05f);

        if (transform.position.y - originalPosition.y > 1)
        {
            Destroy(gameObject);
        }
	}

    private void Update()
    {
        Debug.Log("Looking");
        transform.LookAt(cameraTransform);
    }
}
