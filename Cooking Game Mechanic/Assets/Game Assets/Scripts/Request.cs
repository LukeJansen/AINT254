using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class to hold the information on requests.
public class Request : ScriptableObject{

    // Variable to reference the recipe book.
    private RecipeBook recipeBook;
    // Variable to reference the recipe of the request.
    private Recipe recipe;
    // Variable to hold the time left for the request to be completed.
    private float timeLeft;
    // Variable to hold the time.
    private int time;
    // Variables to hold the state of the request.
    private bool served, failed, finished;

    //Vairables to hold the game objects and their components of the in game request.
    private GameObject requestObject;
    private Slider timeLeftObject;
    private Image timeLeftColour;
    private Image foodImage;
    private Text foodText;
    private Text timeText;

    // Class Constructor
    public Request(Recipe recipe, int time, GameObject requestObject)
    {
        this.recipeBook = new RecipeBook();
        this.recipe = recipe;
        this.time = time;
        this.timeLeft = time;
        this.requestObject = requestObject;

        SetUp();
    }

    // Function for setup.
    private void SetUp()
    {
        // Finds the components for each of the individual parts of the request object.
        timeLeftObject = requestObject.transform.Find("Time Left").GetComponent<Slider>();
        timeLeftColour = requestObject.transform.Find("Time Left").transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        foodImage = requestObject.transform.Find("Food").GetComponent<Image>();
        foodText = requestObject.transform.Find("Name").GetComponent<Text>();
        timeText = requestObject.transform.Find("Time Text").GetComponent<Text>();

        // Loads the image and name for a meal.
        foodImage.sprite = Resources.Load<Sprite>(recipeBook.foodItems[recipe.meal].name);
        foodText.text = recipeBook.foodItems[recipe.meal].name;
    }

    // Function to manage time.
    public void TakeTime()
    {
        Color color;

        // Checks if there is time left and the request hasnt been completed.
        if (timeLeft > 0 && !served)
        {
            // Take a unit of time off.
            timeLeft -= 0.1f;

            // Finds the percentage of time left and applies it to the slider object.
            float value = timeLeft / time;
            timeLeftObject.value = value;

            // Updates the time left text on the object.
            if (timeLeft < 0) timeLeft = 0;
            timeText.text = timeLeft.ToString("N1") + "s";

            // Updates the color of the slider bar.
            if ((1 - value) < 0.5)
            {
                color = Color.Lerp(Color.green, Color.yellow, Mathf.Lerp(0, 1, Mathf.InverseLerp(0, 0.5f, (1- value))));
            }
            else
            {
                color = Color.Lerp(Color.yellow, Color.red, Mathf.Lerp(0, 1, Mathf.InverseLerp(0.5f, 1, (1-value))));
            }
            timeLeftColour.color = color;
        }
    }

    public void Update()
    {
        RectTransform rectTransform = requestObject.GetComponent<RectTransform>();

        // Checks if the request has failed.
        if (timeLeft <= 0)
        {
            failed = true;
            served = true;
        }
        // Manages animating the request object.
        if (served)
        {
            rectTransform.localScale = rectTransform.localScale * 0.85f;
            if (rectTransform.localScale.x < 0.001)
            {
                Debug.Log("Finished");
                finished = true;
            }
        }
        else
        {
            if (rectTransform.localScale.x < 0.025)
            {
                rectTransform.localScale = rectTransform.localScale * 1.15f; ;
            }
        }
    }

    public void FinishRequest()
    {
        served = true;
    }

    public Recipe Recipe
    {
        get
        {
            return recipe;
        }
    }

    public GameObject RequestObject
    {
        get
        {
            return requestObject;
        }
    }

    public bool Finished
    {
        get
        {
            return finished;
        }
    }

    public bool Failed
    {
        get
        {
            return failed;
        }
    }

}

