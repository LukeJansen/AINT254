using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingBehaviour : MonoBehaviour {

    public GameObject uiPrefab;
    public RectTransform canvas;

    private RecipeBook recipeBook;
    private int orderLimit;
    private List<Request> currentOrders;
    private System.Random random;
    private float lastSecond;


	// Use this for initialization
	void Start () {
        recipeBook = new RecipeBook();
        currentOrders = new List<Request>();
        random = new System.Random();
        lastSecond = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        orderLimit = Mathf.FloorToInt(Time.time / 120f) + 1;

        ManageOrders();
        ManageTime();
	}

    private void ManageOrders()
    {
        if (currentOrders.Count < orderLimit)
        {
            currentOrders.Add(
                new Request(recipeBook.recipeItems[random.Next(recipeBook.recipeItems.Count)],
                random.Next(30, 60), Instantiate(uiPrefab, canvas)));
            
        }
    }

    private void ManageTime()
    {
        if (Time.time - lastSecond >= 1)
        {
            foreach (Request request in currentOrders)
            {
                request.TakeSecond();
            }

            lastSecond = Time.time;
        }
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
