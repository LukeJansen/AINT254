using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeMeal(Food meal)
    {
        Debug.Log(meal.name + " is Served!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            TakeMeal(collision.gameObject.GetComponent<MealBehaviour>().food);
            Camera.main.GetComponent<InteractBehaviour>().DestroyPickup(collision.gameObject);
        }
    }
}
