using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBehaviour : MonoBehaviour {

    public GameObject uiPrefab;
    public RectTransform canvas;
    public ScoreboardBehaviour scoreText;
    public int tableNumber;

    private RecipeBook recipeBook;
    private int orderLimit;
    private Request currentOrder;
    private System.Random random;
    private float startTime, lastTime;


	// Use this for initialization
	void Start () {
        recipeBook = new RecipeBook();
        random = new System.Random();
        startTime = Time.time;
        lastTime = Time.time;
	}

    // Update is called once per frame
    void Update() {
        if ((Time.time - startTime) > 5)
        {
            orderLimit = Mathf.FloorToInt(((Time.time - startTime) - 5) / 45f) + 1;
        }
        else orderLimit = 0;

        ManageOrders();
        if (currentOrder != null)
        {
            currentOrder.Update();
            ManageTime();
        }
	}

    private void ManageOrders()
    {
        if (Time.time - startTime > ((45 * tableNumber) + 10 ) && currentOrder == null)
        {
            currentOrder = 
                new Request(recipeBook.recipeItems[random.Next(recipeBook.recipeItems.Count)],
                random.Next(45, 60), Instantiate(uiPrefab, canvas));            
        }


        if (currentOrder != null && currentOrder.Finished)
        {
            if (currentOrder.Failed) scoreText.AddScore(4);
            Destroy(currentOrder.RequestObject);
            currentOrder = null;
        }
        
    }

    private void ManageTime()
    {
        if (Time.time - lastTime >= 0.1)
        {
            currentOrder.TakeTime();

            lastTime = Time.time;
        }
    }

    public void TakeMeal(MealBehaviour meal)
    {

        if (recipeBook.foodItems[currentOrder.Recipe.meal].name == meal.food.name)
        {
            scoreText.AddScore(meal.state);
            currentOrder.FinishRequest();
            return;
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
