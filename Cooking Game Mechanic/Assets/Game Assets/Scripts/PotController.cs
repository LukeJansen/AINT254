using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour {

    public Transform cameraTransform;    
    public PickUpBehaviour controller;
    public GameObject textPrefab;
    public GameObject potText;

    private Dictionary<string, int> contents;


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

            ShowItem(name);

            controller.DestroyPickup(collision.gameObject);
        }
    }

    private void ShowItem(string itemName)
    {
        potText = Instantiate(textPrefab, transform);
        potText.GetComponent<PotTextBehaviour>().objectName = itemName;
    }
}
