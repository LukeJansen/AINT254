using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to rotate whatever object it is placed on.
public class RotateBehaviour : MonoBehaviour {

	
	void Update () {
        transform.Rotate(Vector3.up * 0.75f);
	}
}
