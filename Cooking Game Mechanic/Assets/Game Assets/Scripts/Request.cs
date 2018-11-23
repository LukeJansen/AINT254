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
    private int index;
    private bool served, failed, finished;

    private GameObject requestObject;
    private Slider timeLeftObject;
    private Image timeLeftColour;
    private Image foodImage;
    private Text foodText;
    private Text timeText;

    public Request(Recipe recipe, int time, GameObject requestObject, int index)
    {
        this.recipeBook = new RecipeBook();
        this.recipe = recipe;
        this.time = time;
        this.timeLeft = time;
        this.requestObject = requestObject;
        this.index = index;

        SetUp();
    }

    private void SetUp()
    {
        requestObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-75 - (index * 125), 50, 0);

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
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, new Vector3(-75 - (index * 125), rectTransform.anchoredPosition.y, 0), Time.deltaTime * 1.25f);

        if (timeLeft <= 0)
        {
            failed = true;
            served = true;
        }

        if (served)
        {
            requestObject.transform.Translate(new Vector3(0, 0.5f, 0));
            if (requestObject.GetComponent<RectTransform>().anchoredPosition.y > 50)
            {
                Debug.Log("Finished");
                finished = true;
            }
        }
        else
        {
            if (requestObject.GetComponent<RectTransform>().anchoredPosition.y > -50)
            {
                requestObject.transform.Translate(new Vector3(0, -0.5f, 0));
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

    public int Index
    {
        set
        {
            index = value;
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

