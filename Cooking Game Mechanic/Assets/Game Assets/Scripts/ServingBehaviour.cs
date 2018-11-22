using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBehaviour : MonoBehaviour {

    public GameObject uiPrefab;
    public RectTransform canvas;
    public ScoreboardBehaviour scoreText;

    private RecipeBook recipeBook;
    private int orderLimit;
    private List<Request> currentOrders;
    private System.Random random;
    private float lastTime;


	// Use this for initialization
	void Start () {
        recipeBook = new RecipeBook();
        currentOrders = new List<Request>();
        random = new System.Random();
        lastTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        orderLimit = Mathf.FloorToInt(Time.time / 45f) + 1;

        ManageOrders();
        ManageTime();
	}

    private void ManageOrders()
    {
        if (currentOrders.Count < orderLimit)
        {
            currentOrders.Add(
                new Request(recipeBook.recipeItems[random.Next(recipeBook.recipeItems.Count)],
                random.Next(45, 60), Instantiate(uiPrefab, canvas), currentOrders.Count));            
        }

        foreach(Request request in currentOrders)
        {
            request.Index = currentOrders.IndexOf(request);
            request.Update();

            if (request.Finished)
            {
                if (request.Failed) scoreText.AddScore(4);
                Destroy(request.RequestObject);
                currentOrders.Remove(request);
            }
        }
    }

    private void ManageTime()
    {
        if (Time.time - lastTime >= 0.1)
        {
            foreach (Request request in currentOrders)
            {
                request.TakeTime();
            }

            lastTime = Time.time;
        }
    }

    public void TakeMeal(MealBehaviour meal)
    {
        foreach (Request request in currentOrders)
        {
            if (recipeBook.foodItems[request.Recipe.meal].name == meal.food.name)
            {
                scoreText.AddScore(meal.state);
                request.FinishRequest();
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            TakeMeal(collision.gameObject.GetComponent<MealBehaviour>());
            Camera.main.GetComponent<InteractBehaviour>().DestroyPickup(collision.gameObject);
        }
    }
}
