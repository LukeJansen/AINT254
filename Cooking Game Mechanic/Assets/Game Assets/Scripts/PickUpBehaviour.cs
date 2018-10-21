using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpBehaviour : MonoBehaviour {

    public Material cube, cubeSelected;
    public Transform cameraTransform;
    public GameObject textObject;

    private Text text;
    private bool holding;
    private GameObject pickUp, collision;
    

	// Use this for initialization
	void Start () {
        holding = false;
        text = textObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        TextUpdate();

        if (Input.GetKeyDown(KeyCode.E) && collision != null)
        {
            if (collision.tag == "PickUp")
            {
                pickUp = collision;
                holding = !holding;
                collision.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }         
            else if (collision.tag == "Crate")
            {
                pickUp = Instantiate(collision.GetComponent<CrateObject>().crateObject);
                holding = !holding;
            }

        }

        if (holding)
        {
            pickUp.GetComponent<Transform>().position = transform.position + (cameraTransform.forward);
        }
        else
        {
            pickUp = null;
        }
		
	}

    private void TextUpdate()
    {
        if (collision != null && collision.tag != "Untagged")
        {
            switch (collision.tag)
            {
                case ("PickUp"):
                    if (holding) text.text = "Press \"E\" to Drop " + collision.name;
                    if (!holding) text.text = "Press \"E\" to Pickup " + collision.name;
                    break;
                case ("Crate"):
                    text.text = "Press \"E\" to Dispense " + collision.GetComponent<CrateObject>().name;
                    break;
                case ("Pot"):
                    text.text = "Press \"E\" to Clear Pot";
                    break;
            }        

            textObject.SetActive(true);
        }
        else
        {
            textObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!holding) collision = other.gameObject;
        //Log(collision.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (pickUp == null) collision = null;
    }

    public void DestroyPickup(GameObject collider)
    {
        Destroy(collider);
        pickUp = null;
        holding = false;
    }
}
