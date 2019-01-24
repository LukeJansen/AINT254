using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServingBehaviour : MonoBehaviour {

    // Variable to hold the request UI prefab.
    public GameObject uiPrefab;
    // Variable to reference the canvas for the prefab.
    public RectTransform canvas;
    // Variable to reference the scoreboard.
    public ScoreboardBehaviour scoreText;
    // Variable to hold the table number to delay requests.
    public int tableNumber;
    // Variable to reference the audio source of the table.
    public AudioSource audioSource;

    // Variable to reference the recipe book.
    private RecipeBook recipeBook;
    // Variable to hold the current order.
    private Request currentOrder;
    // Variable to hold a random generator.
    private System.Random random;
    // Variables to manage timing.
    private float startTime, lastTime;

	void Start () {
        // Create a recipe book.
        recipeBook = new RecipeBook();
        // Initialise the random generator.
        random = new System.Random();
        // Set time variables to current time.
        startTime = Time.time;
        lastTime = Time.time;

        // Set audio volume to data object settings.
        audioSource.volume = GameObject.Find("Data").GetComponent<DataHolder>().Volume;
    }

    void Update() {

        ManageOrders();

        // Checks if an order is currently active.
        if (currentOrder != null)
        {
            currentOrder.Update();
            ManageTime();
        }
	}

    // Function to manage the orders.
    private void ManageOrders()
    {
        // Checks if an order is not currently active and if this table is allowed to send orders yet.
        if (Time.time - startTime > ((30 * tableNumber) + 10 ) && currentOrder == null)
        {
            // If so it adds a new order and plays an audio cue.
            currentOrder = 
                new Request(recipeBook.recipeItems[random.Next(recipeBook.recipeItems.Count)],
                random.Next(45, 60), Instantiate(uiPrefab, canvas));
            audioSource.Play();
        }

        // Checks if an order has been finished.
        if (currentOrder != null && currentOrder.Finished)
        {
            // Checks if the order failed or not.
            if (currentOrder.Failed) scoreText.AddScore(4);
            // Destroys the object and resets the currentOrder.
            Destroy(currentOrder.RequestObject);
            currentOrder = null;
        }
        
    }

    // Function to manage timings.
    private void ManageTime()
    {
        if (Time.time - lastTime >= 0.1)
        {
            currentOrder.TakeTime();

            lastTime = Time.time;
        }
    }

    // Function to check if the meal given matches the meal needed.
    public void TakeMeal(MealBehaviour meal)
    {
        // Checks if the names of the needed meal and dropped meal match.
        if (recipeBook.foodItems[currentOrder.Recipe.meal].name == meal.food.name)
        {
            // Adds to scoreboard.
            scoreText.AddScore(meal.state);
            // Finishs the request.
            currentOrder.FinishRequest();
            return;
        }
    }

    // Event for when a pickup collides with the table.
    private void OnCollisionEnter(Collision collision)
    {
        // Checks if the collision is a PickUp.
        if (collision.gameObject.tag == "PickUp")
        {
            // Calls take meal.
            TakeMeal(collision.gameObject.GetComponent<MealBehaviour>());
            // Destroys the pickup.
            Camera.main.GetComponent<InteractBehaviour>().DestroyPickup(collision.gameObject);
        }
    }
}
