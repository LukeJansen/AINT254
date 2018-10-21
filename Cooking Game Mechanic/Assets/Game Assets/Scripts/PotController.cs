using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour {

    public Dictionary<string, int> contents;
    public PickUpBehaviour controller;

	// Use this for initialization
	void Start () {
        contents = new Dictionary<string, int>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            string name = collision.gameObject.name.Replace("(Clone)", "");

            if (contents.ContainsKey(name))
            {
                contents[name] += 1;
            }
            else
            {
                contents.Add(name, 1);
            }

            controller.DestroyPickup(collision.gameObject);
        }
    }
}
