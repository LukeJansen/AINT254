using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public Camera cam;
    public GameObject dotPrefab;
    public float force;

    private GameObject[] dotArray;
    private Transform ballTransform;
    private Vector3 ballPos, direction;
    private float magnitude;
    private float gravity = 9.81f;

	// Use this for initialization
	void Start () {
        dotArray = new GameObject[10];
        ballTransform = GetComponent<Transform>();

        for (int i = 0; i < dotArray.Length; i++)
        {
            dotArray[i] = Instantiate(dotPrefab);
            dotArray[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            ballPos = cam.WorldToScreenPoint(ballTransform.position);
            ballPos.z = 0;

            direction = ballPos - Input.mousePosition;
            magnitude = direction.magnitude * 0.1f;
            Debug.Log(magnitude);
            direction = direction.normalized;

            Aim();

            //Debug.Log("Ball: " + ballPos + " /// Mouse: " + Input.mousePosition + " /// Vector: " + direction);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }
	}

    private void Aim()
    {
        float sx = direction.x * magnitude;
        float sy = direction.y * magnitude;

        for (int i = 0; i < dotArray.Length; i++)
        {
            float t = i * 0.1f;


            float dx = sx * t;
            float dy = sy * t - 0.5f * gravity * t * t;

            dotArray[i].transform.position = new Vector3(ballTransform.position.x + dx, ballTransform.position.y + dy, ballTransform.position.z);

            dotArray[i].SetActive(true);
        }

    }

    private void Fire()
    {
        Rigidbody ballBody = GetComponent<Rigidbody>();

        float sx = direction.x * magnitude;
        float sy = direction.y * magnitude;

        ballBody.AddForce(new Vector3(sx, sy, 0));
    }

}
