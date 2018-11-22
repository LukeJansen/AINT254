using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request{

    private RecipeBook recipeBook;
    private Recipe recipe;
    private int time;
    private int timeLeft;

    private GameObject requestObject;
    private Slider timeLeftObject;
    private Image timeLeftColour;
    private Image foodImage;
    private Text foodText;

    public Request(Recipe recipe, int time, GameObject requestObject)
    {
        this.recipeBook = new RecipeBook();
        this.recipe = recipe;
        this.time = time;
        this.timeLeft = time;
        this.requestObject = requestObject;

        SetUp();
    }

    private void SetUp()
    {
        timeLeftObject = requestObject.transform.Find("Time Left").GetComponent<Slider>();
        timeLeftColour = requestObject.transform.Find("Time Left").transform.Find("Fill Area").transform.Find("Fill").GetComponent<Image>();
        foodImage = requestObject.transform.Find("Food").GetComponent<Image>();
        foodText = requestObject.transform.Find("Text").GetComponent<Text>();

        Debug.Log(recipeBook.foodItems[recipe.meal].name);
        foodImage.sprite = Resources.Load<Sprite>(recipeBook.foodItems[recipe.meal].name);
        foodText.text = recipeBook.foodItems[recipe.meal].name;
    }

    public void TakeSecond()
    {
        Color color;

        if (timeLeft > 0)
        {
            timeLeft -= 1;

            float value = (float)timeLeft / time;
            timeLeftObject.value = value;

            Debug.Log(1 - value);

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

}

