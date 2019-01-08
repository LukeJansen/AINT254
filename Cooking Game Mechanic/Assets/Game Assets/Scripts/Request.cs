using Assets.Game_Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Request : ScriptableObject{

    private RecipeBook recipeBook;
    private Recipe recipe;
    private float timeLeft;
    private int time;
    private bool served, failed, finished;

    private GameObject requestObject;
    private Slider timeLeftObject;
    private Image timeLeftColour;
    private Image foodImage;
    private Text foodText;
    private Text timeText;

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
        foodText = requestObject.transform.Find("Name").GetComponent<Text>();
        timeText = requestObject.transform.Find("Time Text").GetComponent<Text>();

        foodImage.sprite = Resources.Load<Sprite>(recipeBook.foodItems[recipe.meal].name);
        foodText.text = recipeBook.foodItems[recipe.meal].name;
    }

    public void TakeTime()
    {
        Color color;

        if (timeLeft > 0 && !served)
        {
            timeLeft -= 0.1f;

            float value = (float)timeLeft / time;
            timeLeftObject.value = value;

            if (timeLeft < 0) timeLeft = 0;
            timeText.text = timeLeft.ToString("N1") + "s";

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

        if (timeLeft <= 0)
        {
            failed = true;
            served = true;
        }
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

