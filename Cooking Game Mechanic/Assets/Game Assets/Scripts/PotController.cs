using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotController : MonoBehaviour {

    public Dictionary<string, int> contents;
    public PickUpBehaviour controller;
    public GameObject textPrefab;


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
        GameObject tempObject = Instantiate(textPrefab, transform);
        tempObject.GetComponent<PotTextBehaviour>().objectName = itemName;
    }
}
